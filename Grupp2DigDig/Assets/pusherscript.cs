using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusherscript : MonoBehaviour
{

    public BoxCollider PushBoxAkaHandPosition;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward);
            print("putting");
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
