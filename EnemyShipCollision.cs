using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipCollision : MonoBehaviour
{
    //AudioSources
    public AudioSource explosionSoundPre;


    //ParticleSystems
    public ParticleSystem explosionPrefab;


    //Scripts
    private GameManager manager;
    private SpawnManager spawn;
    private UIManager uiManagerScript;


    //Timers
    private int destroyTimeLimit = 3;


    //Values
    public int scoreValue = 25;

    void Awake()
    {
        //Import Scripts
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManagerScript = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    //Increase score when an enemy ship is destroyed or evaded
    void OnDestroy()
    {
        if (!manager.gameOver) // Check it's not game over
        {
            if (!manager.asteroidField) //Check player is not in the asteroid field
            {
                uiManagerScript.score += scoreValue; //Increase score by ship's score value
            }
        }
    }

    //Check where the ship involved in the collision is located and spawn an explosion at that location
    //Play explosion audio
    //Remove ship from the spaqn list
    //Destroy all ships involved
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.tag == "enemy" && other.gameObject.tag == "asteroid" ||
        gameObject.tag == "asteroid" && other.gameObject.tag == "enemy" ||
        gameObject.tag == "enemy" && other.gameObject.tag == "enemy")
        {
            if (gameObject.tag == "enemy")
            {
                SpawnExplosion(gameObject.transform);
                SpawnAudio(gameObject.transform);
                SpawnListRemove(gameObject);
                Destroy(gameObject);
            }


            else if (gameObject.tag == "asteroid" && other.gameObject.tag == "enemy")
            {
                SpawnExplosion(other.gameObject.transform);
                SpawnAudio(other.gameObject.transform);
                SpawnListRemove(other.gameObject);
                Destroy(other.gameObject);
            }

            else
            {
                SpawnExplosion(other.gameObject.transform);
                SpawnExplosion(gameObject.transform);
                SpawnAudio(other.gameObject.transform);
                SpawnAudio(gameObject.transform);
                SpawnListRemove(other.gameObject);
                SpawnListRemove(gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    //Spawn,scale and play explosion particle based on ship's location and size
    private void SpawnExplosion(Transform target)
    {
        //Spawn explosion based on ship's location
        ParticleSystem explosion = Instantiate(explosionPrefab, target.transform.position, Quaternion.identity);
        //Scale explosion based on ship's size
        explosion.transform.localScale = transform.localScale;
        //Play particle 
        explosion.Play();
        //Delete particle
        StartCoroutine(DestroySpawn(explosion.gameObject));
    }

    //Spawn and play explosion audio
    private void SpawnAudio(Transform target)
    {
        //Spawn explosion audio
        AudioSource explosionSound = Instantiate(explosionSoundPre, target.transform.position, Quaternion.identity);
        //Play Audio
        explosionSound.Play();
        //Delete AudioSource
        StartCoroutine(DestroySpawn(explosionSound.gameObject));
    }

    //Delete desired gameobject after a give time
    IEnumerator DestroySpawn(GameObject target)
    {
        yield return new WaitForSeconds(destroyTimeLimit);
        Destroy(target);
    }

    //Remove given gameobject from the spawn list
    private void SpawnListRemove(GameObject target)
    {
        spawn.spawnItems.Remove(target);
    }
}
