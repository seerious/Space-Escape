using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //Prefabs
    public GameObject laserPrefab;


    //Scripts
    private GameManager manager;


    //Timers
    private int shotChanceTime = 1;


    //Values
    private int bulletOffset = 2;
    public int laserSpeed = 15;
    private int targetInt = 3;


    void Awake()
    {
        //Import scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //Give chance to shoot every given seconds
        InvokeRepeating("ShootLaser", 0, shotChanceTime);
    }


    void ShootLaser()
    {
        int randomInt = Random.Range(0, 3); //Generate random number
        if (randomInt == targetInt) //Check if random number equals target number, if so shoot
        {
            GameObject laser = Instantiate(laserPrefab,
            new Vector3(this.transform.position.x - bulletOffset, // x position - offset
            this.transform.position.y, // y position
             0), //z position
            Quaternion.identity); //Rotation

            laser.transform.eulerAngles = new Vector3(0, 0, 180); //Rotate to face right way **(Spawns wrong way)**
            laser.GetComponent<Rigidbody2D>().velocity = Vector2.left * laserSpeed; //Move laser
        }
    }


    void Update()
    {
        //Make it possible to shoot if in lasers fixed game state, (Impossible by default)
        if (manager.lasersFixed)
        {
            targetInt = 1;
        }
    }


}
