using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    //TextMeshPro
    private TextMeshProUGUI bestScore;

    void Start()
    {
        int best = PlayerPrefs.GetInt("BestScore"); //Load Best Score from player prefs
        bestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>(); //Import best score text
        bestScore.text = "Your Best Score: " + best; //Display best score
    }
}
