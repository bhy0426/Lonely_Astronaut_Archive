using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Enemy enemy;
    public GameObject shadow;
    Animator anim;
    public RuntimeAnimatorController[] animCon;
    SpriteRenderer spriter;

    float moveCooldown = 0.4f;
    public float curTime;
    public float velocity;
    public float maxTime = 1f;
    public bool isMove = false;

    Vector2 dirVec;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetBool("isMove", isMove);
        anim.runtimeAnimatorController = GameManager.instance.player.transform.position.x > enemy.rigid.position.x ? animCon[0] : animCon[1];
        shadow.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    void FixedUpdate()
    {
        if (!enemy.isHit)
        {
            if (!isMove)
            {
                curTime += Time.deltaTime;
                velocity = 0;
                enemy.rigid.velocity = Vector2.zero;
                if (curTime >= maxTime)
                {
                    isMove = true;
                    curTime = 0;
                    Invoke("MoveCooldown", moveCooldown);
                    dirVec = enemy.target.position - enemy.rigid.position;
                }
            }
            else
            {
                //velocity += Time.deltaTime;
                Vector2 nextVec = dirVec.normalized * Time.deltaTime * 4f;
                //enemy.rigid.MovePosition(enemy.rigid.position + nextVec);
                enemy.rigid.AddForce(dirVec.normalized * enemy.speed, ForceMode2D.Impulse);
            }
        }
        else
        {
            //curTime = 0;
        }
    }

    void MoveCooldown()
    {
        isMove = false;
    }
}
