using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hará la espada")]
    public int damage;
    private int random;
    public GameObject bloodAnim, canvasDamage;
    private GameObject hitPoint;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            int totalDamage = damage;

            //Calcula la suerte para el fallo del enemigo
            random = Random.Range(0, 100);

            //Calcula la suerte para el fallo 
            if (random < 5)
            {
                totalDamage = damage / 4;
            }
            else if (random >= 5 && random <= 10)
            {
                totalDamage = damage / 3;
            }
            else if (random > 10 && random < 20)
            {
                totalDamage = damage / 2;
            }

            if (bloodAnim != null && hitPoint != null)
            {
                Destroy(Instantiate(bloodAnim, hitPoint.transform.position, hitPoint.transform.rotation), 0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
        }
    }
}
