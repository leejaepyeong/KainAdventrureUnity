using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillDamage : MonoBehaviour
{
    public enum SkillType {Skill1, Skill2, Skill3 }

    public SkillType skillType;

    public EnemyData enemyData;

    public int damage;

    private void Start()
    {
        switch(skillType)
        {
            case SkillType.Skill1:
                damage = enemyData.skillDamage1;
                break;
            case SkillType.Skill2:
                damage = enemyData.skillDamage2;
                break;
            case SkillType.Skill3:
                damage = enemyData.skillDamage3;
                break;
        }
    }

}
