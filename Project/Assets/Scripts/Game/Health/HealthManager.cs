using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool flashActive;
    public bool isDead;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;
    //private QuestEnemy quest;
    //private QuestManager questManager;
    //private ItemsManager itemsManager;

    private GoTo reset;

    // Start is called before the first frame update
    void Start()
    {
        reset = FindObjectOfType<GoTo>();
        _characterRenderer = GetComponent<SpriteRenderer>();
        //itemsManager = GetComponent<ItemsManager>();

        //quest = GetComponent<QuestEnemy>();
        //questManager = FindObjectOfType<QuestManager>();

        maxHealth = 100;
        currentHealth = maxHealth;
        flashActive = true;

        StartCoroutine(addHealth());

    }

    private void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter > flashLength * 0.66f)
            {
                ToggleColor(false);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                ToggleColor(true);
            }
            else if (flashCounter > 0)
            {
                ToggleColor(false);
            }
            else
            {
                ToggleColor(true);
                flashActive = false;
                //GetComponent<BoxCollider2D>().enabled = true;
                //GetComponent<PlayerController>().canMove = true;

            }
        }


    }

    public void DamageCharacter(int damage)
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        //Health -= damage;
        currentHealth -= damage;

        if (gameObject.name.Equals("Player"))
        {
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        }
        else if (gameObject.tag.Equals("Enemy"))
        {
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ATTACK);

        }

        if (currentHealth <= 0) //health
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                //questManager.enemyKilled = quest;
            }

            if (gameObject.name.Equals("Player"))
            {
                //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE);
                isDead = true;
            }

            gameObject.SetActive(false);
        }

        if(flashLength > 0)
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<PlayerController>().canMove = false;
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    IEnumerator addHealth()
    {
        while (true)
        { // loops forever...
            if (currentHealth < maxHealth)
            { // if health < 100...
                currentHealth += 1; // increase health and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                yield return null;
            }
        }
    }

    void ToggleColor(bool visible)
    {
        _characterRenderer.color = new Color(_characterRenderer.color.r,
                                                _characterRenderer.color.g,
                                                _characterRenderer.color.b,
                                                (visible ? 1 : 0));
    }

}
