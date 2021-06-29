using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Quest quest;

    public EnemyData enemyData;

    public PlayerStatusData playerData;

    //??
    protected float currentHp;
    protected float currentMp;

    protected float delay;

    // ???
    protected float walkSpeed;
    protected float RunSpeed;
    protected float applySpeed;

    // 회복 시간
    protected float regenTime;

    protected Vector3 direction;  // ??

    public Transform Target;    // ??
    public BoxCollider meleeArea;   // ?? ????
    public GameObject[] skillArea;

    public Image hpBar;


    public GameObject[] SkillPrefabs;

    // 
    protected float currentTime = 5f;

    // ??
    protected bool isHit = false;   //????????
    protected bool isDead = false;  // ????
    protected bool isWalking = false;   // ????
    protected bool isRegen = true;  // ????????
    protected bool isSight = false; // ???????? ????
    protected bool isAttack = false;    // ??????
    protected bool isSkill = false;
    protected bool isSkillCool = true;
    protected bool isArea = false;

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

        Target = FindObjectOfType<PlayerControleer>().transform;
        quest = FindObjectOfType<Quest>();

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
            SkillOn();
            
        }
    }


    
    protected void AiMoveCheck()
    {
        float distance = Vector3.Distance(transform.position,Target.transform.position);
        nav.SetDestination(Target.position);
        anim.SetBool("Walk", isWalking);

        if (distance < 11f)
        {
            isSight = true;
        }   
        else
        {
            isSight = false;

            nav.speed = 0;
            nav.isStopped = true;
            nav.updatePosition = false;
            nav.updateRotation = false;
            nav.velocity = Vector3.zero;
            isWalking = false;
        }

        if (isSight && !isArea)
        {
            nav.isStopped = false;
            nav.updatePosition = true;
            nav.updateRotation = true;

            nav.speed = RunSpeed;
            isWalking = true;
        }
        
        if(isArea)
        {
            nav.speed = 0;
            nav.isStopped = true;
            nav.updatePosition = false;
            nav.updateRotation = false;
            nav.velocity = Vector3.zero;
            isWalking = false;
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

    protected virtual void SkillOn()
    {
        if(!isDead && isSkillCool)
        {

            
            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, enemyData.targetRange + 1.5f,
                                  transform.up, 0.5f, LayerMask.GetMask("Player"));

            

            if (rayHits.Length > 0 && !Target.GetComponent<PlayerControleer>().isDead)
            {
                isArea = true;
                isSkillCool = false;
                StartCoroutine(Skill());
            }

            else if (rayHits.Length == 0)
                isArea = false;
        }
    }


    protected virtual void Targeteting()
    {
        if (!isDead)
        {
                 

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, enemyData.targetRange + 0.2f,
                                  transform.up, 0.5f, LayerMask.GetMask("Player"));


            if (rayHits.Length > 0 && !isAttack && !Target.GetComponent<PlayerControleer>().isDead && !isSkill)
            {
                isArea = true;
                isWalking = false;
                isAttack = true;
                StartCoroutine(Attack());
            }

            else if(rayHits.Length == 0)
                isArea = false;

        }

    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
    }


    protected virtual IEnumerator Skill()
    {

        yield return null;
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

        hpBar.fillAmount = currentHp / enemyData.hp;
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
        currentHp -= ( (_damage - enemyData.defense) <= 0 ? 1 : _damage - enemyData.defense);
        hpBar.fillAmount = currentHp / enemyData.hp;

        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            quest.MonsterDead(enemyData);
            anim.SetTrigger("Die");
            playerData.currentExp += enemyData.Exp;
            Destroy(gameObject, 2f);
            
        }

        yield return new WaitForSeconds(0.4f);

        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ????
        if (other.tag == "PlayerAttack")
        {
            Weapon playerAttack = other.transform.parent.gameObject.GetComponent<Weapon>();

            if (!isHit && playerAttack.type == Weapon.Type.Melee && !isDead)
            {
                Hit(playerData.meleeDamage);
            }

        }
        else if(other.tag == "PlayerRange") //??? ??
        {
            if (!isHit && !isDead)
            {
                Hit(playerData.rangeDamage);
                Destroy(other.gameObject, 0.1f);

            }
        }
        else if(other.tag == "PlayerSkill")
        {
            AttackSkill skill = other.GetComponent<AttackSkill>();

            if(!isHit)
            {
                Hit(skill.skill.dataValue);
                Destroy(other.gameObject, 0.1f);
            }
        }
    }

}

