using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public bool isInfoOn = false;   // ??? ?????  ??? ??

    bool isMenuOn = false;
    bool isStatusOn = false;
    bool isMapOn = false;
    bool isQuestOn = false;

    //???? ????????
    public GameObject MenuPannel;
    public GameObject MenuBtn;
    public GameObject StatusPannel;
    public GameObject MiniMapPannel;
    public GameObject InvenPannel;
    public GameObject QuestPannel;

    private void Update()
    {
        TryOpenMenu();
    }

    public void MenuOpen()
    {

        isMenuOn = true;
        MenuPannel.SetActive(true);
        MenuBtn.SetActive(false);

    }

    public void MenuClose()
    {

        isMenuOn = false;

        MenuPannel.SetActive(false);
        MenuBtn.SetActive(true);
    }

    public void StatusOpen()
    {
        if(!isInfoOn)
        {
            isInfoOn = true;

            isStatusOn = true;
            GameManager.instance.isInfoOn = true;

            StatusPannel.SetActive(true);
            MenuPannel.SetActive(false);

        }

    }

    public void StatusClose()
    {
        if(isInfoOn && isStatusOn)
        {
            isInfoOn = false;

            isStatusOn = false;
            GameManager.instance.isInfoOn = false;

            StatusPannel.SetActive(false);
            MenuBtn.SetActive(true);
        }

        
    }

    public void MapOpen()
    {
        if(!isInfoOn)
        {
            isInfoOn = true;

            isMapOn = true;
            GameManager.instance.isInfoOn = true;

            MiniMapPannel.SetActive(true);
            MenuPannel.SetActive(false);
        }
       

    }

    public void MapClose()
    {
        if(isInfoOn && isMapOn)
        {
            isInfoOn = false;

            isMapOn = false;
            GameManager.instance.isInfoOn = false;

            MiniMapPannel.SetActive(false);
            MenuBtn.SetActive(true);
        }
        
    }

    public void QuestOpen()
    {
        if (!isInfoOn)
        {
            isInfoOn = true;

            isQuestOn = true;
            GameManager.instance.isInfoOn = true;

            QuestPannel.SetActive(true);
            MenuPannel.SetActive(false);
        }


    }

    public void QuestClose()
    {
        if (isInfoOn && isQuestOn)
        {
            isInfoOn = false;

            isQuestOn = false;
            GameManager.instance.isInfoOn = false;

            QuestPannel.SetActive(false);
            MenuBtn.SetActive(true);
        }

    }


    void TryOpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (!Inventory.inventoryActivated)
            {
                MenuBtn.SetActive(false);
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMapOn)
            {
                MenuBtn.SetActive(false);
                MapOpen();
            }
            else
            {
                MapClose();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isStatusOn)
            {
                MenuBtn.SetActive(false);
                StatusOpen();
            }
            else
            {
                StatusClose();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isQuestOn)
            {
                MenuBtn.SetActive(false);
                QuestOpen();
            }
            else
            {
                QuestClose();
            }
        }
    }

    public void OpenInventory()
    {
        if(!isInfoOn)
        {
            isInfoOn = true;

            Inventory.inventoryActivated = true;
            InvenPannel.SetActive(true);
            GameManager.instance.isInfoOn = true;
            MenuPannel.SetActive(false);
        }
        

    }

    public void CloseInventory()
    {
        if(isInfoOn && Inventory.inventoryActivated)
        {
            isInfoOn = false;

            Inventory.inventoryActivated = false;
            InvenPannel.SetActive(false);
            GameManager.instance.isInfoOn = false;
            MenuBtn.SetActive(true);
        }

        
    }
}
