using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ObjectPooling : MonoBehaviour
{
    public GameObject[] prefabsToPool;

    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private Transform spawnFloor;
    private float spacing = 22f;
    private float lastspacing = 22f;

    [SerializeField] private Transform[] parentsOfObjects;

    [SerializeField] private float spawnrate;

    [SerializeField] private int poolSize;
    [SerializeField] private int maxObjectsInScene;
    [SerializeField] private int activeObjects;

    Queue<GameObject> pool;

    private void Start()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(prefabsToPool[0]);
            instance.transform.SetParent(parentsOfObjects[0]);
            instance.SetActive(false);
            pool.Enqueue(instance);
        }

        StartCoroutine(Spawns());
    }


    private IEnumerator Spawns()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnrate);
            if (activeObjects < maxObjectsInScene && pool.Count > 0)
            {
                GameObject objeto = pool.Dequeue();

                Vector3 space = new Vector3(spawnFloor.position.x, spawnFloor.position.y + lastspacing, spawnFloor.position.z);
                lastspacing += spacing;
                objeto.transform.position = space;
                objeto.SetActive(true);
                activeObjects++;
            }
        }
    }

    public void ReturnToQueue(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
        activeObjects--;
    }


}
