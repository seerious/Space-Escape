using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Booleans
    private bool finalStage = false;
    public bool inField;


    //Lists
    public List<GameObject> spawnItems;



    //Prefabs
    public GameObject[] asteroidPrefabs;
    public GameObject[] nebSprites;
    public GameObject[] obstaclePrefabs;
    public GameObject[] planetSprites;
    public GameObject[] powerUpPrefabs;


    //Scripts
    private GameManager manager;
    private Upgrades upgradeScript;


    //Timers
    private float asteroidFieldSpawnRate = 0.5f;
    private int powerUpInterval = 10;
    private float spawnInterval = 2f;
    private float spawnMove = 1f;


    //Transforms
    public Transform spawnPos;


    //Values
    private int yBound = 9;


    void Awake()
    {
        //Import Scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        upgradeScript = GameObject.Find("GameManager").GetComponent<Upgrades>();
    }

    void Start()
    {
        //Spawn items and move the spawner
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval);
        InvokeRepeating("SpawnPowerUp", 5f, powerUpInterval);
        InvokeRepeating("MoveSpawn", 0, spawnMove);
        InvokeRepeating("SpawnPlanet", Random.Range(0, 20), Random.Range(20, 40));
        InvokeRepeating("SpawnNeb", Random.Range(0, 60), Random.Range(60, 120));
    }

    void SpawnObstacle()
    {
        //Spawn a random obstacle (ship or asteroid) from the prefab list
        int randomIndex = Random.Range(0, obstaclePrefabs.Length); //Get random index
        GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPos.position, Quaternion.identity); //Spawn item

        //Rotate to correct position
        if (obstacle.tag == "enemy")
        {
            obstacle.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        spawnListAdd(obstacle); //Add obstacle to spawn list

    }

    void SpawnPowerUp()
    {
        //Spawn a random power up
        int randomIndex = Random.Range(0, powerUpPrefabs.Length); //Get random index
        GameObject powerUp = Instantiate(powerUpPrefabs[randomIndex], spawnPos.position, Quaternion.identity); //Spawn powerup

        if (powerUp.tag == "speed boost") //Rotate
        {
            powerUp.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        spawnListAdd(powerUp); //Add to spawn list
    }

    void SpawnAsteroid()
    {
        //Spawn a random asteroid (Used only in asteroid field)
        int randomIndex = Random.Range(0, asteroidPrefabs.Length); //Get random index
        GameObject asteroid = Instantiate(asteroidPrefabs[randomIndex], spawnPos.position, Quaternion.identity); //Spawn
        spawnListAdd(asteroid); //Add asteroid to spawn list
    }


    void SpawnPlanet()
    {
        //Spawn random background planet
        int randomIndex = Random.Range(0, planetSprites.Length); // Get random index
        GameObject planet = Instantiate(planetSprites[randomIndex], spawnPos.position, Quaternion.identity);//Spawn
        planet.transform.localScale *= Random.Range(1, 4); //Resize randomly

        int hugePlanet = Random.Range(0, 10000); //1 in 10,000 chance of a huge planet spawning
        if (hugePlanet == 1)
        {
            planet.transform.localScale *= 6;
        }
        spawnListAdd(planet); // Add planet to spawn list
    }

    void SpawnNeb()
    {
        //Spawn a random background nebula
        int randomIndex = Random.Range(0, nebSprites.Length); //Get random index
        GameObject neb = Instantiate(nebSprites[randomIndex], spawnPos.position, Quaternion.identity); //Spawn
        neb.transform.localScale *= Random.Range(1, 7); //Resize randomly
        spawnListAdd(neb); //Add nebula to spawn list
    }

    void MoveSpawn()
    {
        //Move the spawner randomly to diffent Y locations in the bounds of the scene
        int randomY = Random.Range(-yBound, yBound); //Gt random y position within bounds
        transform.position = new Vector2(transform.position.x, randomY); //Move spawner
    }

    void Update()
    {
        //Update script plaer is in asteroid field
        if (manager.asteroidField && !inField)
        {
            inField = true;
            CancelInvoke("SpawnObstacles"); //Cancel spawning of all obstacles
            InvokeRepeating("SpawnAsteroid", 0f, asteroidFieldSpawnRate); //Spawn only asteroids
        }

        //Update script player has left asteroid field
        else if (!manager.asteroidField && inField)
        {
            CancelInvoke("SpawnAsteroid"); //Cancel asteroid only spawning and return to obstacles
            inField = false;
        }

        if (upgradeScript.stage5 && !finalStage)
        {
            //Update script player is in final stage
            finalStage = true;
            Multiply(); //Create double the amount of obstacles
        }
    }

    public void Multiply()
    {
        CancelInvoke("SpawnObstacle");
        InvokeRepeating("SpawnObstacles", 0f, 1f);
        spawnMove = 0.5f;
    }

    private void spawnListAdd(GameObject item)
    {
        spawnItems.Add(item);
    }


}
