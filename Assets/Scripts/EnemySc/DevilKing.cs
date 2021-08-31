using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilKing : MeleeEnemy
{
    public Transform[] skillPos;   // 1 skill Position

    protected override IEnumerator Skill()
    {
        int random = Random.Range(0,11);

        isSkill = true;

        switch (random)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                

                yield return new WaitForSeconds(0.5f);
                anim.SetTrigger("Skill3");
                yield return new WaitForSeconds(0.2f);

                for (int i = 0; i < skillPos.Length; i++)
                {
                    GameObject skill = Instantiate(SkillPrefabs[0], skillPos[i].position, skillPos[i].rotation);
                    Rigidbody skillRigid = skill.GetComponent<Rigidbody>();
                    skillRigid.velocity = skillPos[i].forward * 4f;
                    Destroy(skill, 2.1f);

                    yield return new WaitForSeconds(0.15f);
                }

                yield return new WaitForSeconds(0.5f);
               
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                yield return new WaitForSeconds(0.3f);
                anim.SetTrigger("Skill2");
                yield return new WaitForSeconds(0.3f);
                skillArea[0].SetActive(true);

                yield return new WaitForSeconds(1f);
                skillArea[0].SetActive(false);

                yield return new WaitForSeconds(0.1f);
                skillArea[1].SetActive(true);

                yield return new WaitForSeconds(0.5f);
                skillArea[1].SetActive(false);

                break;
            case 9:
            case 10:
                anim.SetTrigger("Charge");
                yield return new WaitForSeconds(3f);
                anim.SetTrigger("Skill1");
                yield return new WaitForSeconds(0.3f);
                skillArea[2].SetActive(true);

                yield return new WaitForSeconds(0.5f);
                skillArea[2].SetActive(false);

                break;

        }

        isSkill = false;

        yield return new WaitForSeconds(3f);
        isSkillCool = true;

        yield return base.Skill();
    }

}
