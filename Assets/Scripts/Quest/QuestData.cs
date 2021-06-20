using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="quest",menuName ="Quest/quest")]
public class QuestData : ScriptableObject
{
    public bool isStart = false;
    public bool isClear = false;
    public bool isReward = false;

    public enum Type { Kill, Resource}

    public Type type;

    public enum ItemType {None, Resource, Struct}

    public ItemType itemType;

    public string QuestName;    // ?????? ????

    public int monsterId;   // ?? ??????
    public int ResourseId; // ???? ???? ID??
    public int Value;   // ?????????? ???? ??
    public int MaxValue;

    


    [TextArea]
    public string QuestDesc;    // ?????? ????

    public float expReward;
    public int coinReward;

    public bool cynematic;
}
