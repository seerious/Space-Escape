using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Instances
    public static UIManager instance;


    //Booleans
    public bool isGameOver;


    //Text
    private TextMeshProUGUI bestScoreText;
    private TextMeshProUGUI scoreText;

    public TextMeshProUGUI gameOverText; //Starts as deactivated


    //Values
    private int addScoreRate = 1;
    private int addScoreRateSpeedBoosted = 3;
    private int bestScore = 0;

    public int score = 0;




    void Awake()
    {
        //Set Instance to save best score
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }


        //Import text objects
        bestScoreText = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("CurrentScore").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        LoadBestScore(); //Load best score from player prefs
        SetScoreText(); //Adjust the score text
        SetBestScoreText(); //Adjust the best score text
        InvokeRepeating("AddScore", 0, addScoreRate); //Add to the score at a given interval
    }

    private void AddScore()
    {
        //Add to the score
        if (!GameObject.Find("Player").GetComponent<PlayerController>().speedBoostActive)
        {
            //If player is not speed boosted add 1 to score each iteration
            score++;
        }
        else
        {
            //If player is speed boosted adjust the score to add more
            score += addScoreRateSpeedBoosted;
        }
    }

    private void SetScoreText()
    {
        //Set the score text
        scoreText.text = "Score: " + score;
    }

    private void SetBestScoreText()
    {
        //Set best score text
        bestScoreText.text = "Best Score: " + bestScore;
    }

    void SaveBestScore()
    {
        //Save the best score
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    void LoadBestScore()
    {
        //Load the best score
        bestScore = PlayerPrefs.GetInt("BestScore");
    }

    private void Update()
    {
        //Continuously adjust score text
        SetScoreText();

        if (score > bestScore)
        {
            bestScore = score; //Reset best score if score is higher than current best score
            SaveBestScore(); //Save best score
        }

        SetBestScoreText(); //Continuous adjust best score text

        if (isGameOver)
        {
            CancelInvoke("AddScore"); //Stop adding score if player dies
        }

    }

    public void ShowGameOver()
    {
        //Show the game over screen if the player dies
        gameOverText.gameObject.SetActive(true);
    }
}
