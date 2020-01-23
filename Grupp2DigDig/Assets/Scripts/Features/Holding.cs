using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    [SerializeField] private GameObject lampObject;
    [SerializeField] private GameObject lampPrefab;
    private GameObject droppedLamp;

    [SerializeField] private Transform dropLocation;

    private bool dropped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !dropped)
        {
            droppedLamp = Instantiate(lampPrefab, new Vector3(dropLocation.position.x, dropLocation.position.y, dropLocation.position.z), Quaternion.identity);
            lampObject.SetActive(false);
            dropped = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && dropped && Vector3.Distance(droppedLamp.transform.position, transform.position) < 1.5f)
        {
            Destroy(droppedLamp);
            lampObject.SetActive(true);
            dropped = false;
        }
    }
}
