using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Upgrade", menuName ="User/Upgrade")]
public class ItemUpgradeData : ScriptableObject
{
    public string itemName;

    public Item[] MaterialItems;  //�ʿ� ��� ������

    public int[] MaterialItemCount; // �ʿ� ��� ����


}
