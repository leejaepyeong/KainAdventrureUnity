using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushKing : MeleeEnemy
{
    protected override IEnumerator Skill()
    {

        isSkill = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Skill1");
        yield return new WaitForSeconds(0.3f);
        skillArea[0].SetActive(true);

        yield return new WaitForSeconds(0.9f);
        skillArea[0].SetActive(false);
        isSkill = false;

        yield return new WaitForSeconds(5f);
        isSkillCool = true;


        yield return base.Skill();
    }

}
