using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    private const float kLaserSpeed = 100;
    private float SpawnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.up * (kLaserSpeed * Time.smoothDeltaTime);

        // Figure out termination
        bool outside = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds) == GlobalBehavior.WorldBoundStatus.Outside;
        //bool timeToDie = (Time.realtimeSinceStartup - SpawnTime) > 1f;
        if (outside)
        {
            //GlobalBehavior.sTheGlobalBehavior.mLaserStat.IncScore(100);
            Destroy(gameObject);  // this.gameObject, this is destroying the game object
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //Debug.Log("collision");
            Destroy(gameObject);
        }
    }

}
