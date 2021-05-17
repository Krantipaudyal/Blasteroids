using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private const float kLaserSpeed = 80f;
    private float SpawnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kLaserSpeed * Time.smoothDeltaTime);

        // Figure out termination
        bool outside = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds) == GlobalBehavior.WorldBoundStatus.Outside;
        //bool timeToDie = (Time.realtimeSinceStartup - SpawnTime) > 1f;
        if (outside)
        {
            GlobalBehavior.sTheGlobalBehavior.mLaserStat.IncScore(100);
            Destroy(gameObject);  // this.gameObject, this is destroying the game object
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Egg OnTriggerEnter");
        // Collision with hero (especially when first spawned) does not count
        //Will add more for different asteroids since they will be worth different amounts of points
        if (collision.gameObject.name != "Asteroid")
        {
            Destroy(this);
            GlobalBehavior.sTheGlobalBehavior.mLaserStat.IncScore(100);
        }
    }

}
