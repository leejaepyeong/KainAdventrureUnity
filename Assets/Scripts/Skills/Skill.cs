using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill/skill")]

public class Skill : ScriptableObject
{
    public enum Type { Attack, Buff, Heal };
    public Type type;

    public GameObject skillPrefab;
    public Image skillImage;

    public string skillName;

    public int mana;
    public float dataValue; // 효과 수치
    public float delayTime; // 지속시간

    [TextArea] public string skillData;
}
