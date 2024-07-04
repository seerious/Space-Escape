using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    //AudioSources
    public AudioSource explosionSoundPre;


    //Booleans
    private bool speedIncreased = false;


    //GameObjects
    private GameObject player;


    //ParticleSystems
    public ParticleSystem explosionPrefab;


    //Scripts
    private GameManager manager;
    private SpawnManager spawn;
    private Upgrades upgradesScript;


    //Values
    private float breakOffPoint = 9f;
    private float destroyLimit = -25f;

    public float speed = 3f; //Referenced


    private void Awake()
    {
        //Import game objects
        player = GameObject.Find("Player");

        //Import scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        upgradesScript = GameObject.Find("GameManager").GetComponent<Upgrades>();
    }

    private void Update()
    {
        if (gameObject.tag == "enemy")
        {
            //Movement for enemy ship
            if (upgradesScript.stage4 && !speedIncreased)
            {
                //If player in stage 4 or higher move faster
                speedIncreased = true;
                this.speed *= 2;
            }
            if (manager.targetted && transform.position.x > -breakOffPoint)
            {
                //If player in stage 1 (targetted) move towards player until given break off point
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            else
            {
                //If player in stage 0 just move forward
                transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
            }
        }

        else
        {
            if (gameObject.tag == "asteroid")
            {
                //Asteroid movement
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            else if (gameObject.tag == "speed boost")
            {
                //Speed boost powerup movement
                transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
            }

            else if (gameObject.tag == "shield" || gameObject.tag == "planet" || gameObject.tag == "neb")
            {
                //Shield powerup, planet and nebula movement
                transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
            }
        }


        if (transform.position.x < destroyLimit)
        {
            //Remove item from spawn list and destroy if position X hits given limit
            spawn.spawnItems.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
