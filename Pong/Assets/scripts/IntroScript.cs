using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        source.PlayOneShot(clickSound);
    }

    public void finishIntro()
    {
        SceneManager.LoadScene(1);
    }
}
