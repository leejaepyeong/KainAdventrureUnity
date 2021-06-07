using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    public MaterialData materialData;

    private int currentHp;

    bool isHit = false;
    bool isBreak = false;

    public GameObject[] treeReward;
    public GameObject[] mineReward;
    public ParticleSystem hitEffect;

    private int rewardCase; // 0 : Normal , 1 : Uniq, 2 : Legend

    private void Start()
    {
        currentHp = materialData.hp;
    }

    void Hit(int _damage)
    {
        isHit = true;
        hitEffect.gameObject.SetActive(true);

        currentHp -= _damage;

        if(currentHp <= 0)
        {
            isBreak = true;
            PlayerStatus.instance.currentExp += materialData.exp;
            Reward();
        }

        Invoke("HitOver", 0.9f);

    }

    void HitOver()
    {
        hitEffect.gameObject.SetActive(false);
        isHit = false;
    }

    protected virtual void Reward()
    {
        if(materialData.materialType == MaterialData.MaterialType.Tree)
        {
            switch(materialData.materialValue)
            {
                case MaterialData.MaterialValue.Normal:
                    rewardCase = 0;
                    break;
                case MaterialData.MaterialValue.Uniq:
                    rewardCase = 1;
                    break;
                case MaterialData.MaterialValue.Legend:
                    rewardCase = 2;
                    break;
            }

            StartCoroutine(CreateWood(rewardCase));
        }
        else if(materialData.materialType == MaterialData.MaterialType.Mine)
        {
            switch (materialData.materialValue)
            {
                case MaterialData.MaterialValue.Normal:
                    rewardCase = 0;
                    break;
                case MaterialData.MaterialValue.Uniq:
                    rewardCase = 1;
                    break;
                case MaterialData.MaterialValue.Legend:
                    rewardCase = 2;
                    break;
            }
            StartCoroutine(CreateMine(rewardCase));

        }

        Destroy(gameObject, 0.2f);
    }

    IEnumerator CreateWood(int _value)
    {
        for (int i = 0; i < 2+_value; i++)
        {
            // 0 : 5  1 : 3  3 : 1
            int random = Random.Range(0, 9);
            if (random < 5 && random > 2) random = 1;
            else if (random >= 5) random = 0;

            Instantiate(treeReward[random], transform.position + transform.up, Quaternion.identity);
        }

        yield return null;
    }

    IEnumerator CreateMine(int _value)
    {
        for (int i = 0; i < 2 * (_value + 1); i++)
        {
            int random = Random.Range(0, 4);

            Instantiate(mineReward[random], transform.position + transform.up * 0.7f, Quaternion.identity);
        }

        yield return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack" && !isHit)
        {
            

            Weapon playerAttack = other.transform.parent.gameObject.GetComponent<Weapon>();

            if(materialData.materialType == MaterialData.MaterialType.Tree && playerAttack.type == Weapon.Type.Axe)
            {
                Hit(playerAttack.damage);
            }
            else if(materialData.materialType == MaterialData.MaterialType.Mine && playerAttack.type == Weapon.Type.PickAxe)
            {
                Hit(playerAttack.damage);
            }
            

        }
    }
}
