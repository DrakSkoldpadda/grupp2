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
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private RespawnScript startPoint;
    [SerializeField] private ThirdPersonCamera cameraScript;

    [Header("Animator")]
    [SerializeField] private Animator animator;

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
        optionsMenu.SetActive(false);
        isInMainMenu = false;

        MouseLockState(true);

        int spawnLocation = PlayerPrefs.GetInt("SpawnLocation");

        if (spawnLocation != 1)
        {
            startPoint.Death();
        }
    }

    public void OptionsButton()
    {
        animator.SetBool("isInOptions", true);

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
        animator.SetBool("isInKeybindings", true);

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
                animator.SetBool("isInOptions", false);

                isInOptionsMenu = false;

                mainMenu.SetActive(true);

                controllerSelectedButton = optionsButton;
            }
            else if (isInKeybindingsMenu)
            {
                animator.SetBool("isInKeybindings", false);

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

            //Time.timeScale = 0f;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Time.timeScale = 1f;
        }
    }
}
