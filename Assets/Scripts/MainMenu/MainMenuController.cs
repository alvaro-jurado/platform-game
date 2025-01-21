using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas optionsCanvas;
    public Canvas videoCanvas;
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Options()
    {
        mainCanvas.GetComponent<Canvas>().enabled = false;
        optionsCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void Audio()
    {
        mainCanvas.GetComponent<Canvas>().enabled = false;
        optionsCanvas.GetComponent<Canvas>().enabled = false;
        videoCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void Video()
    {
        mainCanvas.GetComponent<Canvas>().enabled = false;
        optionsCanvas.GetComponent<Canvas>().enabled = false;
        videoCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void Back()
    {
        optionsCanvas.GetComponent<Canvas>().enabled = false;
        mainCanvas.GetComponent<Canvas>().enabled = true;
        videoCanvas.GetComponent<Canvas>().enabled = false;
    }
    public void BackOptions()
    {
        optionsCanvas.GetComponent<Canvas>().enabled = true;
        mainCanvas.GetComponent<Canvas>().enabled = false;
        videoCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
