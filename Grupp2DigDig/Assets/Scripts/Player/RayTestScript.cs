using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTestScript : MonoBehaviour
{

    [Header("Player Sphare Cast Components")]
    public GameObject currentHitObject;

    public float sphareRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;

    //Use this for something
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;

        //if (Physics.SphereCast(origin, sphareRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        //{
        //    currentHitObject = hit.transform.gameObject;
        //    currentHitDistance = hit.distance;

        //    Debug.Log("Has hit something With Test");
        //}
        //else
        //{
        //    currentHitDistance = maxDistance;
        //    currentHitObject = null;
        //    Debug.Log("Not hitting With Test");
        //}

        if (Physics.SphereCast(origin, sphareRadius, direction, out hit, maxDistance, layerMask))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;

            Debug.Log("Has hit something With Test");

            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
            Debug.Log("Not hitting With Test");

            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * maxDistance, Color.white);

        }






        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(-Vector3.up) * currentHitDistance, sphareRadius);

    }
}
