using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger : MonoBehaviour
{
    public int questID;
    public bool startPoint, endPoint, automaticCatch;
    private bool playerInZone;
    private QuestManager questManager;
    public GameObject blockedPath, blockedPath2;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            if(automaticCatch || (!automaticCatch && Input.GetKeyDown(KeyCode.F)))
            {
                Quest q = questManager.QuestWithID(questID);

                if (q == null)
                {
                    Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                    return;
                }
                //Si llego aquí la misión existe
                if (!q.questCompleted) //Si se quita esta línea la misión será repetible
                {
                    //No he completado la misión actual
                    if (startPoint){
                        //Estoy en la zona donde empieza la misión
                        if (!q.gameObject.activeInHierarchy)
                        {
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    if(endPoint){
                        //Estoy en la zona donde acaba la misión
                        if (q.gameObject.activeInHierarchy)
                        {
                            q.CompleteQuest();

                            if(blockedPath != null && blockedPath2 != null)
                            {
                                blockedPath.SetActive(false);
                                blockedPath2.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}
