using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticleLayer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 4;

    }
}

