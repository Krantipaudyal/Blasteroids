using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{

    private int hp = 4;


    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            //Debug.Log("LaserColl");
            hp--;
            //Destroy(collision.gameObject);
            //If collision issues arise, could destroy lasers here
            //instead of in laser script
        }
        if(hp <= 0)
        {
            Destroy(gameObject);
            GlobalBehavior.sTheGlobalBehavior.mLaserStat.IncScore(100);
        }
    }
}
