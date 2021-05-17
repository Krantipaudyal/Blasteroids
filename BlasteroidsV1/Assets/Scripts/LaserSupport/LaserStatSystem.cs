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

    // Spawning support
    private GameObject laser = null;
    // handle correct cool off time
    private float mSpawnEggAt = 0f;

    // Score
    private int difficulty = 1;
    private int score = 0;

    void Start()
    {
        Debug.Assert(mLaserInterval != null);
        Debug.Assert(mLaserShotTime != null);
        mLaserInterval.value = 0.2f; //Fire rate
        laser = Resources.Load<GameObject>("Prefabs/GLaser");

        //mSpawnEggAt = Time.realtimeSinceStartup - mEggInterval.value ; // assume one was shot
    }

    void Update()
    {
        UpdateCoolDownUI();
    }

    #region Spawning support
    public bool CanSpawn()
    {
        return TimeTillNext() <= 0f;
    }

    public float TimeTillNext()
    {
        float sinceLastEgg = Time.realtimeSinceStartup - mSpawnEggAt;
        return mLaserInterval.value - sinceLastEgg;
    }

    public void SpawnAnEgg(Vector3 p, Vector3 dir)
    {
        Debug.Assert(CanSpawn());
        GameObject e = GameObject.Instantiate(laser);// as GameObject;
        LaserBehavior egg = e.GetComponent<LaserBehavior>(); // Shows how to get the script from GameObject
        if (null != egg)
        {
            e.transform.position = p;
            e.transform.up = dir;
        }
        mSpawnEggAt = Time.realtimeSinceStartup;
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
        GlobalBehavior.sTheGlobalBehavior.UpdateGameState("Score: " + score);
    }
    public void DecScore(int amount) 
    {
        score -= amount; 
        EchoScore(); 
    }
    public void IncScore(int amount) 
    { 
        score += amount; 
        EchoScore(); 
    }
}
