using System.Collections;
using UnityEngine;

public class PruebaNiveles : MonoBehaviour
{
    private CheckVisibility visibility;

    [Header("Enemy Spawn Info")]
    [SerializeField] private Transform[] enemySpawnsFloor1;
    [SerializeField] private Transform[] enemySpawnsFloor2;



    private void Start()
    {
        visibility = GetComponent<CheckVisibility>();
        //SpawnEnemiesOnFloor(enemySpawnsFloor1,3,7);
        //SpawnEnemiesOnFloor(enemySpawnsFloor2,3,7);
    }

    private void Update()
    {
        if (visibility.IsVisible())
        {
            return;
        }

        else if (!visibility.IsVisible())
        {
            ObjectPooling oP = FindAnyObjectByType<ObjectPooling>();
            oP.ReturnToPool("Piso",this.gameObject);
        }
    }
    
    public void SpawnEnemiesOnFloor(Transform[] floorSpawns, int minEnemies, int maxEnemies)
    {
        ObjectPooling oB = FindAnyObjectByType<ObjectPooling>();
        int enemiesToSpawn = Random.Range(minEnemies,maxEnemies);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            oB.SpawnFromPool("Enemigo", floorSpawns[i].position);
        }
    }



}
