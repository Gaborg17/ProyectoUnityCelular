using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class PoolConfig
    {
        public string poolName;
        public GameObject[] prefabsToPool;
        public int poolSize;
        public int maxInScene;
        [HideInInspector] public int activeObjects;
        public Transform parentTransform;
    }

    [Header("Pool Configurations")]
    [SerializeField] private PoolConfig poolConfigPisos = new PoolConfig { poolName = "Piso" };
    [SerializeField] private PoolConfig poolConfigGemas = new PoolConfig { poolName = "Gema" };
    [SerializeField] private PoolConfig poolConfigEnemy = new PoolConfig { poolName = "Enemigo" };


    [Header("Other Data")]
    [SerializeField] private Transform spawnFloor;
    [SerializeField] private float floorSpacing = 22f;
    [SerializeField] private float lastSpacing = 22f;
    [SerializeField] private float spawnRate;
    private int lastRandomIndex = -1;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, PoolConfig> poolConfigDictionary = new Dictionary<string, PoolConfig>();


    private void Start()
    {
        CreatePool(poolConfigGemas);
        CreatePool(poolConfigPisos);
        CreatePool(poolConfigEnemy);

        StartCoroutine(SpawnFloors());
    }

    private void CreatePool(PoolConfig config)
    {
        Queue<GameObject> objectToPool = new Queue<GameObject>();


        for (int i = 0; i < config.poolSize; i++)
        {
            GameObject prefab = GetRandomPrefab(config);

            GameObject instance = Instantiate(prefab);
            instance.name = $"{config.poolName}_{i}";

            if (config.parentTransform != null)
                instance.transform.SetParent(config.parentTransform);
            else
                instance.transform.SetParent(transform);

            instance.SetActive(false);
            objectToPool.Enqueue(instance);

        }
        poolDictionary.Add(config.poolName, objectToPool);
        poolConfigDictionary.Add(config.poolName, config);


    }

    private GameObject GetRandomPrefab(PoolConfig config)
    {
        if (config.prefabsToPool == null || config.prefabsToPool.Length == 0)
            return null;

        if (config.prefabsToPool.Length == 1)
            return config.prefabsToPool[0];

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, config.prefabsToPool.Length);
        }
        while (config.prefabsToPool.Length > 1 && randomIndex == lastRandomIndex);

        lastRandomIndex = randomIndex;
        return config.prefabsToPool[randomIndex];
    }

    public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation = default)
    {
        if (!poolDictionary.ContainsKey(poolName) || !poolConfigDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"Pool '{poolName}' not found!");
            return null;
        }

        PoolConfig config = poolConfigDictionary[poolName];

        if (config.activeObjects >= config.maxInScene)
            return null;

        Queue<GameObject> objectPool = poolDictionary[poolName];

        if (objectPool.Count == 0)
        {
            Debug.LogWarning($"Pool '{poolName}' is empty! Consider increasing pool size.");
            return null;
        }

        GameObject objectToSpawn = objectPool.Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        config.activeObjects++;

        return objectToSpawn;
    }

    public void ReturnToPool(string poolName, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(poolName) || !poolConfigDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"Pool '{poolName}' not found!");
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[poolName].Enqueue(objectToReturn);

        PoolConfig config = poolConfigDictionary[poolName];
        config.activeObjects--;
    }


    private Vector3 GetNextFloorPosition()
    {
        Vector3 space = new Vector3(spawnFloor.position.x, spawnFloor.position.y + lastSpacing, spawnFloor.position.z);
        lastSpacing += floorSpacing;
        return space;
    }

    private IEnumerator SpawnFloors()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            if (poolConfigPisos.activeObjects < poolConfigPisos.maxInScene)
            {
                SpawnFromPool(poolConfigPisos.poolName, GetNextFloorPosition());

            }
        }
    }

}