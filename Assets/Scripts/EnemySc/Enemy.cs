using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    //??
    protected float currentHp;
    protected float currentMp;
    protected float damage;
    protected float defense;
    protected float delay;

    // ???
    protected float walkSpeed;
    protected float RunSpeed;
    protected float applySpeed;

    // ??
    protected float hpRegen;
    protected float mpRegen;
    protected float regenTime;

    protected Vector3 direction;  // ??

    public Transform Target;    // ??
    public BoxCollider meleeArea;   // ?? ????

    // 
    protected float currentTime = 5f;

    // ??
    protected bool isHit = false;   //????????
    protected bool isDead = false;  // ????
    protected bool isWalking = false;   // ????
    protected bool isRegen = true;  // ????????
    protected bool isSight = false; // ???????? ????
    protected bool isAttack = false;    // ??????

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
     
        //?????????? ?????? ??????

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

    public virtual void Hit(float _damage)
    {

        isRegen = false;
        regenTime = 0;
        StopCoroutine("HitCoroutine");

        if(!isDead)
        StartCoroutine(HitCoroutine(_damage));
    }

    IEnumerator HitCoroutine(float _damage)
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
        // ????
        if (other.tag == "PlayerAttack")
        {

            Weapon playerAttack = other.transform.parent.gameObject.GetComponent<Weapon>();
            
            if(!isHit)
            {
                Hit(playerAttack.damage + PlayerStatus.instance.meleeDamage);
            }

        }
        else if(other.tag == "PlayerRange") //??? ??
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

