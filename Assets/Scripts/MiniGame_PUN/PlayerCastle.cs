using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCastle : MonoBehaviourPun
{
    public GameObject target;
    public GameObject bullet;

    public int Hp = 100;
    public float range = 3f;
    public float delay = 1f;
    public string enemyTag;
    bool isAttack = false;
    public bool isDead = false;

    public GameObject DestroyEffect;
    public PhotonView PV;

    private void Start()
    {
        PV = photonView;

        if (tag == "Player1")
            enemyTag = "Player2";
        else if (tag == "Player2")
            enemyTag = "Player1";
    }


    private void Update()
    {
        UpdateTartget();
        PV.RPC("TryAttack",RpcTarget.AllBufferedViaServer);

        if (Hp <= 0)
        {
            isDead = true;
            DestroyEffect.SetActive(true);
            PV.RPC("Destroy", RpcTarget.AllBufferedViaServer);
        }
            
        
    }

    void UpdateTartget()
    {
        if (target != null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= range)
                {
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                        break;
                    }
                }


            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy;
            }
        }
    }

    [PunRPC]
    void TryAttack()
    {
        if(!isAttack && target != null)
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {


        GameObject fire = Instantiate(bullet, target.transform.position, Quaternion.identity);
        DiceUnit diceEnemy = target.GetComponent<DiceUnit>();
        diceEnemy.hp -= 5 - diceEnemy.deffence;

        yield return new WaitForSeconds(0.8f);
        Destroy(fire);

        yield return new WaitForSeconds(delay - 0.8f);
    }


    [PunRPC]
    private void Destroy()
    {
        

    }
}
