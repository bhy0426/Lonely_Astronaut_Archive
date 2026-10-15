using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject crowbar;
    public RuntimeAnimatorController[] animCon;
    public Sprite[] spriteWeapon;
    public RuntimeAnimatorController[] meleeMaster;
    public Sprite[] handgunMaster;
    public Sprite[] shotgunMaster;
    public Sprite[] rifleMaster;
    public Sprite[] sniperMaster;
    SpriteRenderer sprite;
    public Animator anim;

    float angle;
    Vector2 mouse;
    float axisX;
    float axisY;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.melee)
            transform.localScale = new Vector3(GameManager.instance.playerAtk.meleeSize, GameManager.instance.playerAtk.meleeSize, 0);
        else
            transform.localScale = Vector3.one;

        if (GameManager.instance.playerAtk.isAttackAr)   
        {
            //transform.position = new Vector2(GameManager.instance.player.transform.position.x + axisX, GameManager.instance.player.transform.position.y + axisY);
            if (Input.GetMouseButton(0))
            {
                if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.rifle)
                {
                    transform.position = GameManager.instance.player.transform.position;
                    transform.localPosition += (transform.up * Random.Range(-1, 2)) * 0.015f;
                }
            }
        }
        else
            transform.position = GameManager.instance.player.transform.position;



        //if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.rifle)
        //{
        //    //transform.position = new Vector2(GameManager.instance.player.transform.position.x + axisX, GameManager.instance.player.transform.position.y + axisY);
        //    if (Input.GetMouseButton(0))
        //    {
        //        if (GameManager.instance.playerAtk.isAttackAr)
        //        {
        //            transform.localPosition = new Vector2(GameManager.instance.player.transform.localPosition.x, GameManager.instance.player.transform.localPosition.y + Random.Range(-0.04f, 0.04f));
        //        }
        //    }
        //}
        //else
        //    transform.position = GameManager.instance.player.transform.position;





        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - GameManager.instance.player.transform.position.y, mouse.x - GameManager.instance.player.transform.position.x) * Mathf.Rad2Deg;
        //if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.melee)
        //{
        if (transform.position.x < mouse.x)
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        else if (transform.position.x > mouse.x)
            transform.localRotation = Quaternion.Euler(0, 180, -(angle + 180f));
        //}
        //else
        //    this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //if (transform.position.x <= mouse.x)
        //    this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //else if(transform.position.x > mouse.x)
        //    this.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
    }

    private void LateUpdate()
    {
       
        //sprite.flipY = transform.position.x > mouse.x;
        anim.runtimeAnimatorController = animCon[(int)GameManager.instance.player.curPlayerWeapon];
        //if(anim.runtimeAnimatorController == animCon[(int)Player.playerWeapon.melee])
        //    anim.SetFloat("meleeAnimSpeed", GameManager.instance.playerAtk.meleeAnimSpeed);
    }
}
