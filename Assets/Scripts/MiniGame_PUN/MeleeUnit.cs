using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MeleeUnit : DiceUnit
{
    [PunRPC]
    protected override void tryAttack()
    {
        base.tryAttack();
    }

    protected override IEnumerator Attack()
    {
        anim.SetTrigger("SwordAttack");


        if(target != castle.gameObject)
        {
            DiceUnit unit = target.GetComponent<DiceUnit>();
            unit.hp -= damage - unit.deffence;
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
