using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //AudioSources
    public AudioSource explosionSound;


    //Booleans
    public bool invincible = false;
    public bool speedBoostActive = false;


    //GameObjects
    public GameObject playerShield;
    public GameObject speedBoost;


    //ParticleSystems
    public ParticleSystem explosionPre;


    //Scripts
    private GameManager manager;
    private SpawnManager spawn;


    //Values
    private int speedBoostDuration = 5;
    private int speedMultipler = 3;

    public float speed = 5.0f;


    private void Awake()
    {
        //Import Scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        //Player Movement
        float vertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(vertical * -1, 0) * speed * Time.deltaTime;

        transform.Translate(movement);
    }

    public void EnableSpeedBoost()
    {
        //Turn speed boost on
        speedBoost.SetActive(true);
        speedBoostActive = true;
        speed *= speedMultipler;
        StartCoroutine("DisableSpeedBoost");

        for (int i = 0; i < spawn.spawnItems.Count; i++)
        {
            spawn.spawnItems[i].GetComponent<ObstacleMovement>().speed *= speedMultipler;
        }
    }

    IEnumerator DisableSpeedBoost()
    {
        //Turn off speed boost after a given time
        yield return new WaitForSeconds(speedBoostDuration);
        speedBoostActive = false;
        speedBoost.SetActive(false);
        speed /= speedMultipler;

        for (int i = 0; i < spawn.spawnItems.Count; i++)
        {
            spawn.spawnItems[i].GetComponent<ObstacleMovement>().speed /= speedMultipler;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Detect collision with game ending obstacles
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "asteroid" || other.gameObject.tag == "laser")
        {
            if (invincible)
            {
                //If player is is invincible (has active shield) destroy obstacle and deactivate shield
                ParticleSystem explosion = Instantiate(explosionPre, other.gameObject.transform.position, Quaternion.identity);
                explosion.Play(); //Particle effect
                explosionSound.Play(); //Audio
                Destroy(other.gameObject);
                invincible = false; //Turn off invincibility
                playerShield.SetActive(false); //Turn off shield
                spawn.spawnItems.Remove(other.gameObject); //Remove destroyed object from spawn list
            }
            else
            {
                //If player is not invincible
                manager.targetted = false;
                manager.gameOver = true; //Game over state
                ParticleSystem explosion = Instantiate(explosionPre, gameObject.transform.position, Quaternion.identity);
                explosion.Play(); //Particle effect
                gameObject.SetActive(false); //Player disappears
            }
        }
    }
}
