using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, ISelectHandler
{
    public string selectSound;
    public string clickSound;
    private SoundScript audioManager;

    private void Start()
    {
        audioManager = SoundScript.instance;
    }

    public void OnSelect(BaseEventData eventData)
    {
        audioManager.PlaySound(selectSound);
    }

    public void playClickSound()
    {
        audioManager.PlaySound(clickSound);
    }
}
