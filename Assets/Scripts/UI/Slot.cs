using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemDB[] SomeDB;
    public InventoryData[] invenData;


    public Item item;   // 획득 아이템 이미지
    public int itemCount;   //  획득한 아이템 갯수
    public Image ItemImage; // 아이템 이미지

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    private ItemEffectDataBase theItemEffectDatabase;


    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDataBase>();
    }

    //이미지 투명도 조절
    public void SetColor(float _alpha)
    {
        Color color = ItemImage.color;
        color.a = _alpha;   // 알파값
        ItemImage.color = color;

    }

    // 아이템 획득
    public void AddItem(Item _item, int _count)
    {
        item = _item;
        itemCount = _count;
        ItemImage.sprite = item.itemImage;  // sprite에 이미지 넣어줘야대서 .sprite추가

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

    // 아이템 갯수 조정
    public void SetSlotCount(int _num)
    {
        itemCount += _num;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화
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
        //이 객체에 마우스 우클릭하면 실행
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

    // 드래그 시작
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

    // 드래그 놓기
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // 드래그 놓기 해당 슬롯 위에
    public void OnDrop(PointerEventData eventData)
    {
        //빈 슬롯일때 호출 안되게
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
        Item _tempItem = item;  // 치환용
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        // 해당 슬롯에 아이템 이동

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

    
    // 마우스가 슬롯에 들어갈때 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            theItemEffectDatabase.ShowToolTip(item, transform.position);
    }

    // 마우스가 슬롯에 나올때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        theItemEffectDatabase.HideToolTip();
    }

}
