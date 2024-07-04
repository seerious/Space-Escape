using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    //Scripts
    private SpawnManager spawnManager;
    private PlayerController player;


    //Strings
    public string powerUpType;


    private void Awake()
    {
        //Import Scripts
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if it's the player colliding 
        if (other.gameObject.tag == "Player")
        {
            spawnManager.spawnItems.Remove(gameObject); //Remove powerup from spawn lis
            ApplyPowerEffect(player); //Apply effect to player
            Destroy(gameObject); //Destroy Powerup
        }
    }

    void ApplyPowerEffect(PlayerController player)
    {


        switch (powerUpType)
        {
            case "shield":
                player.invincible = true;
                player.playerShield.SetActive(true);
                break;
            case "speed boost":
                if (!player.speedBoostActive)
                {
                    player.EnableSpeedBoost();
                }
                break;
        }
    }
}
