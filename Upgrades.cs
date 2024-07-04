using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    //AudioSources
    private AudioSource enteringAsteroidField;
    private AudioSource laserUpgradeCompleteVoice;
    private AudioSource notification;
    private AudioSource openFire;
    private AudioSource sendEveryone;
    private AudioSource thrusters;


    //Scripts
    private GameManager manager;
    private UIManager score;


    //Stages and Trigger Scores
    public bool stage1 = false;
    private int stage1TriggerScore = 500;
    public bool stage2 = false;
    private int stage2TriggerScore = 2000;
    public bool stage3 = false;
    private int stage3TriggerScore = 2120;
    public bool stage4 = false;
    private int stage4TriggerScore = 10000;
    public bool stage5 = false;
    private int stage5TriggerScore = 50000;


    //Timers
    private int messageDelay = 1;


    void Awake()
    {
        //Import AudioSources
        enteringAsteroidField = GameObject.Find("EnterAsteroidField").GetComponent<AudioSource>();
        laserUpgradeCompleteVoice = GameObject.Find("UpgradeComplete").GetComponent<AudioSource>();
        notification = GameObject.Find("Notification").GetComponent<AudioSource>();
        openFire = GameObject.Find("OpenFire").GetComponent<AudioSource>();
        sendEveryone = GameObject.Find("SendEveryone").GetComponent<AudioSource>();
        thrusters = GameObject.Find("Thrusters").GetComponent<AudioSource>();

        //Import Scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        score = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        //Check if first score trigger is met
        if (score.score >= stage1TriggerScore && !stage1)
        {
            stage1 = true; //Trigger met
            StartCoroutine(UpdateNotice(laserUpgradeCompleteVoice)); //Audio
            manager.targetted = true; //Turn on targetted mode
        }

        //Check if second score trigger is met
        else if (score.score >= stage2TriggerScore && !stage2)
        {
            stage2 = true; //Trigger met
            StartCoroutine(UpdateNotice(enteringAsteroidField)); //Audio
            manager.asteroidField = true; //Turn on asteroid field
        }

        //Check if third score trigger is met
        else if (score.score >= stage3TriggerScore && !stage3)
        {
            stage3 = true; //Trigger met
            StartCoroutine(UpdateNotice(openFire)); //Audio
            manager.asteroidField = false; //Turn off asteroid field
            manager.lasersFixed = true; //Turn on enemy lasers
        }

        //Check if fourth score triggers is met
        else if (score.score >= stage4TriggerScore && !stage4)
        {
            stage4 = true; //Trigger met
            StartCoroutine(UpdateNotice(thrusters)); //Audio
        }

        else if (score.score >= stage5TriggerScore && !stage5)
        {
            stage5 = true; //Trigger Met
            StartCoroutine(UpdateNotice(sendEveryone)); //Audio
        }
    }

    IEnumerator UpdateNotice(AudioSource notice)
    {
        notification.Play();
        yield return new WaitForSeconds(messageDelay);
        notice.Play();
    }
}
