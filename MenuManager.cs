using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame()
    {
        //If player clicks play button load the game
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //If player hits close button close the game
        Application.Quit();
    }
}
