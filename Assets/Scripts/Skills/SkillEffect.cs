using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    //public static Skill instance;

    public Skill[] skill;

    public int[] isSkill = { 0 };   // 1~3 ?? ??(?? / ?? / ?)

    public Transform skillPos;

    public GameObject buffSkillObject;

    private int currentSkillNum;


    //????????
    public PlayerControleer player;


    private void Update()
    {
        SkillPos();
    }

    void SkillPos()
    {
        if(isSkill[1] == 1)
        {
            buffSkillObject.transform.position = player.transform.position;
        }
    }

    public void useSkill(int _num)
    {
        if(isSkill[_num] == 0)
        {
            if (PlayerStatus.instance.currentMp < skill[_num].mana)
            {
                return;
            }

            isSkill[_num] = 1;
            PlayerStatus.instance.currentMp -= skill[_num].mana;
            
                

            switch (skill[_num].type)
            {
                case Skill.Type.Attack:
                    StartCoroutine(AttackSkill(_num));
                    break;
                case Skill.Type.Buff:
                    StartCoroutine(BuffSkill(_num));
                    break;
                case Skill.Type.Heal:
                    StartCoroutine(HealSkill(_num));
                    break;
            }
        }
    }

    IEnumerator AttackSkill(int _num)
    {
        GameObject SkillObject = Instantiate(skill[_num].skillPrefab, player.transform.position, player.transform.rotation);

        Rigidbody skillRigid = SkillObject.GetComponent<Rigidbody>();
        skillRigid.velocity = skillPos.forward * 4;

        Destroy(SkillObject, 2.1f);
        yield return new WaitForSeconds(skill[_num].delayTime);
        isSkill[_num] = 0;
    }

    IEnumerator BuffSkill(int _num)
    {
        buffSkillObject = Instantiate(skill[_num].skillPrefab, player.transform.position - new Vector3(0f,0f,-0.35f), player.transform.rotation);


        switch(skill[_num].skillName)
        {
            case "AttackUp":
                PlayerStatus.instance.meleeDamage *= skill[_num].dataValue;
                yield return new WaitForSeconds(skill[_num].delayTime);
                PlayerStatus.instance.meleeDamage /= skill[_num].dataValue;
                break;
            case "DeffenceUp":
                break;
            case "IgnoreDamage":
                break;
        }

        

        yield return new WaitForSeconds(1f);
        Destroy(buffSkillObject);
        isSkill[_num] = 0;
    }

    IEnumerator HealSkill(int _num)
    {
        GameObject SkillObject = Instantiate(skill[_num].skillPrefab, player.transform.position, player.transform.rotation);

        PlayerStatus.instance.currentHp += skill[_num].dataValue;
        if (PlayerStatus.instance.currentHp >= PlayerStatus.instance.maxHp)
            PlayerStatus.instance.currentHp = PlayerStatus.instance.maxHp;

        Destroy(SkillObject, 0.5f);
        yield return new WaitForSeconds(skill[_num].delayTime);
        isSkill[_num] = 0;
    }




}
