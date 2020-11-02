using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraLimits : MonoBehaviour
{
    //Sets the limits of the camera using a box collider
    void Start()
    {
        FindObjectOfType<CameraFollow>().ChangeLimits(this.GetComponent<BoxCollider2D>());
    }

}
