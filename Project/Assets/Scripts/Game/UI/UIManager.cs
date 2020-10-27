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

    private void Start()
    {
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        mainPanel.SetActive(false);
        itemsPanel.SetActive(false);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        gameOver.SetActive(false);
        sortingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            ToggleInventory();

        }

    }

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

    public void ToggleQuests()
    {
        questsPanel.SetActive(true);
        statsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        sortingPanel.SetActive(false);
        inventoryText.text = "Misiones";
    }

    public void ToggleStats()
    {
        statsPanel.SetActive(true);
        questsPanel.SetActive(false);
        itemsPanel.SetActive(false);
        sortingPanel.SetActive(false);
        inventoryText.text = "Estado";
    }

    public void ToggleItems()
    {
        itemsPanel.SetActive(true);
        questsPanel.SetActive(false);
        statsPanel.SetActive(false);
        sortingPanel.SetActive(true);
        inventoryText.text = "Inventario";
    }

}
