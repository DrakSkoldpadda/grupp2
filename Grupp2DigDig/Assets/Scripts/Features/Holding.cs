using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holding : MonoBehaviour
{
    [SerializeField] private GameObject lampObject;
    [SerializeField] private GameObject lampPrefab;
    private GameObject droppedLamp;

    [SerializeField] private Transform dropLocation;
    [SerializeField] private Transform holdingLocation;

    private bool holdingAItem = false;
    private bool holdingLatern = true;

    [SerializeField] private float pickUpDistance = 2f;

    private ItemsToHold itemToHold;

    private GameObject[] items;
    private GameObject closest = null;

    private void Start()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!holdingAItem)
            {
                Lantern();
            }
            if (!holdingLatern)
            {
                HoldingItem();
            }
        }
        if (holdingAItem)
        {
            float step = 10 * Time.deltaTime;
            itemToHold.transform.position = Vector3.MoveTowards(itemToHold.transform.position, holdingLocation.position, step);
        }
    }

    void HoldingItem()
    {
        if (holdingAItem)
        {
            itemToHold.transform.position = dropLocation.position;
            itemToHold = null;
        }

        else
        {
            itemToHold = ClosestItem().GetComponent<ItemsToHold>();
        }
    }

    public GameObject ClosestItem()
    {
        foreach (GameObject item in items)
        {
            Vector3 diff = item.transform.position - transform.position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < pickUpDistance)
            {
                closest = item;
            }
        }
        if (closest != null)
        {
            return closest;
        }
        else
        {
            return null;
        }
    }

    void Lantern()
    {
        if (holdingLatern)
        {
            droppedLamp = Instantiate(lampPrefab, new Vector3(dropLocation.position.x, dropLocation.position.y, dropLocation.position.z), Quaternion.identity);
            lampObject.SetActive(false);
            holdingLatern = false;
        }

        else if (!holdingLatern && !holdingAItem && Vector3.Distance(droppedLamp.transform.position, transform.position) < pickUpDistance)
        {
            Destroy(droppedLamp);
            lampObject.SetActive(true);
            holdingLatern = true;
        }
    }
}
