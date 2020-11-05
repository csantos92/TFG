using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public GameObject canvasDamage;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            int totalDamage = damage;
            totalDamage = Mathf.Clamp(totalDamage, 1, 9999);

            //Calcula la suerte para el fallo del enemigo
            if(Random.Range(0, 100) < 20)
            {
                totalDamage = 0;
            }

            var clone = (GameObject)Instantiate(canvasDamage, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);

        }
    }
}
