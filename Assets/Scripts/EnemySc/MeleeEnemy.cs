using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{


    protected override IEnumerator Attack()
    {
        transform.LookAt(Target);

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(enemyData.delay - 0.5f);

        isAttack = false;

        yield return base.Attack();

    }
}
