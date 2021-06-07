using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    //public static Skill instance;

    public Skill skill;

    public bool isSkill = false;

    public Transform skillPos;

    //ÄÄÆ÷³ÍÆ®
    public PlayerControleer player;

    public void useSkill()
    {
        if(!isSkill)
        {
            isSkill = true;
            PlayerStatus.instance.currentMp -= skill.mana;

            switch (skill.type)
            {
                case Skill.Type.Attack:
                    StartCoroutine(AttackSkill());
                    break;
                case Skill.Type.Buff:
                    break;
                case Skill.Type.Heal:
                    break;
            }
        }
    }

    IEnumerator AttackSkill()
    {
        GameObject SkillObject = Instantiate(skill.skillPrefab, player.transform.position, player.transform.rotation);

        Rigidbody skillRigid = SkillObject.GetComponent<Rigidbody>();
        skillRigid.velocity = skillPos.forward * 4;

        Destroy(SkillObject, 2.1f);
        yield return new WaitForSeconds(skill.delayTime);
        isSkill = false;
    }

}
