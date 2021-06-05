using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
// ���ο� �޴�â ����
//create�� �ٷ� �� ������Ʈ����

// scrip�� �ʿ���� ������Ʈ
public class Item : ScriptableObject
{
    //������ �̸� . �̹���
    public string itemName;
    [TextArea]  // �ٹٲ޵� �����ϰ�
    public string itemDesc; //�������� ����
    public ItemType itemType;
    public Sprite itemImage;

    public GameObject itemPrefab; // �������� ������

    public string weaponType;   // ���� Ÿ��
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

}
