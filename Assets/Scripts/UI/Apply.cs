using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apply : MonoBehaviour
{
    public ChooseUpgrade chooseUpgrade;
    public GameObject chooseMaster;
    public GameObject OXUI;

    public void Accept()
    {
        ApplyUpgrade(chooseUpgrade.upgrade);
        OXUI.SetActive(false);
        Time.timeScale = 1f;
        chooseUpgrade.gameObject.SetActive(false);
        GameManager.instance.player.isLevelUp = false;
        if(GameManager.instance.player.meleeMasterLevel == 10)
        {
            chooseMaster.SetActive(true);
            chooseMaster.GetComponent<ChooseMaster>().Init(Player.playerWeapon.melee);
            Time.timeScale = 1f;
        }
    }

    public void ApplyUpgrade(int index)
    {
        //Debug.Log("적용된 업그레이드 번호 " + index.ToString());
        switch (index)
        {
            // Public
            case (int)Upgrade.UpgradeList.AddHealth:
            case (int)Upgrade.UpgradeList.AddHealth_shot:
                GameManager.instance.player.maxHealth += 10f;
                GameManager.instance.player.addHealthLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.AddSpeed:
            case (int)Upgrade.UpgradeList.AddSpeed_shot:
                GameManager.instance.player.speedValue += 0.05f;
                GameManager.instance.player.addSpeedLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.AddExpChance:
            case (int)Upgrade.UpgradeList.AddExpChance_shot:
                // 1레벨 : 1개 90퍼, 2개 10퍼;
                // 2레벨 : 1개 70퍼, 2개 20퍼, 3개 10퍼;
                // 3레벨 : 1개 30퍼, 2개 40퍼, 3개 30퍼;
                // 4레벨 : 1개  0퍼, 2개 50퍼, 3개 30퍼, 4개 20퍼;
                // 5레벨 : 1개  0퍼, 2개  0퍼, 3개 50퍼, 4개 20퍼;
                GameManager.instance.player.addExpChanceLevel += 1;
                //Debug.Log("경험치 드랍률 증가");
                break;
            case (int)Upgrade.UpgradeList.AddItemChance:
            case (int)Upgrade.UpgradeList.AddItemChance_shot:
                GameManager.instance.player.addItemChanceLevel += 1;
                //Debug.Log("아이템 드랍률 증가");
                break;
            case (int)Upgrade.UpgradeList.AddAmmoChance:
            case (int)Upgrade.UpgradeList.AddAmmoChance_shot:
                GameManager.instance.player.addAmmoChanceLevel += 1;
                //Debug.Log("탄약 드랍률 증가");
                break;

            // Melee
            case (int)Upgrade.UpgradeList.MeleeDamage:
            case (int)Upgrade.UpgradeList.MeleeDamage_shot:
                GameManager.instance.playerAtk.meleeDamage += 1 + GameManager.instance.player.meleeDamageLevel;
                GameManager.instance.player.meleeDamageLevel += 1;
                GameManager.instance.player.meleeMasterLevel += 1;
                //Debug.Log("MeleeDamage");
                break;
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed:
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed_shot:
                GameManager.instance.playerAtk.meleeCooldown -= 0.05f;
                //GameManager.instance.playerAtk.meleeAnimSpeed *= 1.05f;
                GameManager.instance.player.meleeAtkSpeedLevel += 1;
                GameManager.instance.player.meleeMasterLevel += 1;
                //Debug.Log("MeleeAtkSpeed");
                break;
            case (int)Upgrade.UpgradeList.MeleeCritChance:
            case (int)Upgrade.UpgradeList.MeleeCritChance_shot:
                GameManager.instance.playerAtk.meleeCritChance += 5f;
                GameManager.instance.player.meleeCritChanceLevel += 1;
                GameManager.instance.player.meleeMasterLevel += 1;
                //Debug.Log("MeleeCritChance");
                break;
            case (int)Upgrade.UpgradeList.MeleeCritDamage:
            case (int)Upgrade.UpgradeList.MeleeCritDamage_shot:
                GameManager.instance.playerAtk.meleeCritDamage += 0.1f;
                GameManager.instance.player.meleeCritDamageLevel += 1;
                GameManager.instance.player.meleeMasterLevel += 1;
                //Debug.Log("MeleeCritDamage");
                break;
            case (int)Upgrade.UpgradeList.MeleeSize:
            case (int)Upgrade.UpgradeList.MeleeSize_shot:
                GameManager.instance.playerAtk.meleeSize += 0.15f;
                GameManager.instance.player.meleeSizeLevel += 1;
                GameManager.instance.player.meleeMasterLevel += 1;
                //GameManager.instance.hand.transform.localScale *= GameManager.instance.playerAtk.meleeSize;
                //Debug.Log("MeleeSize");
                break;

            // Handgun
            case (int)Upgrade.UpgradeList.HgDamage:
                GameManager.instance.playerAtk.bulletData[0].damage += 2f;
                GameManager.instance.player.HgDamageLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.HgAtkSpeed:
                GameManager.instance.playerAtk.handgunCooldown -= 0.005f;
                GameManager.instance.player.HgAtkSpeedLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmoChance: // 탄약 프리팹 획득시 얻는 탄의 개수 증가
                GameManager.instance.playerAtk.handgunAmmoChance += 5f;
                GameManager.instance.player.HgExtraAmmoChanceLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmo: // 최대 탄창 증가
                GameManager.instance.playerAtk.weaponMaxAmmo[(int)Player.playerWeapon.handGun] += 2;
                GameManager.instance.player.HgExtraAmmoLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.HgCritChance:
                GameManager.instance.playerAtk.bulletData[0].critChance += 5f;
                GameManager.instance.player.HgCritChanceLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.HgCritDamage:
                GameManager.instance.playerAtk.bulletData[0].critDamage += 0.5f;
                GameManager.instance.player.HgCritDamageLevel += 1;
                GameManager.instance.player.hgMasterLevel += 1;
                break;

            // Shotgun
            case (int)Upgrade.UpgradeList.SgDamage:
            case (int)Upgrade.UpgradeList.SgDamage_shot:
                GameManager.instance.playerAtk.bulletData[1].damage += 0.5f;
                GameManager.instance.player.SgDamageLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SgAtkSpeed:
            case (int)Upgrade.UpgradeList.SgAtkSpeed_shot:
                GameManager.instance.playerAtk.sgCooldown -= 0.1f;
                GameManager.instance.player.SgAtkSpeedLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance_shot:
                GameManager.instance.playerAtk.shotgunAmmoChance += 5f;
                GameManager.instance.player.SgExtraAmmoChanceLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmo:
            case (int)Upgrade.UpgradeList.SgExtraAmmo_shot:
                GameManager.instance.playerAtk.weaponMaxAmmo[(int)Player.playerWeapon.sg] += 2;
                GameManager.instance.player.SgExtraAmmoLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SgCritChance:
            case (int)Upgrade.UpgradeList.SgCritChance_shot:
                GameManager.instance.playerAtk.bulletData[1].critChance += 5f;
                GameManager.instance.player.SgCritChanceLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SgCritDamage:
            case (int)Upgrade.UpgradeList.SgCritDamage_shot:
                GameManager.instance.playerAtk.bulletData[1].critDamage += 0.5f;
                GameManager.instance.player.SgCritDamageLevel += 1;
                GameManager.instance.player.sgMasterLevel += 1;
                break;

            // Rifle
            case (int)Upgrade.UpgradeList.ArDamage:
                GameManager.instance.playerAtk.bulletData[2].damage += 1f;
                GameManager.instance.player.ArDamageLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.ArAtkSpeed:
                GameManager.instance.playerAtk.rifleCooldown -= 0.01f;
                GameManager.instance.player.ArAtkSpeedLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmoChance:
                GameManager.instance.playerAtk.rifleAmmoChance += 5f;
                GameManager.instance.player.ArExtraAmmoChanceLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmo:
                GameManager.instance.playerAtk.weaponMaxAmmo[(int)Player.playerWeapon.rifle] += 10;
                GameManager.instance.player.ArExtraAmmoLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.ArCritChance:
                GameManager.instance.playerAtk.bulletData[2].critChance += 5f;
                GameManager.instance.player.ArCritChanceLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.ArCritDamage:
                GameManager.instance.playerAtk.bulletData[2].critDamage += 0.5f;
                GameManager.instance.player.ArCritDamageLevel += 1;
                GameManager.instance.player.arMasterLevel += 1;
                break;

            // Sniper
            case (int)Upgrade.UpgradeList.SrDamage:
            case (int)Upgrade.UpgradeList.SrDamage_shot:
                GameManager.instance.playerAtk.bulletData[3].damage += 4f;
                GameManager.instance.player.SrDamageLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SrAtkSpeed:
            case (int)Upgrade.UpgradeList.SrAtkSpeed_shot:
                GameManager.instance.playerAtk.srCooldown -= 0.25f;
                GameManager.instance.player.SrAtkSpeedLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance_shot:
                GameManager.instance.playerAtk.sniperAmmoChance += 5f;
                GameManager.instance.player.SrExtraAmmoChanceLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmo:
            case (int)Upgrade.UpgradeList.SrExtraAmmo_shot:
                GameManager.instance.playerAtk.weaponMaxAmmo[(int)Player.playerWeapon.rifle] += 2;
                GameManager.instance.player.SrExtraAmmoLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SrCritChance:
            case (int)Upgrade.UpgradeList.SrCritChance_shot:
                GameManager.instance.playerAtk.bulletData[3].critChance += 5f;
                GameManager.instance.player.SrCritChanceLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
            case (int)Upgrade.UpgradeList.SrCritDamage:
            case (int)Upgrade.UpgradeList.SrCritDamage_shot:
                GameManager.instance.playerAtk.bulletData[3].critDamage += 0.5f;
                GameManager.instance.player.SrCritDamageLevel += 1;
                GameManager.instance.player.srMasterLevel += 1;
                break;
        }
    }
}
