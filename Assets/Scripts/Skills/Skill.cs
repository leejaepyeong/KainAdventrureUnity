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
    public Sprite skillImage;

    public string skillName;

    public int mana;
    public float dataValue; // ????
    public float delayTime; // ????

    [TextArea] public string skillData;
}
