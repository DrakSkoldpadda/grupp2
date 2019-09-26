using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOrganizerScript : MonoBehaviour
{

    public void Play()
    {
        StartCoroutine(whatHappensInPlay());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator whatHappensInPlay()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(2);

    }
    public void Update()
    {
        Cursor.visible = false;
    }
}
