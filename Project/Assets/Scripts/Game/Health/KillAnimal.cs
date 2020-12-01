using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAnimal : MonoBehaviour
{
    public GameObject bloodAnim, bloodPoint;

    public void Start()
    {
        bloodPoint = transform.Find("Blood Point").gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Katana"))
        {
            Instantiate(bloodAnim, bloodPoint.transform.position, bloodPoint.transform.rotation);
            gameObject.SetActive(false);

            if (transform.gameObject.tag.Equals("Chicken"))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.BLOOD);
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.CHICKEN);
            }
            else if (transform.gameObject.tag.Equals("Butterfly"))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.BLOOD);
            }
        }
    }
}
