using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;

    //스탯
    protected int currentHp;
    protected int currentMp;
    protected int damage;
    protected int defense;
    protected float delay;

    // 무브 속도
    protected float walkSpeed;
    protected float RunSpeed;
    protected float apllySpeed;

    // 회복
    protected int hpRegen;
    protected int mpRegen;
    protected float regenTime;

    protected Vector3 direction;  // 방향

    // 딜레이 시간
    protected float currentTime;

    // 실행상태
    protected bool isHit = false;
    protected bool isDead = false;
    protected bool isWalking = false;
    protected bool isRunning = false;
    protected bool isAction = false;
    protected bool isRegen = true;

    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected BoxCollider boxCollider;
    protected Animator anim;



    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        currentHp = enemyData.hp;
        currentMp = enemyData.mp;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            Move();
            Rotation();
            HPRegen();
            MPRegen();
            RegenTime();
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
    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ReSet();
            }
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false;
        isAction = true;
        // anim.SetBool("Walking", isWalking);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
    }

    protected virtual void Move()
    {
        if(isHit)
        {

        }
        else
        {
            if (isWalking)
                rigid.MovePosition(transform.position + (transform.forward * enemyData.moveSpeed * Time.deltaTime));
        }
    }

    protected virtual void Rotation()
    {
        if(isHit)
        {

        }
        else
        {

        }
    }

    protected virtual void HPRegen()
    {
        if(isRegen && currentHp < enemyData.hp)
        {
            isRegen = false;
            regenTime = enemyData.regenTime / 2;

            Debug.Log("회복중");
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
        StopAllCoroutines();
        StartCoroutine(HitCoroutine(_damage));
    }

    IEnumerator HitCoroutine(int _damage)
    {
        
        isHit = true;
        currentHp -= _damage;
        Debug.Log(currentHp);

        if (currentHp <= 0)
        {
            isDead = true;
            Destroy(gameObject, 2f);
        }

        yield return new WaitForSeconds(0.7f);

        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            Debug.Log("충돌 감지");

            Weapon playerAttack = other.transform.parent.gameObject.GetComponent<Weapon>();
            
            if(!isHit)
            {
                Debug.Log(playerAttack.damage);
                Hit(playerAttack.damage);
            }

        }
        else if(other.tag == "PlayerRange")
        {
            Debug.Log("화살 감지");

            Bullets bullet = other.gameObject.GetComponent<Bullets>();

            if (!isHit)
            {
                Debug.Log(bullet.damage);
                Hit(bullet.damage);
                Destroy(other.gameObject, 0.1f);

            }
        }
    }

}

