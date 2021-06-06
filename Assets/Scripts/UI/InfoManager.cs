using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    bool isMenuOn = false;
    bool isStatusOn = false;
    bool isMapOn = false;

    //필요 컴포넌트
    public GameObject MenuPannel;
    public GameObject MenuBtn;
    public GameObject StatusPannel;
    public GameObject MiniMapPannel;
    public GameObject InvenPannel;

    private void Update()
    {
        TryOpenInventory();
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
        isMenuOn = true;
        GameManager.instance.isInfoOn = true;

        StatusPannel.SetActive(true);
        MenuPannel.SetActive(false);

    }

    public void StatusClose()
    {
        isMenuOn = false;
        GameManager.instance.isInfoOn = false;

        StatusPannel.SetActive(false);
        MenuBtn.SetActive(true);
    }

    public void MapOpen()
    {
        isMapOn = true;
        GameManager.instance.isInfoOn = true;

        MiniMapPannel.SetActive(true);
        MenuPannel.SetActive(false);

    }

    public void MapClose()
    {
        isMapOn = false;
        GameManager.instance.isInfoOn = false;

        MiniMapPannel.SetActive(false);
        MenuBtn.SetActive(true);
    }


    void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.inventoryActivated = !Inventory.inventoryActivated;

            if (Inventory.inventoryActivated)
            {
                MenuBtn.SetActive(false);
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    public void OpenInventory()
    {
        Inventory.inventoryActivated = true;
        InvenPannel.SetActive(true);
        GameManager.instance.isInfoOn = true;
        MenuPannel.SetActive(false);

    }

    public void CloseInventory()
    {
        Inventory.inventoryActivated = false;
        InvenPannel.SetActive(false);
        GameManager.instance.isInfoOn = false;
        MenuBtn.SetActive(true);
    }
}
