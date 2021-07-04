using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUnit : DiceUnit
{
    public GameObject bulletPrefab;

    protected override IEnumerator Attack()
    {
        anim.SetTrigger("SwordAttack");

        yield return new WaitForSeconds(0.2f);

        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bullet.tag = tag;
        bulletRigid.velocity = gameObject.transform.forward * 3f;

        Destroy(bullet, 0.8f);

        yield return new WaitForSeconds(delay - 0.2f);

        yield return base.Attack();

    }
}
