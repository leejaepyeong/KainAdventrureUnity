using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RangeUnit : DiceUnit
{
    public GameObject bulletPrefab;

    [PunRPC]
    protected override void tryAttack()
    {
        base.tryAttack();
    }

    protected override IEnumerator Attack()
    {
        

        yield return new WaitForSeconds(0.2f);

        anim.SetTrigger("SwordAttack");

        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bullet.tag = tag;
        bulletRigid.velocity = gameObject.transform.forward * 3f;

        

        Destroy(bullet, 1.1f);

        yield return new WaitForSeconds(delay - 0.2f);

        yield return base.Attack();

    }
}
