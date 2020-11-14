using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAnimal : MonoBehaviour
{
    public GameObject bloodAnim, bloodPoint;

    private void Start()
    {
        bloodPoint = transform.Find("Blood Point").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Katana"))
        {
            Instantiate(bloodAnim, bloodPoint.transform.position, bloodPoint.transform.rotation);

            gameObject.SetActive(false);
        }
    }
}
