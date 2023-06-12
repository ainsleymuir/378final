using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    private int totalFruits = 0;
    private int collectedFruits = 0;
    public GameObject wall;

    private void Start()
    {
        // Find all fruit GameObjects in the scene
        GameObject[] apples = GameObject.FindGameObjectsWithTag("Apple");
        totalFruits += apples.Length;

        GameObject[] oranges = GameObject.FindGameObjectsWithTag("Orange");
        totalFruits += oranges.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple") || collision.CompareTag("Orange"))
        {
            // Increment the collected fruits count
            collectedFruits++;

            if (collectedFruits >= totalFruits)
            {
                Destroy(wall);
            }
        }
        Debug.Log("Collected Fruits: " + collectedFruits + ". Total Fruits: " + totalFruits);
    }
}

