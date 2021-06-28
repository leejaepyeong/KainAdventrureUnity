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
    bool isOptionOn = false;
    bool isHelpOn = false;

    //각 메뉴들 패널
    public GameObject MenuPannel;   // 메뉴창
    public GameObject MenuBtn;  // 메뉴 버튼
    public GameObject StatusPannel; // 스탯 창
    public GameObject MiniMapPannel;    // 미니맵 창
    public GameObject InvenPannel;  // 인벤토리 창
    public GameObject QuestPannel;  // 퀘스트 창
    public GameObject OptionPannel; // 옵션 창
    public GameObject helpPannel;   // 도움말 창

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

        if(Input.GetKeyDown(KeyCode.F10))
        {
            if (!isOptionOn)
            {
                MenuBtn.SetActive(false);
                OptionOpen();
            }
            else
            {
                Debug.Log("Close");
                OptionClose();
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

    public void OptionOpen()
    {
        if (!isInfoOn)
        {
            isInfoOn = true;

            isOptionOn = true;
            GameManager.instance.isInfoOn = true;

            OptionPannel.SetActive(true);
            MenuPannel.SetActive(false);

        }

    }

    // 스텟창 닫기
    public void OptionClose()
    {
        if (isInfoOn && isOptionOn)
        {
            isInfoOn = false;

            isOptionOn = false;
            GameManager.instance.isInfoOn = false;

            OptionPannel.SetActive(false);
            MenuBtn.SetActive(true);
        }


    }

    public void HelpOpen()
    {
        isHelpOn = true;
        isOptionOn = false;
        OptionPannel.SetActive(false);

        helpPannel.SetActive(true);

    }
    
    public void HelpClose()
    {
        if (isInfoOn && isHelpOn)
        {
            isInfoOn = false;
            isHelpOn = false;
            GameManager.instance.isInfoOn = false;

            helpPannel.SetActive(false);
            MenuBtn.SetActive(true);
        }
    }
}
