using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Enemy enemy;
    public bool atkReady;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        // 드론이 맞지 않았으면
        if (!enemy.isHit)
        {
            // 사거리 밖에 있을 때
            if (Vector2.Distance(enemy.target.position, enemy.rigid.position) >= 7f)
            {
                // 이동
                Vector2 dirVec = enemy.target.position - enemy.rigid.position;
                Vector2 nextVec = dirVec.normalized * enemy.speed * Time.fixedDeltaTime;
                enemy.rigid.MovePosition(enemy.rigid.position + nextVec);
                atkReady = false;
                //enemy.rigid.constraints = ~RigidbodyConstraints2D.FreezePosition;
            }
            // 사거리 안에 있을 때
            else
            {
                atkReady = true;
                //enemy.rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            atkReady = false;
        }
    }
}
