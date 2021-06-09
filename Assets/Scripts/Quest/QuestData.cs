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

    public string QuestName;    // ����Ʈ �̸�

    public int monsterId;   // �� ���̵�
    public int ResourseId; // �ڿ� ��� ID��
    public int Value;   // Ŭ�����ؾ� �ϴ� ��


    [TextArea]
    public string QuestDesc;    // ����Ʈ ����

    public float expReward;
    public int coinReward;
}
