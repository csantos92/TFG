using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPosition;
    public float cameraSpeed = 5;
    private Camera theCamera;
    private Vector3 minLimits, maxLimits;
    private float halfHeight, halfWidth;

    //Camera limits
    public void ChangeLimits(BoxCollider2D cameraLimits)
    {
        minLimits = cameraLimits.bounds.min;
        maxLimits = cameraLimits.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight / Screen.height * Screen.width;
    }

    //Follow player
    void Update()
    {
        float posX = Mathf.Clamp(this.target.transform.position.x, minLimits.x + halfWidth, maxLimits.x - halfWidth);
        float posY = Mathf.Clamp(this.target.transform.position.y, minLimits.y + halfHeight, maxLimits.y - halfHeight);

        targetPosition = new Vector3(posX, posY, this.transform.position.z);
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
