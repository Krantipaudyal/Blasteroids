using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidSpawnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject asteroid;
    public GameObject asteroid2;
    public GameObject asteroid3;
    private int difficulty = 0;
    public int spawnRate = 250;

    private int counter = 0;
    private int maxSpawned = 4;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GlobalBehavior.sTheGlobalBehavior.isPaused)
        {
            difficulty = GlobalBehavior.sTheGlobalBehavior.mLaserStat.difficulty;
            spawnRate = 250 - difficulty;
            //Debug.Log(counter);
            //counter++;
            if (counter < maxSpawned)
            {
                spawnAsteroid();
            }
        }
    }

    public void spawnAsteroid()
    {
        float x, y, z;
        Vector3 pos;
        if (Random.Range(0, spawnRate) == 0)
        {
            //Debug.Log("Asteroid1");
            x = Random.Range(-100, 100);
            y = 88f;
            z = 0f;
            pos = new Vector3(x, y, z);
            Instantiate(asteroid, pos, asteroid.transform.rotation);
            counter++;
        //    Debug.Log("UpCount: " + counter);
            // Instantiate(asteroid, pos, Quaternion.identity);
        }
        if (Random.Range(0, spawnRate) == spawnRate / 2)
        {
            //Debug.Log("Asteroid2");
            x = Random.Range(-100, 100);
            y = 88f;
            z = 0f;
            pos = new Vector3(x, y, z);
            Instantiate(asteroid2, pos, asteroid.transform.rotation);
            counter++;
          //  Debug.Log("UpCount: " + counter);
            // Instantiate(asteroid, pos, Quaternion.identity);
        }
        if (Random.Range(0, spawnRate) == spawnRate-1)
        {
            //Debug.Log("Asteroid3");
            x = Random.Range(-100, 100);
            y = 88f;
            z = 0f;
            pos = new Vector3(x, y, z);
            Instantiate(asteroid3, pos, asteroid.transform.rotation);
            counter++;
         //   Debug.Log("UpCount: " + counter);
            // Instantiate(asteroid, pos, Quaternion.identity);
        }
    }

    public void changeSpawn(int newSpawn)
    {     
        spawnRate = newSpawn;
    }
    public void lowerCounter()
    {
        counter--;
      //  Debug.Log("LowCount: "+counter);
    }
    public void incMaxSpawned(int i)
    {
        maxSpawned=maxSpawned+i;
        Debug.Log("maxSpawned: " + maxSpawned);
    }
}