using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : MonoBehaviour
{
    Animator anim;
    public RuntimeAnimatorController[] animCon;
    SpriteRenderer sprite;
    BoxCollider2D coll;

    Vector2 target, mouse;
    float angle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        coll.enabled = false;
    }

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - GameManager.instance.player.transform.position.y, mouse.x - GameManager.instance.player.transform.position.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void LateUpdate()
    {
        GetComponent<Animator>().SetBool("isAttack", GameManager.instance.playerAtk.isAttackAnimMelee);
        //GetComponent<Animator>().SetFloat("meleeAnimSpeed", GameManager.instance.playerAtk.meleeAnimSpeed);

        //if (transform.position.x < mouse.x)
        //    transform.localRotation = Quaternion.Euler(0, 0, 0);
        //else if (transform.position.x > mouse.x)
        //    transform.localRotation = Quaternion.Euler(0, 180, -(angle + 180f));

        //sprite.flipY = transform.position.x > mouse.x;

        //if (transform.position.x > mouse.x)
        //{
        //    anim.runtimeAnimatorController = animCon[0];
        //}
        //else if (transform.position.x < mouse.x)
        //{
        //    anim.runtimeAnimatorController = animCon[1];
        //}
    }
}
