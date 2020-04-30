using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRange : MonoBehaviour
{
    private Transform player;
    private InTheDarkMeater outside;
<<<<<<< HEAD
    public float lightRange = 10f;
=======
    public float lightRange = 4f;
    private bool isInLight;

>>>>>>> efe8b65f4e7e58b3ae4fa5d48388270b2c19d72a

    //[SerializeField] private GameObject lightRangeObject;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        outside = GameObject.FindWithTag("UI").GetComponent<InTheDarkMeater>();
    }

    //private void Start()
    //{
    //    LightObjectSize();
    //}

    // Update is called once per frame
    void Update()
    {
        // If the player is ferther than lightRange away then reduse the ammount of light the player is in
        if (Vector3.Distance(player.position, transform.position) > lightRange && isInLight)
        {

            isInLight = false;
            outside.lightRanges--;
            print("Outside");

        }
        else if (Vector3.Distance(player.position, transform.position) <= lightRange && !isInLight)
        {
            isInLight = true;
            outside.lightRanges++;
            print("Inside");

        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lightRange);
    }

    //private void LightObjectSize()
    //{
    //    if (lightRangeObject != null)
    //    {
    //        lightRangeObject.transform.localScale = new Vector3(lightRange, lightRange, lightRange);
    //    }
    //}
}
