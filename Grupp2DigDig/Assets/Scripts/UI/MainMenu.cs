using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;

    private bool isInMainMenu = true;
    private bool isInPauseMenu = false;

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

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        optionsButton.Select();
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
