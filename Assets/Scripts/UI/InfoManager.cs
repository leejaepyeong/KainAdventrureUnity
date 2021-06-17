using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public static bool isInfoOn = false;   // 정보창 열림

    bool isMenuOn = false;
    bool isStatusOn = false;
    bool isMapOn = false;
    bool isQuestOn = false;

    //각 메뉴들 패널
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

    // 메뉴창 오픈
    public void MenuOpen()
    {

        isMenuOn = true;
        MenuPannel.SetActive(true);
        MenuBtn.SetActive(false);

    }

    // 메뉴창 닫기
    public void MenuClose()
    {

        isMenuOn = false;

        MenuPannel.SetActive(false);
        MenuBtn.SetActive(true);
    }

    // 스텟창 오픈
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

    // 스텟창 닫기
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

    // 미니맵 오픈
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

    // 미니맵 닫기
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

    // 퀘스트 창 오픈
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


    // 퀘스트 창 닫기
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

    // 단축키로 메뉴 창 열기
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
