using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GlobalBehavior : MonoBehaviour {
    public static GlobalBehavior sTheGlobalBehavior = null;
    public LaserStatSystem mLaserStat = null;
	public asteroidSpawnSystem mAstSpawn= null;

	public GameObject[] pauseObjects;
	public GameObject[] gameUI;

    public Text mGameStateEcho = null;  // Defined in UnityEngine.UI
	public Text mShipHealth = null;
	public Text mPlanetHealth = null;
	public Text mGameOver = null;
	public Text mReset = null;
	private bool isOver = false;
	//public AudioSource audioSource=null;
	//public LaserStatSystem mLaserStat = null;
	public bool isPaused;


	#region World Bound support
	private Bounds mWorldBound;  // this is the world bound
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Camera mMainCamera;
    #endregion 

    // This is called before any Start()
    //     https://docs.unity3d.com/Manual/ExecutionOrder.html
    void Awake()
    {
        // just so we know
    }

    // Use this for initialization
    void Start () {
		Debug.Assert(mLaserStat != null);
		Debug.Assert(mAstSpawn != null);
		//audioSource = GetComponent<AudioSource>();
		GlobalBehavior.sTheGlobalBehavior = this;  // Singleton pattern

		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		gameUI = GameObject.FindGameObjectsWithTag("GameUI");
		hidePause();
		foreach (GameObject g in gameUI)
        {
			g.SetActive(SceneManager.GetActiveScene().name == "MainScene");
        }
		isPaused = !(SceneManager.GetActiveScene().name == "MainScene");

        #region world bound support
        mMainCamera = Camera.main; // This is the default main camera
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
		#endregion
		//Time.timeScale = 0;
    }

     //Update is called once per frame 
	void Update () {
		if (isOver == true)
        {
			if (Input.GetKey(KeyCode.R))
			{
				Time.timeScale = 1;
				isPaused = false;
				SceneManager.LoadScene("MainScene");
			}
		}
		else if (Input.GetKeyDown(KeyCode.P))
        {
			ManagePause();
        }

	} 

    #region Game Window World size bound support
    public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};
	
	/// <summary>
	/// This function must be called anytime the MainCamera is moved, or changed in size
	/// </summary>
	public void UpdateWorldWindowBound()
	{
		// get the main 
		if (null != mMainCamera) {
			float maxY = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			float sizeX = 2 * maxX;
			float sizeY = 2 * maxY;
			float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
			
			// Make sure z-component is always zero
			Vector3 c = mMainCamera.transform.position;
			c.z = 0.0f;
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

			mWorldCenter = new Vector2(c.x, c.y);
			mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
			mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
		}
	}

	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}
	
	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;

		if (mWorldBound.Intersects (objBound)) {
			if (objBound.max.x > mWorldBound.max.x)
				status = WorldBoundStatus.CollideRight;
			else if (objBound.min.x < mWorldBound.min.x)
				status = WorldBoundStatus.CollideLeft;
			else if (objBound.max.y > mWorldBound.max.y)
				status = WorldBoundStatus.CollideTop;
			else if (objBound.min.y < mWorldBound.min.y)
				status = WorldBoundStatus.CollideBottom;
			else if ((objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
				status = WorldBoundStatus.Outside;
		} else 
			status = WorldBoundStatus.Outside;

		return status;
	}

    public WorldBoundStatus ObjectClampToWorldBound(Transform t)
    {
        WorldBoundStatus status = WorldBoundStatus.Inside;
        Vector3 p = t.position;

        if (p.x > WorldMax.x)
        {
            status = WorldBoundStatus.CollideRight;
            p.x = WorldMax.x;
        }
        else if (t.position.x < WorldMin.x)
        {
            status = WorldBoundStatus.CollideLeft;
            p.x = WorldMin.x;
        }

        if (p.y > WorldMax.y)
        {
            status = WorldBoundStatus.CollideTop;
            p.y = WorldMax.y;
        }
        else if (p.y < WorldMin.y)
        {
            status = WorldBoundStatus.CollideBottom;
            p.y = WorldMin.y;
        }

        if ((p.z < mWorldBound.min.z) || (p.z > mWorldBound.max.z))
        {
            status = WorldBoundStatus.Outside;
        }

        t.position = p;
        return status;
    }
    #endregion

    public void UpdateScore(string msg)
    {
        mGameStateEcho.text = msg;
    }

	public void UpdateShipHealth(string msg)
	{
		mShipHealth.text = msg;
	}

	public void UpdatePlanetHealth(string msg)
	{
		mPlanetHealth.text = msg;
	}

	public void UpdateGameOver()
    {
		Time.timeScale = 0;
		mGameOver.text = "Game Over";
		mReset.text = "Press R to try again!";
		isOver = true;
		this.isPaused = true;
    }

	public void LoadLevel(string level)
    {
		foreach (GameObject g in gameUI)
        {
			g.SetActive(SceneManager.GetActiveScene().name == "MainScene");
        }
		Time.timeScale = 1;
		SceneManager.LoadScene(level);
    }

	public void ManagePause()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPause();
			this.isPaused = true;
		}
		else
		{
			Time.timeScale = 1;
			hidePause();
			this.isPaused = false;
		}
	}


	public void showPause()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}

	public void hidePause()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}
}
