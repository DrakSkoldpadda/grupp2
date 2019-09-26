using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator playerAnimator;

    public float speed;
    public float boundY;
    private Rigidbody2D rb2d;
    public GameObject arrow;
    public GameObject arrowSpawnPoint;
    public GameObject[] arrowQuiver;
    public string stunSound;
    private SoundScript audioManager;


    [SerializeField]
    private int numberOfArrows = 5;
    bool stunned;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        audioManager = SoundScript.instance;
    }

    private void Update()
    {
        float moveVertically = Input.GetAxisRaw("Vertical");

        Vector2 movementUpDown = new Vector2(0, moveVertically);

        if (stunned != true)
            rb2d.velocity = new Vector2(0, moveVertically * speed * Time.deltaTime);

        if (transform.position.y >= boundY)
        {
            transform.position = new Vector2(transform.position.x, boundY);
        }
        if (transform.position.y <= -boundY)
        {
            transform.position = new Vector2(transform.position.x, -boundY);
        }

        if (Input.GetButtonDown("P1_Fire1") && numberOfArrows >= 1)
        {
           
            GameObject arrowGO = (GameObject)Instantiate(arrow);
            arrowGO.transform.position = arrowSpawnPoint.transform.position;
            numberOfArrows--;
            arrowQuiver[numberOfArrows].SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "arrow" && stunned == false)
        {
            StartCoroutine(Stunned());
        }

        if (collision.tag == "ball")
        {
            playerAnimator.SetTrigger("hit");
        }
    }

    public IEnumerator Stunned()
    {
        stunned = true;
        audioManager.PlaySound(stunSound);
        playerAnimator.SetTrigger("stunned");
        rb2d.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(4);
        stunned = false;
    }
}
