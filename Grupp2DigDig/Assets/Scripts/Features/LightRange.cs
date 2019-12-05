using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRange : MonoBehaviour
{
    private Transform player;
    [SerializeField] private InTheDarkMeater outside;
    public float lightRange = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
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
}
