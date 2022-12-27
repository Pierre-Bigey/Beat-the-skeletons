using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnersObjects;
    [SerializeField] GameObject skeletonPrefab;
    

    public void SummonSkeleton()
    {
        GameObject spawner = spawnersObjects[Random.Range(0, spawnersObjects.Length)];
        Instantiate(skeletonPrefab, spawner.transform.position, spawner.transform.rotation);
    }

}
