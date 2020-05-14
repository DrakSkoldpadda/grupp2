using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyController : MonoBehaviour
{
    public int keyCollected = 0;
    private bool messageSent = false;
    [SerializeField] private TextMeshProUGUI keyCollectText;
    [SerializeField] private Animator anim;


    private void Update()
    {
        if(keyCollected == 1 && !messageSent)
        {
            keyCollectText.text = "The key seems to be broken? I need to find the other piece";

            anim.SetTrigger("PopuppTrigger");

            messageSent = true;
        }
        if (keyCollected == 2 && messageSent)
        {

            StartCoroutine(popupReturn());

            //Tell player that they have 1 key
            messageSent = false;            
        }
    }

    IEnumerator popupReturn()
    {
        anim.SetTrigger("ReturnTrigger");
        yield return new WaitForSeconds(1);
        keyCollectText.text = "The piece fit perfecly with the other one! I should be able to open that gate now";

        anim.SetTrigger("PopuppTrigger");


    }


    // Handle player key ammount
}
