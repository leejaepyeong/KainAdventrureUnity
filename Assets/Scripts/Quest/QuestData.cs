using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="quest",menuName ="Quest/quest")]
public class QuestData : ScriptableObject
{
    public bool isStart = false;
    public bool isClear = false;

    public enum Type { Kill, Resource}

    public Type type;

    public enum ItemType {None, Resource, Struct}

    public ItemType itemType;

    public string QuestName;    // 퀘스트 이름

    public int monsterId;   // 적 아이디
    public int ResourseId; // 자원 목록 ID값
    public int Value;   // 클리어해야 하는 양


    [TextArea]
    public string QuestDesc;    // 퀘스트 내용

    public float expReward;
    public int coinReward;
}
