using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{

    private int hp = 4;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private void Update()
    {
        if (transform.position.y <= -88)
        {
            Destroy(gameObject);
            GlobalBehavior.sTheGlobalBehavior.mAstSpawn.lowerCounter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            //Debug.Log("LaserColl");
            hp--;
            if (hp == 3)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;

            }
            else if (hp == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;

            }
            else if (hp == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite3;

            }
            //Destroy(collision.gameObject);
            //If collision issues arise, could destroy lasers here
            //instead of in laser script
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
            GlobalBehavior.sTheGlobalBehavior.mLaserStat.IncScore(100);
            GlobalBehavior.sTheGlobalBehavior.mAstSpawn.lowerCounter();
        }
    }
}

