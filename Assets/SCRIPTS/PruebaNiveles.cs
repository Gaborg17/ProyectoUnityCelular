using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaNiveles : MonoBehaviour
{
    private CheckVisibility visibility;

    [Header("Enemy Spawn Info")]
    public List<Transform> eSpawns1 = new List<Transform>();
    private List<Transform> eSpawns2 = new List<Transform>();
    private bool enemiesSpawned = false;

    private void Start()
    {
        visibility = GetComponent<CheckVisibility>();

        Transform floor1Parent = transform.GetChild(0);
        foreach (Transform child in floor1Parent)
        {
            eSpawns1.Add(child);
        }

        Transform floor2Parent = transform.GetChild(1);
        foreach (Transform child in floor2Parent)
        {
            eSpawns2.Add(child);
        }


    }

    private void Update()
    {
        if (visibility.InFrame() && enemiesSpawned == false)
        {
            enemiesSpawned = true;
            SpawnEnemiesOnFloor(eSpawns1, 3, 7);
            SpawnEnemiesOnFloor(eSpawns2, 3, 7);
        }

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
    
    public void SpawnEnemiesOnFloor(List<Transform> floorSpawns, int minEnemies, int maxEnemies)
    {
        ObjectPooling oB = FindAnyObjectByType<ObjectPooling>();
        int enemiesToSpawn = Random.Range(minEnemies,maxEnemies);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            oB.SpawnFromPool("Enemigo", floorSpawns[i].position);
            //Debug.Log($"Punto de spawn {i}: {floorSpawns[i].position}");
        }
    }



}
