using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroBehavior : MonoBehaviour {

    public EggStatSystem mEggStat = null;
    public float speed = 50f;
    public float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
    Vector2 mousePos;
    public Rigidbody2D rb2d;

    // Use this for initialization

    void Start () {
        Debug.Assert(mEggStat != null);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;

        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        

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
        ProcessEggSpwan();
    }

    private void UpdateMotion()
    {
       /* if (Input.GetKey(KeyCode.A))
        {
            transform.position+=;
            //pos += ((speed * Time.smoothDeltaTime) * transform.up);
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed -= 0.1f;
            //pos -= ((speed * Time.smoothDeltaTime) * transform.up);
        }*/
        /* mHeroSpeed += Input.GetAxis("Vertical");
         Vector3 pos = transform.position;
         transform.position += transform.up * (mHeroSpeed * Time.smoothDeltaTime);
         transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") *
                                     (kHeroRotateSpeed * Time.smoothDeltaTime));*/
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

    private void ProcessEggSpwan()
    {
        if (mEggStat.CanSpawn()) {
            if (Input.GetKey("space"))
                mEggStat.SpawnAnEgg(transform.position, transform.up);
        }
    }
}