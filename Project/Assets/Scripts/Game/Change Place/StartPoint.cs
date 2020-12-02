using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string uuid;
    private PlayerController player;
    private CameraFollow theCamera;
    public Vector2 facingDirection = Vector2.zero;

    //Gets the position where the player must start when changing to another scene
    public void Start()
    {
        player = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraFollow>();
    }

    public void Update()
    {
        if (GoToNewPlace.isPlayer && player.nextUuid.Equals(uuid))
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.KNOCK);
            GoToNewPlace.isPlayer = false;
            ToNextPlace();
        }
    }

    public void ToNextPlace()
    {
        player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);

        player.lastMovement = facingDirection;
    }
}
