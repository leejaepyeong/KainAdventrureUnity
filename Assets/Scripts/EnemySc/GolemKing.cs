using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemKing : MeleeEnemy
{

    protected override IEnumerator Skill()
    {


        int random = Random.Range(0,3);

        if (random == 2) random = 0;   // skill1 66% /  skill2 33%

        isSkill = true;

        switch(random)
        {
            case 0:
                yield return new WaitForSeconds(0.5f);
                anim.SetTrigger("Skill1");
                yield return new WaitForSeconds(0.3f);
                skillArea[random].SetActive(true);

                yield return new WaitForSeconds(0.9f);
                skillArea[random].SetActive(false);
                break;
            case 1:
                yield return new WaitForSeconds(1f);
                anim.SetTrigger("Skill2");
                yield return new WaitForSeconds(0.5f);
                skillArea[random].SetActive(true);

                yield return new WaitForSeconds(0.4f);
                skillArea[random].SetActive(false);

                yield return new WaitForSeconds(0.5f);
                skillArea[random].SetActive(true);

                yield return new WaitForSeconds(0.4f);
                skillArea[random].SetActive(false);

                break;

        }

        isSkill = false;

        yield return new WaitForSeconds(5f);
        isSkillCool = true;


        yield return base.Skill();
    }


}
