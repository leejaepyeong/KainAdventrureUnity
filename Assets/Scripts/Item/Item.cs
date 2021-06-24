using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// New Item안의 item에 아이템 생성 

// 이스크립트는 아이템을 나타냅니다.
public class Item : ScriptableObject
{
    //아이템 이름
    public string itemName;
    [TextArea]  // 줄바꿈 가능하게
    public string itemDesc; // 아이템 설명
    public ItemType itemType;   // 아이템 타입 
    public Sprite itemImage;    // 아이템 이미지

    public GameObject itemPrefab; // 아이템 프리팹

    public string weaponType;   // 무기 종류 (검 활 아머)

    public int coinValue;   // 가격
    public int value;   // 수치(공격력 방어력)

    public int Upgrade; // 업그레이드 정도

    // 아이템 타입
    public enum ItemType
    {
        Equipment,  // 장비
        Ingredient, // 재료 및 포션
        Struct, // 구조물
        Coin    // 돈
    }

    // 장비 장착 타입
    public enum EquipType
    {
        None,
        Sword,
        Arrow,
        Armor
    }

    
    public EquipType equipType;

    // 포션 종류
    public enum UseType
    {
        None,
        Hp,
        Mp
    }

    public UseType useType;

    // 포션같은 사용 아이템 유무
    public bool isUsed = false;

    // 장비 장착 중인지
    public bool isEquip = false;
}
