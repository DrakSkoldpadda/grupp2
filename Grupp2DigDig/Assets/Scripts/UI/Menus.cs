using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Button pauseOptionsButton;
    [SerializeField] private Button pauseResumeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button optionsBackButton;
    [SerializeField] private Button keybindingsBackButton;

    [Header("Things")]
    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private RespawnScript startPoint;

    private bool isInMainMenu = true;
    private bool isInOptionsMenu = false;
    private bool isInKeybindingsMenu = false;
    private bool isInPauseMenu = false;

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
        if (Input.GetButtonDown("Pause"))
        {
            if (isInOptionsMenu || isInKeybindingsMenu)
            {
                BackButton();
            }

            if (!isInMainMenu)
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
    }

    public float volumeValue;

    public void SetVolumeLevel(float sliderValue)
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

        PlayerPrefs.GetInt("SpawnLocation");
    }

    public void OptionsButton()
    {
        isInOptionsMenu = true;

        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        controllerSelectedButton = backButton;
    }

    public void MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void KeybindingsMenu()
    {
        isInOptionsMenu = false;
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

        if (isInMainMenu)
        {
            controllerSelectedButton = optionsButton;
            if (isInOptionsMenu)
            {
                isInOptionsMenu = false;

                mainMenu.SetActive(true);
            }
            if (isInKeybindingsMenu)
            {
                isInKeybindingsMenu = false;
                isInOptionsMenu = true;

                optionsMenu.SetActive(true);
                keybindingsMenu.SetActive(false);

                controllerSelectedButton = optionsBackButton;
            }
        }
        else if (isInPauseMenu)
        {
            isInPauseMenu = false;

            controllerSelectedButton = pauseOptionsButton;

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
