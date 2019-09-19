using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveVertically = Input.GetAxis("vertical");

        Vector2 movementUpDown = new Vector2(0, moveVertically);
        
        rb2d.velocity = new Vector2(0, moveVertically * )
    }
}
