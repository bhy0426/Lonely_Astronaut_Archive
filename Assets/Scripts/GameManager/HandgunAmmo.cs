using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunAmmo : MonoBehaviour
{
    Rigidbody2D rigid;
    public Player target;
    public Sprite[] ammoSprite;
    SpriteRenderer spriter;
    int ammoCount, curAmmoType;
    float curTime;
    float maxTime = 0.25f;

    private void Awake()
    {
        if (GameManager.instance.player.level >= 3 && GameManager.instance.player.level < 12)
        {
            if (GameManager.instance.player.hasHandgun && !GameManager.instance.player.hasShotgun)
            {
                curAmmoType = 0;
            }
            else if (!GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                curAmmoType = 1;
            }
            else if (GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                curAmmoType = Random.Range(0, 2);
            }
        }
        else if(GameManager.instance.player.level >= 15 && GameManager.instance.player.level < 99)
        {
            curAmmoType = Random.Range(0, 3);
        }
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player;
        spriter = GetComponent<SpriteRenderer>();
        spriter.sprite = ammoSprite[curAmmoType];
        switch(curAmmoType)
        {
            case 0:
                ammoCount = 11 + (2 * GameManager.instance.player.HgExtraAmmoChanceLevel);
                break;
            case 1:
                ammoCount = 5 + (1 * GameManager.instance.player.SgExtraAmmoChanceLevel);
                break;
            case 2:
                ammoCount = 30 + (3 * GameManager.instance.player.ArExtraAmmoChanceLevel);
                break;
        }
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
            switch(curAmmoType)
            {
                case 0:
                    if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.handGun)
                        GameManager.instance.playerAtk.maxAmmo += ammoCount;
                    else
                        GameManager.instance.playerAtk.saveAmmoHg += ammoCount;
                    break;
                case 1:
                    if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.sg)
                        GameManager.instance.playerAtk.maxAmmo += ammoCount;
                    else
                        GameManager.instance.playerAtk.saveAmmoSg += ammoCount;
                    break;
                case 2:
                    if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.rifle)
                        GameManager.instance.playerAtk.maxAmmo += ammoCount;
                    else
                        GameManager.instance.playerAtk.saveAmmoAr += ammoCount;
                    break;
            }

        }
    }
}
