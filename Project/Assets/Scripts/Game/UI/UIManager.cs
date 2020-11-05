using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel, mainPanel, menuPanel, itemsPanel, questsPanel, statsPanel, gameOver, sortingPanel;
    public Text inventoryText;

    public Slider playerHealthBar, playerHealthBar2;
    public HealthManager playerHealthManager;
    public Text playerHealthBarText;
    private Animator _animator;

    //Set all components not visible
    private void Start()
    {
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

    }

    //Set components related to inventory visible
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        mainPanel.SetActive(true);
        menuPanel.SetActive(true);
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(true);
        inventoryText.text = "Inventario";
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
    public void ToggleItems()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(true);
        inventoryText.text = "Inventario";
    }

}
