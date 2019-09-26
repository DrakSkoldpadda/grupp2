using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float arrowSpeed;
    private Rigidbody2D rbody2D;

    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = new Vector2(1, 0);

        transform.Translate(direction * arrowSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball" || collision.tag == "goal1" || collision.tag == "goal2" || collision.tag == "Player" || collision.tag == "Player2")
        {
            Destroy(gameObject);
        }
    }

}
