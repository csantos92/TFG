using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Don't destroy elements when changing to another scene
    void Start()
    {

        if (!PlayerController.playerCreated)
        {       
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
