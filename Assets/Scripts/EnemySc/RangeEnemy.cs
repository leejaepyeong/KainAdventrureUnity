using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public GameObject bulletPrefab;

    protected override IEnumerator Attack()
    {
        transform.LookAt(Target);

        anim.SetTrigger("RangeAttack");
        yield return new WaitForSeconds(0.3f);

        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,0.3f,0), Quaternion.identity);
        Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = gameObject.transform.forward * 3f;


        Destroy(bullet, 1f);

        yield return new WaitForSeconds(enemyData.delay - 0.3f);

        isAttack = false;

        yield return base.Attack();

    }
}
