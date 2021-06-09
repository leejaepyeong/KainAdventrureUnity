using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public ItemDB[] SomeDB;
    public InventoryData[] draginvenData;
    public Slot dragSlot;

    [SerializeField]
    private Image imageItem;

    public int invenType;
    public int getNum;
    public int setNum;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }



    public void Change()
    {
        int CountTemp = draginvenData[invenType].itemCount[getNum];
        int IdTemp = draginvenData[invenType].itemIDs[getNum];

        draginvenData[invenType].itemCount[getNum] = draginvenData[invenType].itemCount[setNum];
        draginvenData[invenType].itemIDs[getNum] = draginvenData[invenType].itemIDs[setNum];

        draginvenData[invenType].itemCount[setNum] = CountTemp;
        draginvenData[invenType].itemIDs[setNum] = IdTemp;


    }
}
