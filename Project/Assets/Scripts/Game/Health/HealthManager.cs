using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth, currentHealth;
    public bool flashActive, isDead;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;
    private SpriteRenderer[] sprites;
    public GameObject bloodAnim, bloodPoint;

    // Start is called before the first frame update
    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        sprites = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<SpriteRenderer>();
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
                this.transform.gameObject.GetComponentInChildren<NPCDialogue>().blockPaths.SetActive(false);
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
            //GetComponent<BoxCollider2D>().enabled = false;
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
 
        foreach(SpriteRenderer s in sprites)
        {
            if (s.gameObject.activeInHierarchy)
            {
                s.color = new Color(s.color.r,
                                         s.color.g,
                                         s.color.b,
                                         (visible ? 1 : 0));
            }
        }
    }

    public void Heal()
    {
        if (gameObject.name.Equals("Player"))
        {
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE);
            currentHealth += 30;
        }
    }
}
