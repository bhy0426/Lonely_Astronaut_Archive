using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletData myBulletData;

    void Awake()
    {
        switch (GameManager.instance.player.curPlayerWeapon)
        {
            case Player.playerWeapon.handGun:
                myBulletData = GameManager.instance.playerAtk.bulletData[0];
                break;
            case Player.playerWeapon.sg:
                myBulletData = GameManager.instance.playerAtk.bulletData[1];
                break;
            case Player.playerWeapon.rifle:
                myBulletData = GameManager.instance.playerAtk.bulletData[2];
                break;
            case Player.playerWeapon.sr:
                myBulletData = GameManager.instance.playerAtk.bulletData[3];
                break;
        }
    }
}

