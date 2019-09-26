using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    private AudioSource source;
    public AudioClip drawerSound;
    public AudioClip paperUnfoldSound;
    public AudioClip paperPaperSound;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayDrawerSound()
    {
        source.PlayOneShot(drawerSound);
    }

    public void PlayUnfoldPaperSound()
    {
        source.PlayOneShot(paperUnfoldSound);
    }

    public void PlayPaperSound()
    {
        source.PlayOneShot(paperPaperSound);
    }
}
