using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    private enum PlanetState
    {
        normalState,
        hitState,
        invincibleState,
        fireState,
        flBlueState,
        flWhiteState,
    };

    public int planetHealth = 4;
    private PlanetState pState = PlanetState.normalState;
    public float invTime = 1600f;
    public float hurtTime = 8f;
    private int stateFrameTick = 0;
    public LaserStatSystem mLaserStat = null;
    public float fireTime = 30f;
    public int shots = 0;
    public int shotsTot = 3;
    public int flashes = 0;


    private bool hasBeenFireState = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateFSM();
        transform.Rotate(Vector3.forward, -0.1f);
        // ProcessLaserSpwan();

        if (GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetScore() > 10000)
        {
            int newScore = GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetScore() - 10000;
            if (((newScore % 1500) == 0) &&
            newScore!= 0 &&
            GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetCanUseAbility())
            {
                if ((newScore % 3000) == 0)
                {
                    GlobalBehavior.sTheGlobalBehavior.mLaserStat.UseAbility();
                    pState = PlanetState.invincibleState;
                }
                else
                {
                    GlobalBehavior.sTheGlobalBehavior.mLaserStat.UseAbility();
                    pState = PlanetState.fireState;
                }

            }
        }
        else
        {
            if (((GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetScore() % 1000) == 0) &&
                GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetScore() != 0 &&
                GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetCanUseAbility())
            {
                if ((GlobalBehavior.sTheGlobalBehavior.mLaserStat.GetScore() % 2000) == 0)
                {
                    GlobalBehavior.sTheGlobalBehavior.mLaserStat.UseAbility();
                    pState = PlanetState.invincibleState;
                }
                else
                {
                    GlobalBehavior.sTheGlobalBehavior.mLaserStat.UseAbility();
                    pState = PlanetState.fireState;
                }

            }
        }

        //This is just to test the invincible state, delete later
        if (Input.GetKeyDown(KeyCode.I))
        {
            stateFrameTick = 0;
            pState = PlanetState.invincibleState;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            pState = PlanetState.normalState;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pState = PlanetState.fireState;
        }
    }


    //Deals with asteroid collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            //print("collided with" + collision.gameObject.name);
            Destroy(collision.gameObject);
            if (pState == PlanetState.normalState || pState == PlanetState.hitState)
            {
                planetHealth--;
                if (planetHealth == 0)
                {
                    Destroy(gameObject);
                    GlobalBehavior.sTheGlobalBehavior.UpdateGameOver();
                }
                planetHit();
                GlobalBehavior.sTheGlobalBehavior.UpdatePlanetHealth("Planet Health: " + planetHealth);
            }

        }
    }

    void planetHit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        pState = PlanetState.hitState;
        /*if (1 == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }*/
    }

    public void addPlanetHealth()
    {
        planetHealth++;
        GlobalBehavior.sTheGlobalBehavior.UpdatePlanetHealth("Planet Health: " + planetHealth);

    }

    private void updateFSM()
    {
        switch (pState)
        {
            case PlanetState.normalState:
                ServiceNormalState();
                break;
            case PlanetState.invincibleState:
                ServiceInvincibleState();
                break;
            case PlanetState.hitState:
                ServiceHitState();
                break;
            case PlanetState.fireState:
                ServiceFireState();
                break;
            case PlanetState.flWhiteState:
                ServiceflWhiteState();
                break;
            case PlanetState.flBlueState:
                ServiceflBlueState();
                break;
        }
    }

    private void ServiceNormalState()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void ServiceInvincibleState()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        if (stateFrameTick > invTime)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            stateFrameTick = 0;
            pState = PlanetState.flWhiteState;
        }
        else
        {
            stateFrameTick++;
        }
    }

    private void ServiceHitState()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (stateFrameTick > hurtTime)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            stateFrameTick = 0;
            pState = PlanetState.normalState;
        }
        else
        {
            stateFrameTick++;
        }
    }

    private void ServiceFireState()
    {
        if (stateFrameTick > fireTime)
        {
            if (shotsTot > shots)
            {
                stateFrameTick = 0;
                shots++;
                ProcessLaserSpwan();
            }
            else
            {
                ProcessLaserSpwan();
                stateFrameTick = 0;
                shots = 0;
                pState = PlanetState.normalState;
            }

        }
        else
        {
            stateFrameTick++;
        }
    }

    private void ServiceflBlueState()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        if (stateFrameTick > hurtTime)
        {
            stateFrameTick = 0;
            flashes++;
            if (flashes >= 28)
            {
                flashes = 0;
                pState = PlanetState.normalState;
            }
            else
            {
                pState = PlanetState.flWhiteState;
            }
        }
        else
        {
            stateFrameTick++;
        }
    }

    private void ServiceflWhiteState()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        if (stateFrameTick > hurtTime)
        {
            stateFrameTick = 0;
            flashes++;
            pState = PlanetState.flBlueState;
        }
        else
        {
            stateFrameTick++;
        }
    }

    private void ProcessLaserSpwan()
    {

        for (int i = -100; i <= 100; i += 5)
        {
            mLaserStat.SpawnLaser(new Vector3(i, -60), new Vector3(i, 300));
        }

    }
}
