using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    //Positions
    private int resetPointX = -5000;

    private Vector3 previousPosition;  // The position of the backgrounds in the previous frame
    private Vector3 startingPos;


    //Transforms
    public Transform[] backgrounds;    // Array of all the backgrounds to be parallaxed


    //Values
    private float[] backgroundWidths;  // Array to store the width of each background
    public float scrollSpeed = 0.5f;   // Speed at which the background scrolls
    public float smoothing = 1f;       // How smooth the parallax effect should be


    void Start()
    {
        previousPosition = transform.position;
        startingPos = transform.position; //Set staring pos

        // Initialize the backgroundWidths array
        backgroundWidths = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Calculate the width of each background based on the sprite renderer's bounds
            backgroundWidths[i] = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    void Update()
    {
        // Calculate how much the background should move based on scrollSpeed
        Vector3 movement = new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);

        // For each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Move the background
            backgrounds[i].position += movement;

            // Check if the background is off-screen and reset its position
            if (backgrounds[i].position.x <= resetPointX)
            {
                backgrounds[i].position = startingPos;
            }
        }
    }
}