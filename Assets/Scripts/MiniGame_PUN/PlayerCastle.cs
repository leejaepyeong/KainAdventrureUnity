using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCastle : MonoBehaviourPun, IPunObservable
{
    public GameObject target;
    public GameObject bullet;

    
    public int Hp = 100;
    public float range = 9f;
    public float delay = 1.2f;
    public string enemyTag;
    bool isAttack = false;
    public bool isDead = false;
    bool isStart = false;

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

    [PunRPC]
    void InitUnit()
    {

        

        isStart = true;
    }


    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if(!isStart)
                PV.RPC("InitUnit", RpcTarget.AllViaServer);

            if (PhotonNetwork.IsMasterClient && !isDead)
            {
                if (target == null)
                    PV.RPC("UpdateTartget", RpcTarget.AllBuffered);

                if(target != null)
                PV.RPC("TryAttack", RpcTarget.AllBuffered);

                if(Hp <= 0)
                {
                    isDead = true;
                    DestroyEffect.SetActive(true);
                    PV.RPC("tryDestroy", RpcTarget.AllBuffered);
                }

                currentHP();
            }
        }

        
    }


    [PunRPC]
    void UpdateTartget()
    {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = range;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= shortestDistance)
                {
                     shortestDistance = distanceToEnemy;
                     nearestEnemy = enemy;
                     break;
                    
                }


            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy;
            }
     
    }

    [PunRPC]
    void TryAttack()
    {
        Debug.Log("castleTry");
        Debug.Log(target);

        if (!isAttack && target != null && !isDead)
        {
            Debug.Log("castleAttack");
            isAttack = true;
            StartCoroutine(Attack());
        }
            
    }

    IEnumerator Attack()
    {
        GameObject fire = PhotonNetwork.Instantiate(bullet.name, target.transform.position, Quaternion.identity);
        DiceUnit diceEnemy = target.GetComponent<DiceUnit>();

        diceEnemy.hp -= 5 - diceEnemy.deffence;


        yield return new WaitForSeconds(1f);
        PhotonNetwork.Destroy(fire);

        yield return new WaitForSeconds(delay - 1f);

        isAttack = false;


    }


    void currentHP()
    {
        castleHP.fillAmount = Hp / 100f;
    }


    [PunRPC]
    private void tryDestroy()
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(Hp);
        }
        else
        {
            Hp = (int)stream.ReceiveNext();
            currentHP();
        }
    }
}
