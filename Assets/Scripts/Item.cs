using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// 새로운 메뉴창 형성
//create로 바로 새 오브젝트형성

// scrip이 필요없는 오브젝트
public class Item : ScriptableObject
{
    //아이템 이름 . 이미지
    public string itemName;
    [TextArea]  // 줄바꿈도 가능하게
    public string itemDesc; //아이템의 설명
    public ItemType itemType;
    public Sprite itemImage;

    public GameObject itemPrefab; // 아이템의 프리팹

    public string weaponType;   // 무기 타입
    public enum ItemType
    {
        Equipment,
        Ingredient,
        Used
    }

}
