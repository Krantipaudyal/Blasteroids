using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager: MonoBehaviour
{
    public GameObject[] pauseObjects;
    public GameObject[] gameUI;
    // Start is called before the first frame update
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        gameUI = GameObject.FindGameObjectsWithTag("GameUI");
        hidePause();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Time.timeScale = 0;
            foreach (GameObject g in gameUI)
            {
                g.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1;
            foreach (GameObject g in gameUI)
            {
                g.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                ManagePause();
            }
        }
    }

    public void PlayGame(string level)
    {
        if (level == "MainScene")
        {
            foreach (GameObject g in gameUI)
            {
                g.SetActive(true);
            }
        } else if (level == "MainMenu")
        {
            foreach(GameObject g in gameUI)
            {
                g.SetActive(false);
            }
        }
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

    public void ManagePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPause();
        } else
        {
            Time.timeScale = 1;
            hidePause();
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
