using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // 프리팹 목록
    public GameObject enemyPrefab;  // 생성 객체
    public float respawnTime;

    float currentTime = 0f;

   

    private void Update()
    {
        if(GameManager.instance.isGameOn)
            RespawnEnemy();
        
    }

    void TimeCheck()
    {
        currentTime += Time.deltaTime;
    }

    void RespawnEnemy()
    {
        if (enemyPrefab == null)
        {
            TimeCheck();

            if(currentTime >= respawnTime)
            {
                int random = Random.Range(0, enemyPrefabs.Length);
                enemyPrefab = Instantiate(enemyPrefabs[random], transform.position, Quaternion.identity);
                currentTime = 0;
            }
        }
    }

}
