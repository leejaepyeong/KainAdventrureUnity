using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DiceUnit", menuName ="DiceUnit/DiceUnit")]
public class DiceUnitData : ScriptableObject
{
    public string UnitName;

    public enum UnitType {MELEE, RANGE }
    public UnitType unitType;

    public int hp;
    public int damage;
    public int deffence;
    public float delay;
    public float range;
}
