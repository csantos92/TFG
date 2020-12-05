using UnityEngine;

public class GoToNewPlace : MonoBehaviour
{
    public string uuid;
    public static bool isPlayer;
    private PlayerController playerController;

    //Get player component
    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    //Telport player to a new scene (map or interior)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }

    public void Teleport(string name)
    {
        if (name == "Player")
        {
            playerController.nextUuid = uuid;

            isPlayer = true;
        }
    }
}
