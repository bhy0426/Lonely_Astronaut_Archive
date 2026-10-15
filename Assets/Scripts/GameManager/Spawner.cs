using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    private float curTime;
    [SerializeField]
    private float spawnTime;


    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        //transform.position = GameManager.instance.player.transform.position;
        if (GameManager.instance.gameTime < 1200f)
        {
            if (!GameManager.instance.bossLevel)
            {
                if (GameManager.instance.gameTime >= 0.01f && GameManager.instance.gameTime < 60f)
                {
                    spawnTime = 0.7f;
                    Spawn(0);
                }
                //else if (GameManager.instance.gameTime >= 10f && GameManager.instance.gameTime < 20f)
                else if (GameManager.instance.gameTime >= 60f && GameManager.instance.gameTime < 120f)
                {
                    spawnTime = 0.6f;
                    Spawn(0);
                }
                else if (GameManager.instance.gameTime >= 120f && GameManager.instance.gameTime < 180f)
                {
                    spawnTime = 0.5f;
                    Spawn(Random.Range(0, 2));
                }
                else if (GameManager.instance.gameTime >= 180f && GameManager.instance.gameTime < 300f)
                {
                    spawnTime = 0.5f;
                    int randChance = Random.Range(1, 101);
                    if (randChance > 0 && randChance <= 10)
                    {
                        Spawn(2);
                    }
                    else
                    {                   
                        Spawn(Random.Range(0, 2));
                    }
                }
                else if (GameManager.instance.gameTime >= 300f && GameManager.instance.gameTime < 600f)
                {
                    spawnTime = 0.5f;
                    int randChance = Random.Range(1, 101);
                    if (randChance > 0 && randChance <= 10)
                    {
                        Spawn(Random.Range(2, 4));
                    }
                    else
                    {
                        Spawn(Random.Range(0, 2));
                    }
                }
            }
            curTime += Time.deltaTime;
        }
        else
        {
            //Debug.Log("Game End");
        }
    }

    void Spawn(int enemyIndex)
    {
        //spawnTime = 2f;
        if (curTime >= spawnTime)
        {
            int index = Random.Range(1, spawnPoint.Length);
            while (true)
            {
                if (spawnPoint[index].gameObject.GetComponent<SpawnPoint>().isHit)
                {
                    index = Random.Range(1, spawnPoint.Length);
                }
                else
                    break;
            }
            GameObject enemy = GameManager.instance.pool.Get(enemyIndex);
            enemy.transform.position = spawnPoint[index].position;

            enemy.GetComponent<Enemy>().Init(spawnData[enemyIndex]);
            curTime = 0;
        }
    }
}
[System.Serializable]
public class SpawnData
{
    public float health;
    public float speed;
    public float atk;
}
