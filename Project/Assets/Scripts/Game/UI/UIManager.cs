using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel, mainPanel, menuPanel, itemsPanel, questsPanel, statsPanel, gameOver, sortingPanel;


    public Slider playerHealthBar, playerHealthBar2;
    public HealthManager playerHealthManager;
    public Text playerHealthBarText;
    private Animator _animator;
    private WeaponManager weaponManager;
    private ItemsManager itemsManager;
    public int weaponNumber;

    public Text inventoryText, swordName, swordDamage;
    public Image swordImage;
    public Button inventoryButton;

    //Set all components not visible
    private void Start()
    {
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
        sortingPanel.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            ToggleInventory();
        }

        weaponNumber = weaponManager.activeWeapon;
        //swordName.text = "" + weaponManager.GetWeaponAt(weaponNumber).weaponName;
        //swordDamage.text = "Daño: " + weaponManager.GetWeaponAt(weaponNumber).damage;
        //swordImage.sprite = weaponManager.GetWeaponAt(weaponNumber).GetComponent<SpriteRenderer>().sprite;

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

    }

    //Set components related to inventory visible
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        mainPanel.SetActive(true);
        menuPanel.SetActive(true);
        itemsPanel.SetActive(false);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(false);
        inventoryText.text = "";
    }

    //Set components related to quests visible
    public void ToggleQuests()
    {
        questsPanel.SetActive(true);
        statsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        sortingPanel.SetActive(false);
        inventoryText.text = "Misiones";
    }

    //Set components related to stats visible
    public void ToggleStats()
    {
        statsPanel.SetActive(true);
        questsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        sortingPanel.SetActive(false);
        inventoryText.text = "Estado";
    }

    //Set components related to items visible
   /* public void ToggleItems()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(true);
        inventoryText.text = "Inventario";
    }*/

    public void ToggleItems()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(true);
        inventoryText.text = "Inventario";


        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);


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
        int i = 0;
        foreach (GameObject w in weapons)
        {
            AddItemToInventory(w, InventoryButton.ItemType.WEAPON, i);
            i++;
        }
        /*
        i = 0;
        List<GameObject> regularItems = itemsManager.GetItems();
        foreach (GameObject item in regularItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.ITEM, i);
            i++;
        }*/

        i = 0;
        List<GameObject> keyItems = itemsManager.GetQuestItems();
        foreach (GameObject item in keyItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos)
    {
        print(item);
        Button tempB = Instantiate(inventoryButton, itemsPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIdx = pos;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void ShowOnly(int type)
    {
        inventoryText.text = "";
        foreach (Transform t in itemsPanel.transform)
        {
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);
        }
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

    }

    public void ShowAll()
    {
        inventoryText.text = "";
        foreach (Transform t in itemsPanel.transform)
        {
            t.gameObject.SetActive(true);
        }
        //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.MENU);

    }

}
