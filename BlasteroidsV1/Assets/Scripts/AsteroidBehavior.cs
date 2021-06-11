using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{

    private int hp = 3;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public float rotateRate = 0;
    private int spin = 0;


    private void Update()
    {
        if (!GlobalBehavior.sTheGlobalBehavior.isPaused)
        {
            if (transform.position.x % 5 == 1 || transform.position.x * -1 % 5 == 1)
            {
                rotateRate = 0.1f;
            }
            else if (transform.position.x % 5 == 2 || transform.position.x * -1 % 5 == 2)
            {
                rotateRate = -0.1f;
            }
            else if (transform.position.x % 5 == 3 || transform.position.x * -1 % 5 == 3)
            {
                rotateRate = 0.2f;
            }
            else if (transform.position.x % 5 == 4 || transform.position.x * -1 % 5 == 4)
            {
                rotateRate = -0.2f;
            }
            transform.Rotate(Vector3.forward, rotateRate);
            if (transform.position.y <= -85)
            {
                Destroy(gameObject);
                GlobalBehavior.sTheGlobalBehavior.mAstSpawn.lowerCounter();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            //Debug.Log("LaserColl");
            hp--;
            if (hp == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;

            }
           /* else if (hp == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;

            }*/
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

