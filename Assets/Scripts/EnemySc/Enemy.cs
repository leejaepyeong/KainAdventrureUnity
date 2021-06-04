using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    //기본 스탯
    protected int currentHp;
    protected int currentMp;
    protected int damage;
    protected int defense;
    protected float delay;

    // 이동속도
    protected float walkSpeed;
    protected float RunSpeed;
    protected float applySpeed;

    // 회복력
    protected int hpRegen;
    protected int mpRegen;
    protected float regenTime;

    protected Vector3 direction;  // 방향

    public Transform Target;    // 타겟
    public BoxCollider meleeArea;   // 공격 콜라이더

    // 
    protected float currentTime = 5f;

    // 상태여부
    protected bool isHit = false;   //공격당함
    protected bool isDead = false;  // 죽음
    protected bool isWalking = false;   // 걷기
    protected bool isRegen = true;  // 회복가능
    protected bool isSight = false; // 플레이어 감지
    protected bool isAttack = false;    // 공격중

    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected BoxCollider boxCollider;
    protected Animator anim;
    public NavMeshAgent nav;



    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        currentHp = enemyData.hp;
        currentMp = enemyData.mp;
        walkSpeed = enemyData.moveSpeed;
        RunSpeed = walkSpeed * 1.5f;
        applySpeed = walkSpeed;


    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            HPRegen();
            MPRegen();
            RegenTime();
            AiMoveCheck();
            Targeteting();
        }
        
    }

    protected void AiMoveCheck()
    {
        float distance = Vector3.Distance(transform.position,Target.transform.position);
        nav.SetDestination(Target.position);
        anim.SetBool("Walk", isWalking);

        if (distance < 10f)
        {
            isSight = true;
        }   
        else
        {
            isSight = false;

            nav.isStopped = true;
            nav.updatePosition = false;
            nav.updateRotation = false;
            nav.velocity = Vector3.zero;
            isWalking = false;
        }

        if (isSight)
        {
            nav.isStopped = false;
            nav.updatePosition = true;
            nav.updateRotation = true;

            nav.speed = RunSpeed;
            isWalking = true;
        }
        
        if(isAttack)
        {
            nav.isStopped = true;
            nav.updatePosition = false;
            nav.updateRotation = false;
            nav.velocity = Vector3.zero;
        }
       
    }

    protected void RegenTime()
    {
        if(!isHit)
        {
            regenTime += Time.deltaTime;

            if(enemyData.regenTime <= regenTime)
            {
                isRegen = true;
            }
        }
        
    }
  

    protected virtual void Targeteting()
    {
        if (!isDead)
        {
            float targetRadius = 0f;
            float targetRange = 0f;


            targetRadius = 0.5f;       

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius,
                                  transform.forward, targetRange, LayerMask.GetMask("Player"));
       

            if (rayHits.Length > 0 && !isAttack && !Target.GetComponent<PlayerControleer>().isDead)
            {
                StartCoroutine(Attack());
            }
        }

    }

    protected virtual IEnumerator Attack()
    {
        isWalking = false;
        isAttack = true;
        anim.SetTrigger("Attack");

                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.3f);
                meleeArea.enabled = false;

                yield return new WaitForSeconds(enemyData.delay - 0.5f);
     
        //애니메이션 딜레이 있어서

        isAttack = false;

    }


    protected virtual void HPRegen()
    {
        if(isRegen && currentHp < enemyData.hp)
        {
            isRegen = false;
            regenTime = enemyData.regenTime / 2;

            currentHp += enemyData.hpRegen;
        }
           

        if (currentHp > enemyData.hp)
            currentHp = enemyData.hp;
    }

    protected virtual void MPRegen()
    {
        if (isRegen && currentMp < enemyData.mp)
            currentMp += enemyData.mpRegen;

        if (currentMp > enemyData.mp)
            currentMp = enemyData.mp;
    }

    public virtual void Hit(int _damage)
    {

        isRegen = false;
        regenTime = 0;
        StopCoroutine("HitCoroutine");

        if(!isDead)
        StartCoroutine(HitCoroutine(_damage));
    }

    IEnumerator HitCoroutine(int _damage)
    {
        
        isHit = true;
        currentHp -= _damage;

        if (currentHp <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");
            PlayerStatus.instance.currentExp += enemyData.Exp;
            Destroy(gameObject, 2f);
            
        }

        yield return new WaitForSeconds(0.7f);

        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {

            Weapon playerAttack = other.transform.parent.gameObject.GetComponent<Weapon>();
            
            if(!isHit)
            {
                Hit(playerAttack.damage + PlayerStatus.instance.meleeDamage);
            }

        }
        else if(other.tag == "PlayerRange")
        {

            Bullets bullet = other.gameObject.GetComponent<Bullets>();

            if (!isHit)
            {
                Hit(bullet.damage + PlayerStatus.instance.rangeDamage);
                Destroy(other.gameObject, 0.1f);

            }
        }
    }

}

