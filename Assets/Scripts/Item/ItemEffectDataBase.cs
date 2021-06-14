using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // ?????? ????
    [Tooltip("HP, MP?? ??????????")]
    public string[] part; //???? ????
    public int[] num; // ???? ????

};

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;   // ?????? ??????

    //????????
    [SerializeField]
    private PlayerStatus thePlayerStatus;
    /*
    [SerializeField]
    private WeaponManager theWeaponManager;
    [SerializeField]
    private SlotToolTip theSlotToolTip;
    */
    private const string HP = "HP", MP = "MP";

    public GameObject dataPannel;
    public Text dataTitleTxt;
    public Text dataDescTxt;
    public Image dataImg;


    private void Start()
    {
        //theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    public void ShowToolTip(Item _item)
    {
        dataPannel.SetActive(true);
        dataTitleTxt.text = _item.itemName;
        dataDescTxt.text = _item.itemDesc;
        dataImg.sprite = _item.itemImage;
        
    }

    public void HideToolTip()
    {
        dataPannel.SetActive(false);
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
                                //thePlayerStatus.IncreaseHp(itemEffects[x].num[y]);
                                break;
                            case MP:
                                //thePlayerStatus.IncreaseMp(itemEffects[x].num[y]);
                                break;
                            default:
                                Debug.Log("?????? Status????. HP, MP?? ??????????");
                                break;
                        }
                        Debug.Log(_item.itemName + " ?? ????");
                    }
                    return;
                }
            }
            Debug.Log("???????? ?????? ????");
        }

    }



}

