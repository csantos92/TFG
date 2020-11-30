using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class UIManager : MonoBehaviour
{
    public int weaponNumber, numberOfKatanas;
    public bool bossDead, inventoryActive;
    public GameObject inventoryPanel, mainPanel, menuPanel, itemsPanel, questsPanel, statsPanel, gameOver, dialogue;
    public Slider playerHealthBar, playerHealthBar2;
    public HealthManager playerHealthManager;
    public Text playerHealthBarText;
    private Animator _animator;
    private WeaponManager weaponManager;
    private ItemsManager itemsManager;
    public Text inventoryText, swordName, swordDamage;
    public Image swordImage;
    public Button inventoryButton;

    //Set all components not visible
    private void Start()
    {
        numberOfKatanas = 1;
        weaponManager = FindObjectOfType<WeaponManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        mainPanel.SetActive(false);
        itemsPanel.SetActive(false);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        gameOver.SetActive(false);

        Button btn = inventoryButton.GetComponent<Button>();
        btn.onClick.AddListener(ToggleInventory);
    }

    //Open or close pause menu
    void Update()
    {
        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.currentHealth;

        playerHealthBar2.maxValue = playerHealthManager.maxHealth;
        playerHealthBar2.value = playerHealthManager.currentHealth;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.
            Append("HP: ").
            Append(playerHealthManager.currentHealth).
            Append(" / ").
            Append(playerHealthManager.maxHealth);

        playerHealthBarText.text = stringBuilder.ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        weaponNumber = weaponManager.activeWeapon;
        swordName.text = "" + weaponManager.GetWeaponAt(weaponNumber).weaponName;
        swordDamage.text = "Daño: " + weaponManager.GetWeaponAt(weaponNumber).damage;
        swordImage.sprite = weaponManager.GetWeaponAt(weaponNumber).GetComponent<SpriteRenderer>().sprite;

        if (inventoryPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
            _animator.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            _animator.enabled = true;
        }

        if (playerHealthManager.isDead)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }

        if (bossDead)
        {
            numberOfKatanas++;
            bossDead = false;
        }
    }

    //Set components related to inventory visible
    public void ToggleInventory()
    {
        if (!dialogue.activeInHierarchy)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
            mainPanel.SetActive(true);
            menuPanel.SetActive(true);
            itemsPanel.SetActive(false);
            questsPanel.SetActive(false);
            statsPanel.SetActive(false);
            inventoryText.text = "";

            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
        }
    }

    //Set components related to quests visible
    public void ToggleQuests()
    {
        questsPanel.SetActive(true);
        statsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        inventoryText.text = "Misiones";

        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
    }

    //Set components related to stats visible
    public void ToggleStats()
    {
        statsPanel.SetActive(true);
        questsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        inventoryText.text = "Estado";

        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);
    }

    public void ToggleItems()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        inventoryText.text = "Inventario";

        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

        if (inventoryPanel.activeInHierarchy)
        {
            foreach (Transform t in itemsPanel.transform)
            {
                Destroy(t.gameObject);
            }
            FillInventory();
        }
    }

    public void FillInventory()
    {
        List<GameObject> weapons = weaponManager.GetAllWeapons();
        for(int j = 0; j < numberOfKatanas ; j++)
        {
            AddItemToInventory(weapons[j], InventoryButton.ItemType.WEAPON, j);
        }

        int i = 0;
        List<GameObject> keyItems = itemsManager.GetQuestItems();
        foreach (GameObject item in keyItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos)
    {
        Button tempB = Instantiate(inventoryButton, itemsPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIdx = pos;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
}
