using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRange : MonoBehaviour
{
    private Transform player;
    private InTheDarkMeater outside;
    public float lightRange = 4f;

    [SerializeField] private GameObject lightRangeObject;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        outside = GameObject.FindWithTag("UI").GetComponent<InTheDarkMeater>();
    }

    private void Start()
    {
        LightObjectSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > lightRange)
        {
            outside.condition = true;
        }
        else
        {
            outside.condition = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lightRange);
    }

    private void LightObjectSize()
    {
        lightRangeObject.transform.localScale = new Vector3(lightRange, lightRange, lightRange);
    }
}
