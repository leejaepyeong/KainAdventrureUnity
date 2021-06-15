using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEffect : MonoBehaviour
{
    //public static Skill instance;
    public PlayerStatusData playerdata;

    public Skill[] skill;

    public int[] isSkill = { 0 };   // 1~3 ?? ??(?? / ?? / ?)

    public Transform skillPos;

    public GameObject buffSkillObject;

    private int currentSkillNum;

    public Image[] skillCool; 

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
            if (playerdata.currentMp < skill[_num].mana)
            {
                return;
            }

            isSkill[_num] = 1;
            playerdata.currentMp -= skill[_num].mana;

            StartCoroutine(SkiilCool(_num));    

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

    IEnumerator SkiilCool(int _num)
    {
        float time = skill[_num].delayTime;
        while (time>0)
        {

            yield return new WaitForSeconds(0.1f);

            time -= 0.1f;

            if (time < 0)
                time = 0;

            skillCool[_num].fillAmount = time / skill[_num].delayTime; 

        }


        yield return null;
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
                playerdata.meleeDamage *= skill[_num].dataValue;
                playerdata.rangeDamage *= skill[_num].dataValue;
                yield return new WaitForSeconds(skill[_num].delayTime - 1);
                playerdata.meleeDamage /= skill[_num].dataValue;
                playerdata.rangeDamage /= skill[_num].dataValue;
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

        playerdata.currentHp += skill[_num].dataValue;
        if (playerdata.currentHp >= playerdata.maxHp)
            playerdata.currentHp = playerdata.maxHp;

        Destroy(SkillObject, 1.5f);
        yield return new WaitForSeconds(skill[_num].delayTime - 1.5f);
        isSkill[_num] = 0;
    }




}
