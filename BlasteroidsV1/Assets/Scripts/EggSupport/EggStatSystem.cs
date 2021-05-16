using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggStatSystem : MonoBehaviour
{
    // UI Support
    public Scrollbar mEggInterval = null;
    public RectTransform mEggShotTime = null;
    private const float kInitEggShotSize = 100f;

    // Spawning support
    private GameObject mEggSample = null;
    // handle correct cool off time
    private float mSpawnEggAt = 0f;

    // Count
    private int mEggCount = 0;

    void Start()
    {
        Debug.Assert(mEggInterval != null);
        Debug.Assert(mEggShotTime != null);
        mEggSample = Resources.Load<GameObject>("Prefabs/Egg");

        mSpawnEggAt = Time.realtimeSinceStartup - mEggInterval.value ; // assume one was shot
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
        return mEggInterval.value - sinceLastEgg;
    }

    public void SpawnAnEgg(Vector3 p, Vector3 dir)
    {
        Debug.Assert(CanSpawn());
        GameObject e = GameObject.Instantiate(mEggSample) as GameObject;
        EggBehavior egg = e.GetComponent<EggBehavior>(); // Shows how to get the script from GameObject
        if (null != egg)
        {
            e.transform.position = p;
            e.transform.up = dir;
        }
        IncEggCount();
        mSpawnEggAt = Time.realtimeSinceStartup;
        EchoEggCount();
    }
    #endregion

    #region UI Support
    private void UpdateCoolDownUI()
    {
        float percentageT = TimeTillNext() / mEggInterval.value;

        Vector2 s = mEggShotTime.sizeDelta;  // This is the WidthxHeight [in pixel units]
        s.x = percentageT * kInitEggShotSize;
        mEggShotTime.sizeDelta = s;

    }
    #endregion

    // Count support
    private void EchoEggCount() { GlobalBehavior.sTheGlobalBehavior.UpdateGameState("Egg Count: " + mEggCount); }
    public void DecEggCount() { mEggCount--; EchoEggCount(); }
    private void IncEggCount() { mEggCount++; EchoEggCount(); }
}
