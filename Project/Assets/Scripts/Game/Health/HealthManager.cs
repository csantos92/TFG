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
    public GameObject bloodAnim;
    private GameObject bloodPoint;
    //private QuestEnemy quest;
    //private QuestManager questManager;
    private ItemsManager itemsManager;

    private GoTo reset;

    // Start is called before the first frame update
    void Start()
    {
        reset = FindObjectOfType<GoTo>();
        _characterRenderer = GetComponent<SpriteRenderer>();
        itemsManager = GetComponent<ItemsManager>();

        //quest = GetComponent<QuestEnemy>();
        //questManager = FindObjectOfType<QuestManager>();
        currentHealth = maxHealth;
        flashActive = true;

        bloodPoint = transform.Find("Blood Point").gameObject;

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
                GetComponent<BoxCollider2D>().enabled = true;

            }
        }


    }

    public void DamageCharacter(int damage)
    {
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        
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

            Instantiate(bloodAnim, bloodPoint.transform.position, bloodPoint.transform.rotation);

            gameObject.SetActive(false);
        }

        if(flashLength > 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            flashActive = true;
            flashCounter = flashLength;
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
