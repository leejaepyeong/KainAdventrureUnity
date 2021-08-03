using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : MonoBehaviour
{
    public Skill skill;

    public PlayerStatusData playerData;


    private void Start()
    {
        skill.dataValue += playerData.skillLevel * 5;
    }

}
