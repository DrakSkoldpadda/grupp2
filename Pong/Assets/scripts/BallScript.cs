using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D ballbody;
    public int startDirection;
    public Transform resetLocation;
    public GameObject goalParticles;

    public Transform playerTransform;
    public Transform playerTwoTransform;

    [SerializeField]
    private float angleIncreaseValue = 5;

    private bool playerOneWonTheBall;

    private SoundScript audioManager;
    public string wallBounce;
    public string playerBounce;
    public string goalBounce;


    Vector3 direction;

    private void Start()
    {
        audioManager = SoundScript.instance;

    }


    private void OnEnable()
    {
        speed = 1.5f;
        if (playerOneWonTheBall == true)
        {

            direction = new Vector3(-1, 0, 0);
        }
        else
            direction = new Vector3(1, 0, 0);
    }


    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "topBorder")
        {
            direction.y *= -1;
            audioManager.PlaySound(wallBounce);
        }

        if (collision.tag == "Player")
        {
            speed += .05f;
            direction.x *= -1;
            audioManager.PlaySound(playerBounce);

            float dist = Vector2.Distance(transform.position, playerTransform.position);

            if (dist < .03f)
            {
                direction.y = 0;
                return;
            }

            if (transform.position.y > playerTransform.position.y)
            {
                direction.y = dist * angleIncreaseValue;
            }
            else
                direction.y = -dist * angleIncreaseValue;

        }

        if (collision.tag == "Player2")
        {
            speed += .05f;
            direction.x *= -1;
            audioManager.PlaySound(playerBounce);

            float dist2 = Vector2.Distance(transform.position, playerTwoTransform.position);

            if (dist2 < .03f)
            {
                direction.y = 0;
                return;
            }

            if (transform.position.y > playerTwoTransform.position.y)
            {
                direction.y = dist2 * angleIncreaseValue;
            }
            else
                direction.y = -dist2 * angleIncreaseValue;

        }

        if (collision.tag == "goal1")
        {
            playerOneWonTheBall = false;
            GameObject Particles = (GameObject)Instantiate(goalParticles);
            Particles.transform.position = transform.position;
            audioManager.PlaySound(goalBounce);
            gameObject.transform.position = resetLocation.position;
            gameObject.SetActive(false);
        }
        else if (collision.tag == "goal2")
        {
            playerOneWonTheBall = true;
            GameObject Particles = (GameObject)Instantiate(goalParticles);
            Particles.transform.position = transform.position;
            audioManager.PlaySound(goalBounce);
            gameObject.transform.position = resetLocation.position;
            gameObject.SetActive(false);
        }

        if (collision.tag == "arrow")
        {
            audioManager.PlaySound(wallBounce);
            direction.x *= -1;
        }
    }

    private void OnBecameInvisible()
    {

        //if ball would ever pass through walls. 
        if (gameObject != null)
            gameObject.transform.position = resetLocation.position;
    }
}
