using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    public LaserStatSystem mLaserStat = null;
    public float speed = 50f;
    public float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
    Vector2 mousePos;
    public Rigidbody2D rb2d;
    public int heroHealth=4;
    private bool isHurt = false;
    private int redCount = 10;
    private int counter = 0;

    // Use this for initialization

    void Start () {
        Debug.Assert(mLaserStat != null);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!GlobalBehavior.sTheGlobalBehavior.isPaused)
        {
            Vector3 pos = transform.position;

            var mouseScreenPos = Input.mousePosition;
            var startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            mouseScreenPos.x -= startingScreenPos.x;
            mouseScreenPos.y -= startingScreenPos.y;
            var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            if (isHurt == true)
            {
                isRed();
            }

            if (Input.GetKey(KeyCode.D) && pos.x <= 70)
            {
                pos.x += speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A) && pos.x >= -70)
            {
                pos.x -= speed * Time.deltaTime;
            }

            transform.position = pos;
            //UpdateMotion();
            BoundPosition();
            ProcessLaserSpwan();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            //print("collided with" + collision.gameObject.name);
            Destroy(collision.gameObject);
            GlobalBehavior.sTheGlobalBehavior.mAstSpawn.lowerCounter();
            heroHealth--;
            if (heroHealth==0)
            {
                Destroy(gameObject);
                GlobalBehavior.sTheGlobalBehavior.UpdateGameOver();
            }
            else
            {
                isHurt = true;
            }
            GlobalBehavior.sTheGlobalBehavior.UpdateShipHealth("Ship Health: " + heroHealth);
        }
    }

    private void isRed()
    {
        if (counter >= redCount)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            counter = 0;
            isHurt = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            counter++;
        }
        
    }

    private void BoundPosition()
    {
        GlobalBehavior.WorldBoundStatus status = GlobalBehavior.sTheGlobalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds);
        switch (status)
        {
            case GlobalBehavior.WorldBoundStatus.CollideBottom:
            case GlobalBehavior.WorldBoundStatus.CollideTop:
                transform.up = new Vector3(transform.up.x, -transform.up.y, 0.0f);
                break;
            case GlobalBehavior.WorldBoundStatus.CollideLeft:
            case GlobalBehavior.WorldBoundStatus.CollideRight:
                transform.up = new Vector3(-transform.up.x, transform.up.y, 0.0f);
                break;
        }
    }

    private void ProcessLaserSpwan()
    {
        if (mLaserStat.CanSpawn()) {
            if (Input.GetKey("space")|| Input.GetMouseButtonDown(0))
                mLaserStat.SpawnLaser(transform.position, transform.up);
        }
    }
}
