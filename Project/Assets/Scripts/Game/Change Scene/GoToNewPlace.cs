using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToNewPlace : MonoBehaviour
{

    public string newPlaceName = "New Scene name here";
    public string uuid;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Teleport(collision.gameObject.name);
    }

    private void Teleport(string name)
    {
        if (name == "Player")
        {
            playerController.nextUuid = uuid;
            SceneManager.LoadScene(newPlaceName);
        }
       
    }

}
