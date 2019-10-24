using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;

    private bool isInMainMenu = true;
    private bool isInPauseMenu = false;

    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioMixer mixer;


    private void Awake()
    {
        if (isInMainMenu)
        {
            MouseLockState(true);
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            pauseMenu.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        mixer.GetFloat("MasterVolume", out float value);
        audioSlider.value = value;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isInMainMenu)
        {
            if (isInPauseMenu)
            {
                isInPauseMenu = true;
                pauseMenu.SetActive(true);
                MouseLockState(true);
            }

            else
            {
                isInPauseMenu = true;
                pauseMenu.SetActive(true);
                MouseLockState(false);
            }
        }
    }

    public float volumeValue;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);

        if (sliderValue == 0f)
        {
            mixer.SetFloat("MasterVolume", -80f);
        }
    }

    public void PlayButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        isInMainMenu = false;

        MouseLockState(false);
    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        backButton.Select();
    }

    public void BackButton()
    {
        optionsMenu.SetActive(false);
        optionsButton.Select();

        if (isInMainMenu)
        {
            mainMenu.SetActive(true);
        }
        else if (isInPauseMenu)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void MouseLockState(bool lockState)
    {
        if (lockState)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
