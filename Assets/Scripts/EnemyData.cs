using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "New Enemy/enemy")]
public class EnemyData : ScriptableObject
{
    // �� Ÿ��
    public enum EnemyType
    {
        normal,
        Undead,
        Boss,
    }

    public string enemyName;

    public EnemyType enemyType;

    //�⺻ �������ͽ�
    public int hp;
    public int mp;
    public int damage;
    public int defense;
    public float delay;
    public float moveSpeed;

    public int hpRegen;
    public int mpRegen;
    public float regenTime;

    // �� ����
    [TextArea] public string enemyData;

    // �� ������
    public GameObject enemyPrefab;
}
