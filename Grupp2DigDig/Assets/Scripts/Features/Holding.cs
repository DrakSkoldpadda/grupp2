using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    [SerializeField] private GameObject lampObject;
    [SerializeField] private GameObject lampPrefab;
    private GameObject droppedLamp;

    [SerializeField] private Transform dropLocation;

    private bool holdingAItem = false;
    private bool holdingLatern = false;

    [SerializeField] private float pickUpDistance = 2f;

    private ItemsToHold holingItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Lantern();
            HoldingItem();
        }
    }

    void HoldingItem()
    {

    }

    void Lantern()
    {
        if (holdingLatern)
        {
            droppedLamp = Instantiate(lampPrefab, new Vector3(dropLocation.position.x, dropLocation.position.y, dropLocation.position.z), Quaternion.identity);
            lampObject.SetActive(false);
            holdingLatern = false;
        }

        else if (!holdingLatern && Vector3.Distance(droppedLamp.transform.position, transform.position) < pickUpDistance)
        {
            Destroy(droppedLamp);
            lampObject.SetActive(true);
            holdingLatern = true;
        }
    }
}
