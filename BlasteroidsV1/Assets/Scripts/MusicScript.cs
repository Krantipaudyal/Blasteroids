using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }
     /*   if (Input.GetKeyDown(KeyCode.M))
        {
            if (songNum == 1)
            {
                songNum == 2;
                    audioSource1.mute = !audioSource1.mute;
                    audioSource2.mute = !audioSource2.mute;
            }
        }*/
    }
}
