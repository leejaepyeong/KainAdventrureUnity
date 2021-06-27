using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="quest",menuName ="Info/quest")]
public class QuestData : ScriptableObject
{
    public bool isStart = false;
    public bool isClear = false;
    public bool isReward = false;

    public enum Type { Kill, Resource}

    public Type type;

    public enum ItemType {None, Resource, Struct}

    public ItemType itemType;

    public string QuestName;    // 퀘스트 이름

    public int monsterId;   // 몬스터 ID
    public int ResourseId; // 자원 ID
    public int Value;   // 현재 갯수
    public int MaxValue;    // 필요 갯수

    


    [TextArea]
    public string QuestDesc;    // 퀘스트 설명

    public float expReward; // 경험치 보상
    public int coinReward;  // 코인 보상

    public bool cynematic;  // 시네마틱 영상 (메인퀘스트용)
}
