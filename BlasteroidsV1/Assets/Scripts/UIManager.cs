using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour
{
    //public GameObject[] pauseObjects;
    //public GameObject[] gameUI;
    //public AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        //pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        //gameUI = GameObject.FindGameObjectsWithTag("GameUI");
        //hidePause();
        Time.timeScale = 1;
        //audioSource = GetComponent<AudioSource>();

        //if (SceneManager.GetActiveScene().name == "MainMenu")
        //{
        //    //GlobalBehavior.sTheGlobalBehavior.isPaused = true;
        //    foreach (GameObject g in gameUI)
        //    {
        //        g.SetActive(false);
        //    }
        //}
        //else
        //{
        //    //GlobalBehavior.sTheGlobalBehavior.isPaused = false;
        //    foreach (GameObject g in gameUI)
        //    {
        //        g.SetActive(true);
        //    }
        //}

        //Time.timeScale = 0;
        //foreach (GameObject g in gameUI)
        //{
        //    g.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    if (SceneManager.GetActiveScene().name == "MainScene")
        //    {
        //        ManagePause();
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    audioSource.mute = !audioSource.mute;
        //}
    }

    public void PlayGame(string level)
    {
        //if (level == "MainScene")
        //{
        //    foreach (GameObject g in gameUI)
        //    {
        //        g.SetActive(true);
        //    }
        //    //GlobalBehavior.sTheGlobalBehavior.isPaused = false;
        //}
        //else if (level == "MainMenu")
        //{
        //    foreach(GameObject g in gameUI)
        //    {
        //        g.SetActive(false);
        //    }
        //}
        //Time.timeScale = 1;
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

    //public void ManagePause()
    //{
    //    if (Time.timeScale == 1)
    //    {
    //        Time.timeScale = 0;
    //        showPause();
    //        //GlobalBehavior.sTheGlobalBehavior.isPaused = true;
    //    }
    //    else
    //    {
    //        Time.timeScale = 1;
    //        hidePause();
    //        //GlobalBehavior.sTheGlobalBehavior.isPaused = false;
    //    }
    //}


    //public void showPause()
    //{
    //    foreach (GameObject g in pauseObjects)
    //    {
    //        g.SetActive(true);
    //    }
    //}

    //public void hidePause()
    //{
    //    foreach (GameObject g in pauseObjects)
    //    {
    //        g.SetActive(false);
    //    }
    //}
}
