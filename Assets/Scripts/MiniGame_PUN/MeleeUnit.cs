using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : DiceUnit
{

    protected override IEnumerator Attack()
    {
        anim.SetTrigger("SwordAttack");


        if(target.GetComponent<DiceUnit>() != null)
        {
            DiceUnit unit = target.GetComponent<DiceUnit>();
            unit.hp -= unit.deffence - damage;
        }
        else
        {
            PlayerCastle unit = target.GetComponent<PlayerCastle>();
            unit.Hp -= damage;
        }
            
        yield return new WaitForSeconds(delay - 0.5f);

        yield return base.Attack();

    }
}
