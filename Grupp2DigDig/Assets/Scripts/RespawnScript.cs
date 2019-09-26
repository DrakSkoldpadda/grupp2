using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject playerTransform;
    public GameObject[] RespawnLocations;

    public GameObject placeToSpawn;
    public int amountOfSpawnPoints;

    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        placeToSpawn.transform.position = RespawnLocations[0].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<spawnPointScript>() != null)
        {
            placeToSpawn.transform.position = RespawnLocations[other.GetComponent<spawnPointScript>().number].transform.position;
        }
         
        if (other.tag == "dangerous")
        {
            Death();
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        Death();
    //}

    public void Death()
    {
        StartCoroutine(WhatHappensInRespawn());
    }

    IEnumerator WhatHappensInRespawn()
    {
        playerAnimator.SetTrigger("die");
        yield return new WaitForSeconds(2);
        playerTransform.transform.position = placeToSpawn.transform.position;
    }
}
