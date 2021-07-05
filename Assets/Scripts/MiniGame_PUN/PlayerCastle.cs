using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCastle : MonoBehaviourPun
{
    public GameObject target;
    public GameObject bullet;

    
    public int Hp = 100;
    public float range = 9f;
    public float delay = 1.2f;
    public string enemyTag;
    bool isAttack = false;
    public bool isDead = false;

    public Image castleHP;

    public GameObject castleLife;
    public GameObject castleDeath;
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
        if(PhotonNetwork.InRoom)
        {

            UpdateTartget();
            TryAttack();

            if (Hp <= 0)
            {
                isDead = true;
                DestroyEffect.SetActive(true);
                PV.RPC("Destroy", RpcTarget.AllViaServer);
            }

            PV.RPC("currentHP", RpcTarget.MasterClient);
        }

        
    }



    void UpdateTartget()
    {
        if (target == null)
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

    //[PunRPC]
    void TryAttack()
    {

        if(!isAttack && target != null && !isDead)
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isAttack = true;

        GameObject fire = PhotonNetwork.Instantiate(bullet.name, target.transform.position, Quaternion.identity);
        DiceUnit diceEnemy = target.GetComponent<DiceUnit>();
        diceEnemy.hp -= 5 - diceEnemy.deffence;

        Debug.Log(diceEnemy.hp);

        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(fire);

        yield return new WaitForSeconds(delay - 1f);

        isAttack = false;
    }

    [PunRPC]
    void currentHP()
    {
        castleHP.fillAmount = Hp / 100f;
    }


    [PunRPC]
    private void Destroy()
    {
        StartCoroutine(DestroyCastle());
    }

    IEnumerator DestroyCastle()
    {
        DestroyEffect.SetActive(true);
        castleLife.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        castleDeath.SetActive(true);
        

    }
}
