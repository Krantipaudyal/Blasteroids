using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserStatSystem : MonoBehaviour
{
    // UI Support
    public Scrollbar mLaserInterval = null;
    public RectTransform mLaserShotTime = null;
    private const float kInitLaserShotSize = 50f;
    public RectTransform laserOverheatMeter = null;



    // Spawning support
    private GameObject laser = null;
    // handle correct cool off time
    private float mSpawnLaserAt = 0f;

    private int laserOverheat = 0;
    private int counter = 0;

    // Score
    public int difficulty = 0;
    private int score = 0;

    private bool canUseAbility = true;

    void Start()
    {
        Debug.Assert(mLaserInterval != null);
        Debug.Assert(mLaserShotTime != null);
        //Debug.Assert(laserOverheatMeter != null);
        mLaserInterval.value = 0.2f; //Fire rate
        laser = Resources.Load<GameObject>("Prefabs/GLaser");

        //mSpawnEggAt = Time.realtimeSinceStartup - mEggInterval.value ; // assume one was shot
    }

    private void FixedUpdate()
    {
        counter++;
        if(counter >= 20)
        {
            laserOverheat--;
            counter = 0;
        }
    }

    void Update()
    {
        if ((score % 1000 == 0) && (score / 100 != difficulty) && score != 0 && difficulty < 160)
        {
            difficulty = score / 100;
            Debug.Log("New Difficulty: " + difficulty);
        }
        UpdateCoolDownUI();
    }

    public bool GetCanUseAbility()
    {
        return canUseAbility;
    }
    public void UseAbility()
    {
        canUseAbility = false;
    }

    public int GetScore()
    {
        return score;
    }

    #region Spawning support
    public bool CanSpawn()
    {
        if(TimeTillNext() <= 0f && laserOverheat < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
        //return TimeTillNext() <= 0f;
    }

    public float TimeTillNext()
    {
        float sinceLastLaser = Time.realtimeSinceStartup - mSpawnLaserAt;
        return mLaserInterval.value - sinceLastLaser;
    }

    public void SpawnLaser(Vector3 p, Vector3 dir)
    {
        //Debug.Assert(CanSpawn());
        laserOverheat++;
        GameObject e = GameObject.Instantiate(laser);// as GameObject;
        LaserBehavior Laser = e.GetComponent<LaserBehavior>(); // Shows how to get the script from GameObject
        if (null != Laser)
        {
            e.transform.position = p;
            e.transform.up = dir;
        }
        mSpawnLaserAt = Time.realtimeSinceStartup;
    }
    #endregion

    #region UI Support
    private void UpdateCoolDownUI()
    {
        float percentageT = TimeTillNext() / mLaserInterval.value;

        Vector2 s = mLaserShotTime.sizeDelta;  // This is the WidthxHeight [in pixel units]
        s.x = percentageT * kInitLaserShotSize;
        mLaserShotTime.sizeDelta = s;

    }
    #endregion

    // Count support

    private void EchoScore()
    {
        GlobalBehavior.sTheGlobalBehavior.UpdateScore("Score: " + score);
    }
    public void DecScore(int amount)
    {
        score -= amount;
        EchoScore();
    }
    public void IncScore(int amount)
    {
        score += amount;
        canUseAbility = true;
        EchoScore();
    }
}
