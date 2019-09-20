using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform[] RespawnLocations;

    private Transform placeToSpawn;
    public int amountOfSpawnPoints;

    private void Start()
    {
        RespawnLocations[0] = placeToSpawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<spawnPointScript>() != null)
        {
            placeToSpawn = RespawnLocations[other.GetComponent<spawnPointScript>().number];
        }
    }

    public void Death()
    {

        StartCoroutine(whatHappensInRespawn());
    }

    IEnumerator whatHappensInRespawn()
    {
        yield return new WaitForSeconds(2);
        playerTransform.position = placeToSpawn.position;
    }
}
