using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : DiceUnit
{
    Collider meleeArea;

    protected override IEnumerator Attack()
    {
        anim.SetTrigger("SwordAttack");

        DiceUnit unit = target.GetComponent<DiceUnit>();

        unit.hp -= unit.deffence - damage;

        yield return new WaitForSeconds(delay - 0.5f);

        yield return base.Attack();

    }
}
