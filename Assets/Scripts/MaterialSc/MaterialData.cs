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

    //기본 스테이터스
    public int hp;

    // 재료 설명
    [TextArea] public string materialData;

    // 몹 프리팹
    public GameObject materialPrefab;
}
