using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] eventObjs = GameObject.FindGameObjectsWithTag("event");
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("music");
        if ((this.gameObject.tag == "event") && (eventObjs.Length > 1))
        {
            Destroy(this.gameObject);
        }

        if ((this.gameObject.tag == "music") && (musicObjs.Length > 1))
        {
            Destroy(this.gameObject);
        }


        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
