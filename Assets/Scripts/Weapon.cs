using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range, Axe, PickAxe };
    public Type type;
    public float damage;
    public float rate;  //  ????

    public bool isAttack;

    public BoxCollider meleeArea;   // ???? ????
    public TrailRenderer trailEffect;   //  ????

    public Transform bulletPos;
    public GameObject bullet;

    public PlayerControleer playerControl;

    public void Use()
    {
        if(!isAttack)
        {
            isAttack = true;

            StopAllCoroutines();

            if (type == Type.Melee)
            {
                playerControl.anim.SetTrigger("SwordAttack");
                StopCoroutine("Swing");
                StartCoroutine("Swing");
            }
            else if (type == Type.Range)
            {
                playerControl.anim.SetTrigger("BowAttack");
                StartCoroutine("Shot");
            }
            else if (type == Type.PickAxe)
            {
                playerControl.anim.SetTrigger("PickAxe");
                StartCoroutine(PickAxing());
            }
            else if (type == Type.Axe)
            {
                playerControl.anim.SetTrigger("Axe");
                StartCoroutine(Axing());
            }
        }

        

    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        meleeArea.gameObject.SetActive(false);

        yield return new WaitForSeconds(rate - 0.6f);

        isAttack = false;

    }

    IEnumerator Shot()
    {

        yield return new WaitForSeconds(rate);
        //???? ????
        isAttack = false;

        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 4;

        Destroy(instantBullet,2.1f);
        yield return null;
        
    }

    IEnumerator Axing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        meleeArea.gameObject.SetActive(false);
        yield return new WaitForSeconds(rate - 0.5f);

        isAttack = false;

    }

    IEnumerator PickAxing()
    {
        yield return new WaitForSeconds(0.15f);
        meleeArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        meleeArea.gameObject.SetActive(false);
        yield return new WaitForSeconds(rate - 0.55f);

        isAttack = false;
    }


    
}
