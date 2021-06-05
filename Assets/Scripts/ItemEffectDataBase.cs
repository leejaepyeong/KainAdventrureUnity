using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템 이름
    [Tooltip("HP, MP만 가능합니다")]
    public string[] part; //효과 부위
    public int[] num; // 효과 수치

};

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;   // 아이템 효과들

    //컴포넌트
    [SerializeField]
    private PlayerStatus thePlayerStatus;
    /*
    [SerializeField]
    private WeaponManager theWeaponManager;
    [SerializeField]
    private SlotToolTip theSlotToolTip;
    */
    private const string HP = "HP", MP = "MP";



    private void Start()
    {
        //theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
       // theSlotToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
       // theSlotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        /*
        if (_item.itemType == Item.ItemType.Equipment)
        {
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));

        }
        */

       if (_item.itemType == Item.ItemType.Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHp(itemEffects[x].num[y]);
                                break;
                            case MP:
                                thePlayerStatus.IncreaseMp(itemEffects[x].num[y]);
                                break;
                            default:
                                Debug.Log("잘못된 Status부위. HP, MP만 가능합니다");
                                break;
                        }
                        Debug.Log(_item.itemName + " 을 사용");
                    }
                    return;
                }
            }
            Debug.Log("일치하는 아이템 없다");
        }

    }



}

