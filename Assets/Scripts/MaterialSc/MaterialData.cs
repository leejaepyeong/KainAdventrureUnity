using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "New Material/material")]
public class MaterialData : ScriptableObject
{
    public enum MaterialType
    {
        Tree,
        Mine
    }

    public enum MaterialValue
    {
        Normal,
        Uniq,
        Legend
    }

    public string materialName;

    public MaterialType materialType;
    public MaterialValue materialValue;

    //�⺻ �������ͽ�
    public int hp;

    // ��� ����
    [TextArea] public string materialData;

    // �� ������
    public GameObject materialPrefab;
}
