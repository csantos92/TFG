using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth, currentHealth;
    public bool flashActive, isDead, isBoss;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;
    private SpriteRenderer[] sprites;
    public GameObject bloodAnim, bloodPoint;
    public UIManager _uiManager;

    // Start is called before the first frame update
    public void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        sprites = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<SpriteRenderer>();
        bloodPoint = transform.Find("Blood Point").gameObject;
        currentHealth = maxHealth;
        flashActive = true;
    }

    public void Update()
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
        currentHealth -= damage;

        if (currentHealth <= 0) //health
        {
            if (gameObject.tag.Equals("Enemy"))
            {
                if (isBoss)
                {
                    SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE2);
                    this.transform.gameObject.GetComponentInChildren<NPCDialogue>().blockPaths.SetActive(false);
                    _uiManager.bossDead = true;
                }
            }

            if (gameObject.name.Equals("Wolf"))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.WOLF2);
                isDead = true;
            }

            if (gameObject.name.Equals("Player"))
            {
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE);
                isDead = true;
            }

            Instantiate(bloodAnim, bloodPoint.transform.position, bloodPoint.transform.rotation);

            gameObject.SetActive(false);
        }

        if(flashLength > 0)
        {
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    public void ToggleColor(bool visible)
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
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.DIE);
            currentHealth += 30;
        }
    }
}
