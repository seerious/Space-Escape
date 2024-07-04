using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Game States
    public bool asteroidField = false;
    public bool gameOver = false;
    public bool lasersFixed = false;
    public bool targetted = false;


    //Scripts
    private UIManager uiManagerScript;

    private void Awake()
    {
        //Import scripts
        uiManagerScript = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Update()
    {
        if (gameOver) //Check if game over 
        {
            uiManagerScript.isGameOver = true; //Update UI script
            uiManagerScript.ShowGameOver(); //Show game over text

            //Restart game if player presses spacebar
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }

            //Return to menu if player presses escape key
            else if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
