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
    public bool canMove = false;

    [SerializeField] private EventSystem eventSystem;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject keybindingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject game;

    [Header("Buttons")]
    [SerializeField] private Button controllerSelectedButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button pauseOptionsButton;
    [SerializeField] private Button pauseResumeButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button optionsBackButton;
    [SerializeField] private Button keybindingsBackButton;

    [Header("Audio")]
    [SerializeField] private Slider audioSlider;
    [SerializeField] private AudioMixer mixer;

    [Header("Sensitivity")]
    [SerializeField] private Slider sensitivitySlider;

    [Header("Start")]
    [SerializeField] private RespawnScript startPoint;

    [Header("Camera")]
    [SerializeField] private ThirdPersonCamera cameraScript;

    private bool isInMainMenu = true;
    private bool isInOptionsMenu = false;
    private bool isInKeybindingsMenu = false;
    private bool isInPauseMenu = false;

    private float volumeValue = 1f;
    private float sensitivityValue = 1f;

    private bool firstTime = false;

    private void Start()
    {
        mixer.SetFloat("MasterVolume", volumeValue);
        volumeValue = audioSlider.value;
        sensitivityValue = sensitivitySlider.value;

        firstTime = true;

        MouseLockState(false);
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        keybindingsMenu.SetActive(false);
        game.SetActive(false);

        cameraScript.canUseCamera = false;
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

    private void SliderValue(Slider slider)
    {
        if (eventSystem.currentSelectedGameObject == slider.gameObject)
        {
            slider.value += Input.GetAxis("Horizontal");
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (isInOptionsMenu || isInKeybindingsMenu)
            {
                cameraScript.canUseCamera = false;
                BackButton();
            }
            else if (!isInMainMenu)
            {
                if (isInPauseMenu)
                {
                    Resume();
                }

                else
                {
                    controllerSelectedButton = pauseResumeButton;
                    isInPauseMenu = true;
                    pauseMenu.SetActive(true);
                    game.SetActive(false);
                    MouseLockState(false);
                    cameraScript.canUseCamera = false;
                }
                firstTime = true;
            }
        }
    }

    public void SetVolumeLevel(float sliderValue)
    {
        sliderValue = volumeValue;

        mixer.SetFloat("MasterVolume", Mathf.Log10(volumeValue) * 20);

        if (volumeValue == 0f)
        {
            mixer.SetFloat("MasterVolume", -80f);
        }
    }

    public void SetSensitivity(float sliderValue)
    {
        sliderValue = sensitivityValue;

        cameraScript.sensivityX = sensitivityValue;
        cameraScript.sensivityY = sensitivityValue / 2f;
    }

    public void PlayButton()
    {
        cameraScript.canUseCamera = true;

        mainMenu.SetActive(false);
        game.SetActive(true);
        isInMainMenu = false;

        MouseLockState(true);
        canMove = true;

        int spawnLocation = PlayerPrefs.GetInt("SpawnLocation");

        if (spawnLocation != 0)
        {
            startPoint.Death();
        }
    }

    public void OptionsButton()
    {
        isInOptionsMenu = true;

        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);

        controllerSelectedButton = backButton;
        firstTime = true;
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
        firstTime = true;
    }

    public void Resume()
    {
        isInPauseMenu = false;
        pauseMenu.SetActive(false);
        game.SetActive(true);
        MouseLockState(true);
        controllerSelectedButton = pauseResumeButton;

        cameraScript.canUseCamera = true;
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

                controllerSelectedButton = optionsButton;
            }
            else if (isInKeybindingsMenu)
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

            if (isInOptionsMenu)
            {
                isInOptionsMenu = false;

                controllerSelectedButton = pauseOptionsButton;

                pauseMenu.SetActive(true);
            }
            else if (isInKeybindingsMenu)
            {
                isInKeybindingsMenu = false;
                isInOptionsMenu = true;

                optionsMenu.SetActive(true);
                keybindingsMenu.SetActive(false);

                controllerSelectedButton = optionsBackButton;
            }
        }
        firstTime = true;
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
