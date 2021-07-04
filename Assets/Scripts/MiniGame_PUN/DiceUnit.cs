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
    public PlayerCastle castle;
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

    //필요 컴포넌트
    public Rigidbody RB;
    public CapsuleCollider capCollider;
    public Animator anim;
    public NavMeshAgent pathFinder;
    public PhotonView PV;

    private void Start()
    {
        PV = photonView;

        hp = diceUnitData.hp;
        deffence = diceUnitData.deffence;
        delay = diceUnitData.delay;
        attackRange = diceUnitData.range;

        if (tag == "Player1")
            enemyTag = "Player2";
        else if (tag == "Player2")
            enemyTag = "Player1";

        castle = GameObject.FindGameObjectWithTag(enemyTag).GetComponent<PlayerCastle>();
    }

    private void Update()
    {
        PV.RPC("AiMove", RpcTarget.AllViaServer);
       
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
        }
    }


    [PunRPC]
    void AiMove()
    {
        if(!isAttack)
        {
            anim.SetBool("Walk",isMove);
            isMove = true;
            pathFinder.isStopped = false;

            if (target == null)
                pathFinder.SetDestination(castle.transform.position);

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
    

    protected virtual void Die()
    {
        isDead = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == enemyTag && target == null)
        {
            UpdateTartget();
        }
    }
}
