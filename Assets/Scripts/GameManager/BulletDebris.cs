using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDebris : MonoBehaviour
{
    Rigidbody2D rigid;
    float curTime;
    float deadTime;
    float maxTime = 0.25f;
    int rand;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * Random.Range(2.0f, 3.1f), ForceMode2D.Impulse);
        rand = Random.Range(-1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (deadTime >= 5f)
            Destroy(gameObject);

        if (curTime >= maxTime)
        {
            rigid.velocity = new Vector2(0, 0);
        }
        else
            transform.Rotate(0, 0, 15 * rand);

        curTime += Time.deltaTime;
        deadTime += Time.deltaTime;

        transform.Rotate(0, 0, Time.deltaTime * Random.Range(-1, 2));

    }
}
