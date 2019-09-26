using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume = 0.7f;
    public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

    }
}

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("more than one AudioManager in the scene.");
        else
            instance = this;

    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform); 
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }

            //no sound with that name
            Debug.LogWarning("audioManager: sound not found in the list, " + _name);
        }
    }

}
