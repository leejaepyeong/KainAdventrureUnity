using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

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
    private void SetColor(float _alpha)
    {
        Color color = ItemImage.color;
        color.a = _alpha;   // ���İ�
        ItemImage.color = color;

    }

    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
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
        text_Count.text = _num.ToString();

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
                theItemEffectDatabase.UseItem(item);
                if (item.itemType == Item.ItemType.Used)
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
            ChangeSlot();
        }

    }

    void ChangeSlot()
    {
        Item _tempItem = item;
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
    }

    
    // ���콺�� ���Կ� ���� �ߵ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theItemEffectDatabase.ShowToolTip(item, transform.position);
    }

    // ���콺�� ���Կ� ���ö� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        theItemEffectDatabase.HideToolTip();
    }

}
