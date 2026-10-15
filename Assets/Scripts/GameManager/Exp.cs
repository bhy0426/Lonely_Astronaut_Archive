using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    Rigidbody2D rigid;
    public Player target;
    float curTime;
    float maxTime = 0.25f;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player;
    }

    private void Update()
    {
        if (curTime >= maxTime)
        {
            rigid.velocity = new Vector2(0, 0);
        }

        if (Vector2.Distance(target.GetComponent<Rigidbody2D>().position, rigid.position) <= 1f)
        {
            transform.position = Vector2.Lerp(rigid.position, target.GetComponent<Rigidbody2D>().position, Time.deltaTime * 5f);
        }
        curTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            target.exp += 1;
        }
    }
}
