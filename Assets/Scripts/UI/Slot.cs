using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemDB[] SomeDB;
    public InventoryData[] invenData;


    public Item item;   // ȹ�� ������ �̹���
    public int itemCount;   //  ȹ���� ������ ����
    public Image ItemImage; // ������ �̹���

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private ItemEffectDataBase theItemEffectDatabase;


    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDataBase>();
    }

    //�̹��� ���� ����
    public void SetColor(float _alpha)
    {
        Color color = ItemImage.color;
        color.a = _alpha;   // ���İ�
        ItemImage.color = color;

    }

    // ������ ȹ��
    public void AddItem(Item _item, int _count)
    {
        item = _item;
        itemCount = _count;
        ItemImage.sprite = item.itemImage;  // sprite�� �̹��� �־���ߴ뼭 .sprite�߰�

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }


        SetColor(1);
    }

    // ������ ���� ����
    public void SetSlotCount(int _num)
    {
        itemCount += _num;
        text_Count.text = itemCount.ToString();

        int itemId = SomeDB[(int)item.itemType].GetIDFrom(item);


        for (int i = 0; i < invenData[(int)item.itemType].itemCount.Length; i++)
        {
            if(invenData[(int)item.itemType].itemIDs[i] == itemId)
            {
                invenData[(int)item.itemType].itemCount[i]--;
                
                if(invenData[(int)item.itemType].itemCount[i] <= 0)
                {
                    invenData[(int)item.itemType].itemIDs[i] = 0;
                }
            }
        }

        if (itemCount <= 0)
            ClearSlot();
    }

    // ���� �ʱ�ȭ
    void ClearSlot()
    {
        item = null;
        itemCount = 0;
        ItemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�� ��ü�� ���콺 ��Ŭ���ϸ� ����
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                int itemId = SomeDB[(int)item.itemType].GetIDFrom(item);

                theItemEffectDatabase.UseItem(item, itemId);
                if (item.isUsed)
                {
                    SetSlotCount(-1);
                }
            }
        }
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(ItemImage);

            DragSlot.instance.transform.position = eventData.position;

            //DragSlot.instance.draginvenData[(int)item.itemType].itemCount.Length
            for (int i = 0; i < invenData[(int)item.itemType].itemCount.Length; i++)
            {
                if(item == SomeDB[(int)item.itemType].items[invenData[(int)item.itemType].itemIDs[i]])
                {
                    DragSlot.instance.getNum = i;
                }
            }

        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // �巡�� ���� �ش� ���� ����
    public void OnDrop(PointerEventData eventData)
    {
        //�� �����϶� ȣ�� �ȵǰ�
        if (DragSlot.instance.dragSlot != null)
        {
            for (int i = 0; i < invenData[(int)item.itemType].itemCount.Length; i++)
            {
                if (item == SomeDB[(int)item.itemType].items[invenData[(int)item.itemType].itemIDs[i]])
                {
                    DragSlot.instance.setNum = i;
                    DragSlot.instance.invenType = (int)item.itemType;
                }
            }

            ChangeSlot();
        }

    }

    void ChangeSlot()
    {
        Item _tempItem = item;  // ġȯ��
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        // �ش� ���Կ� ������ �̵�

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }

        DragSlot.instance.Change();
    }

    
    // ���콺�� ���Կ� ���� �ߵ�
    //������ ������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theItemEffectDatabase.ShowToolTip(item);
    }

    // ���콺�� ���Կ� ���ö� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        theItemEffectDatabase.HideToolTip();
    }

}
