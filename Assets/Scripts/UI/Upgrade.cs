using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public ChooseUpgrade chooseUpgrade;
    public GameObject OXUI;
    public GameObject otherUpgradeOne;
    public GameObject otherUpgradeTwo;

    public bool isSelect;

    public enum UpgradeList{
        AddHealth, AddSpeed, AddExpChance, AddItemChance, AddAmmoChance, // 최대체력증가, 속도증가, 경험치 드랍률 증가, 아이템 드랍률 증가, 골드 드랍률 증가
        MeleeDamage, MeleeAtkSpeed, MeleeCritChance, MeleeCritDamage, MeleeSize,
        HgDamage, HgAtkSpeed, HgExtraAmmo, HgExtraAmmoChance, HgCritChance, HgCritDamage,
        SgDamage, SgAtkSpeed, SgExtraAmmo, SgExtraAmmoChance, SgCritChance, SgCritDamage,
        ArDamage, ArAtkSpeed, ArExtraAmmo, ArExtraAmmoChance, ArCritChance, ArCritDamage,
        SrDamage, SrAtkSpeed, SrExtraAmmo, SrExtraAmmoChance, SrCritChance, SrCritDamage,
        AddHealth_shot, AddSpeed_shot, AddExpChance_shot, AddItemChance_shot, AddAmmoChance_shot,
        MeleeDamage_shot, MeleeAtkSpeed_shot, MeleeCritChance_shot, MeleeCritDamage_shot, MeleeSize_shot,
        SgDamage_shot, SgAtkSpeed_shot, SgExtraAmmo_shot, SgExtraAmmoChance_shot, SgCritChance_shot, SgCritDamage_shot,
        SrDamage_shot, SrAtkSpeed_shot, SrExtraAmmo_shot, SrExtraAmmoChance_shot, SrCritChance_shot, SrCritDamage_shot,
    };

    public void Choose()
    {
        OXUI.SetActive(true);
        isSelect = true;
        otherUpgradeOne.GetComponent<Upgrade>().isSelect = false;
        otherUpgradeTwo.GetComponent<Upgrade>().isSelect = false;
        chooseUpgrade.Choose();
    }
}
