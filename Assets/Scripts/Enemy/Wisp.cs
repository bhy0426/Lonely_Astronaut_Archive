using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour
{
    Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        if (!enemy.isHit)
        {
            Vector2 dirVec = enemy.target.position - enemy.rigid.position;
            Vector2 nextVec = dirVec.normalized * enemy.speed * Time.fixedDeltaTime;
            enemy.rigid.MovePosition(enemy.rigid.position + nextVec);
        }
    }
}
