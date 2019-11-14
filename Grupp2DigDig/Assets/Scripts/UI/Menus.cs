using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class Menus : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject keybindingsMenu;
    [SerializeField] private GameObject pauseMenu;

    [Header("Buttons")]
    [SerializeField] private Button controllerSelectedButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button optionsBackButton;
    [SerializeField] private Button keybindingsBackButton;
    [SerializeField] private Button pauseResumeButton;

    private bool isInMainMenu = true;
    private bool isInOptionsMenu = false;
    private bool isInKeybindingsMenu = false;
    private bool isInPauseMenu = false;

    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioMixer mixer;

    private bool firstTime = false;

    private void Start()
    {
        mixer.SetFloat("MasterVolume", volumeValue);
        audioSlider.value = volumeValue;

        firstTime = true;

        MouseLockState(false);
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        keybindingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButton("Vertical") && firstTime)
        {
            firstTime = false;

            controllerSelectedButton.Select();
        }

        Pause();
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Pause") && !isInMainMenu)
        {
            if (isInPauseMenu)
            {
                isInPauseMenu = false;
                pauseMenu.SetActive(false);
                MouseLockState(true);
            }

            else
            {
                controllerSelectedButton = pauseResumeButton;
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

        MouseLockState(true);
    }

    public void OptionsButton()
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        controllerSelectedButton = backButton;
    }

    public void KeybindingsMenu()
    {
        isInKeybindingsMenu = true;

        keybindingsMenu.SetActive(true);
        optionsMenu.SetActive(false);

        controllerSelectedButton = keybindingsBackButton;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        isInPauseMenu = false;
    }

    public void BackButton()
    {
        optionsMenu.SetActive(false);
        controllerSelectedButton = optionsButton;

        if (isInMainMenu)
        {
            if (isInOptionsMenu)
            {
                mainMenu.SetActive(true);
            }
            if (isInKeybindingsMenu)
            {
                optionsMenu.SetActive(true);

                controllerSelectedButton = optionsBackButton;
            }
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
        if (!lockState)
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
