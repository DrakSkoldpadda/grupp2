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

    [SerializeField] private Button backButton;
    [SerializeField] private Button optionsButton;

    private bool isInMainMenu = true;
    private bool isInPauseMenu = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {

        }
    }

    public void StartButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        isInMainMenu = false;
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
        optionsButton.Select();
    }
}
