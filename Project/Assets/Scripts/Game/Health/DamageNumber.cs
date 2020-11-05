using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{

    public float damageSpeed;
    public float damagePoints;

    public Text damageText;

    public Vector2 direction = new Vector2(1, 2);
    public float timeToChangeDirection;
    public float timeToChangeDirectionCounter;

    // Update is called once per frame
    void Update()
    {
        timeToChangeDirectionCounter -= Time.deltaTime;
        if(timeToChangeDirectionCounter < timeToChangeDirection/2)
        {
            direction *= -1;
            timeToChangeDirectionCounter += timeToChangeDirection;
        }


        damageText.text = "" + damagePoints;
        this.transform.position = new Vector3(
            this.transform.position.x + direction.x * damageSpeed * Time.deltaTime, 
            this.transform.position.y + damageSpeed * Time.deltaTime,
            this.transform.position.z);

        this.transform.localScale = this.transform.localScale * (1 - Time.deltaTime/3);

    }

}
