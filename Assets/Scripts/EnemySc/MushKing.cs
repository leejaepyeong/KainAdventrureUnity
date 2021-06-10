using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushKing : Enemy
{
    

    protected override IEnumerator Skill()
    {
        isSkill = true;
        anim.SetTrigger("Skill1");
        yield return new WaitForSeconds(0.3f);
        skillArea.SetActive(true);

        yield return new WaitForSeconds(0.9f);
        skillArea.SetActive(false);
        isSkill = false;

        yield return new WaitForSeconds(5f);
        isSkillCool = true;
    }

}
