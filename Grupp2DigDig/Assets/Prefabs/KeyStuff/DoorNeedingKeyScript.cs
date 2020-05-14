using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorNeedingKeyScript : MonoBehaviour
{
    private bool doorOpen = false;
    private Animator anim;
    [SerializeField] private TextMeshPro needKeyText;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorOpen)
        {

            if (GameObject.FindGameObjectWithTag("Player"))
            {
                // Check if player has 2 keys
                // If So open and send message that the player has inserted the key. Also Remove 2 keys from player.
                // If not put a message to look for a key
                if (other.GetComponent<KeyController>().keyCollected >= 2)
                {
                    anim.SetTrigger("DoorOpen");

                    other.GetComponent<KeyController>().keyCollected -= 2;
                    needKeyText.text = "You used the key to open the door!";
                    StartCoroutine(textDissapearTime());
                    doorOpen = true;

                }
                else
                {
                    //Tell the player to look for more keys
                    needKeyText.text = "You need to find a key.";

                    StartCoroutine(textDissapearTime());
                }
            }
        }
    }

    IEnumerator textDissapearTime()
    {
        yield return new WaitForSeconds(5);
        needKeyText.text = "";
    }



}
