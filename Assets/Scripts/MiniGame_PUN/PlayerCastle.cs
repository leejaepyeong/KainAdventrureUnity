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
    public float range = 7.5f;
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


        NetworkManager.NM.GameReset = StartReset;
    }



    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {

            if (PhotonNetwork.IsMasterClient && !isDead)
            {
                if (target == null)
                    PV.RPC("UpdateTartget", RpcTarget.AllBuffered);

                else if(target != null)
                PV.RPC("TryAttack", RpcTarget.AllBuffered);

                

                if(Hp <= 0)
                {
                    isDead = true;
                    DestroyEffect.SetActive(true);

                    if (gameObject.tag == "Player1")
                    {
                        NetworkManager.NM.MyPlayer = NetworkManager.NM.Players[0];
                    }
                    else
                    {
                        NetworkManager.NM.MyPlayer = NetworkManager.NM.Players[1];
                    }

                    PV.RPC("tryDestroy", RpcTarget.AllBuffered);

                    
                     
                }

                currentHP();
            }
        }

        
    }

    void StartReset()
    {
        Hp = 100;
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

        if (!isAttack && target != null && !isDead)
        {
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

        yield return new WaitForSeconds(0.5f);


        NetworkManager.NM.GameSet();
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
