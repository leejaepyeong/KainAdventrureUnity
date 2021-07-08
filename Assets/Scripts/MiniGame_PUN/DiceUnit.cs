using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class DiceUnit : MonoBehaviourPun
{
    public List<GameObject> FoundObjects;
    public GameObject target;
    public GameObject castle;
    public string enemyTag;
    public DiceUnitData diceUnitData;

    bool isMove;
    protected bool isAttack = false;
    bool isDead = false;

    public int hp;
    public int damage;
    public int deffence;
    public float delay;

    public float dist;
    public float attackRange;

    public int myControl;

    //필요 컴포넌트
    public Rigidbody RB;
    public CapsuleCollider capCollider;
    public Animator anim;
    public NavMeshAgent pathFinder;
    public PhotonView PV;
    PlayerScript[] player;

    private void Start()
    {
        PV = photonView;

        hp = diceUnitData.hp;
        deffence = diceUnitData.deffence;
        delay = diceUnitData.delay;
        attackRange = diceUnitData.range;

        

        if (tag == "Player1")
        {
            enemyTag = "Player2";
            myControl = 1;
            castle = GameObject.FindGameObjectWithTag("PlayerCastle2");
            
            
        }
            
        else if (tag == "Player2")
        {
            enemyTag = "Player1";
            myControl = 2;
            castle = GameObject.FindGameObjectWithTag("playerCastle1");
        }
            


        
    }

 

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient || isDead)
            return;


        AiMove();

        if (hp <= 0)
            PV.RPC("Die", RpcTarget.AllViaServer);



    }

    void UpdateTartget(GameObject _other)
    {
        /*
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = 8f;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                
                if (distanceToEnemy < shortestDistance)
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
        */

        target = _other;


    }


    void AiMove()
    {

        if(!isAttack)
        {
            isMove = true;
            anim.SetBool("Walk",isMove);

            pathFinder.isStopped = false;

            if (target == null)
            {
                Debug.Log(castle);
                
                pathFinder.SetDestination(castle.transform.position);
            }
                

            else
            {
                pathFinder.SetDestination(target.transform.position);
                PV.RPC("tryAttack", RpcTarget.AllViaServer);
            }
        }
            

    }

    [PunRPC]
    protected virtual void tryAttack()
    {
        dist = Vector3.Distance(transform.position, target.transform.position);


        if(dist <= attackRange)
        {
            isMove = false;
            if(pathFinder.isStopped == false)
            pathFinder.isStopped = true;
            transform.LookAt(target.transform);

            if(!isAttack)
            {
                isAttack = true;
                StartCoroutine(Attack());
            }

        }
    }

    protected virtual IEnumerator Attack()
    {
        isAttack = false;
        yield return null;
    }
    
    [PunRPC]
    protected virtual void Die()
    {
        isDead = true;

        Destroy(gameObject);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemyTag && target == null && PhotonNetwork.IsMasterClient)
        {
            UpdateTartget(other.gameObject);
        }
    }
}
