using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNotAllowed : MonoBehaviour
{
    public bool playerInZone;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = true;
        }
    }

}
