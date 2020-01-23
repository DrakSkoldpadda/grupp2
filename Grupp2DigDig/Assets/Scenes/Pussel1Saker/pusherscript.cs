using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusherscript : MonoBehaviour
{

    public BoxCollider PushBoxAkaHandPosition;


    public float power;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.transform.Translate((other.gameObject.transform.position - GetComponentInParent<Transform>().position) * power);
            print("putting");
        }
    }
}
