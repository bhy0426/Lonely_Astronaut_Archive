using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseButton : MonoBehaviour
{
    public GameObject OXUI;

    public float damage;
    public float critChance;
    public float critDamage;
    public float atkSpeed;
    public float extraAmmo;
    public float extraAmmoChance;

    enum Upgrade
    {
        MeleeDamage, MeleeAtkSpeed, MeleeCritChance, MeleeCritDamage, MeleeSize,
        HgDamage, HgAtkSpeed, HgExtraAmmo, HgExtraAmmoChance, HgCritChance, HgCritDamage,
        SgDamage, SgAtkSpeed, SgExtraAmmo, SgExtraAmmoChance, SgCritChance, SgCritDamage,
        ArDamage, ArAtkSpeed, ArExtraAmmo, ArExtraAmmoChance, ArCritChance, ArCritDamage
    };

    public int[] choose = new int[3];
    public int i = 0;

    private void OnEnable()
    {
        choose[0] = 99;
        choose[1] = 99;
        choose[2] = 99;
        Rand(0);
        Rand(1);
        Rand(2);
    }

    public void ChooseFirst()
    {
        i = 0;
        OXUI.SetActive(true);
        //Debug.Log("1. " + choose[0].ToString());
        //Debug.Log("i.. " + i);
    }

    public void ChooseSecond()
    {
        i = 1;
        OXUI.SetActive(true);
        //Debug.Log("2. " + choose[1].ToString());
        //Debug.Log("i.. " + i);
    }

    public void ChooseThird()
    {
        i = 2;
        OXUI.SetActive(true);
        //Debug.Log("3. " + choose[2].ToString());
        //Debug.Log("i.. " + i);
    }

    public void Accept()
    {
        Apply(choose[i]);
        OXUI.SetActive(false);
        GameManager.instance.player.isLevelUp = false;
    }

    public void Cancel()
    {
        OXUI.SetActive(false);
    }

    // 난수를 뽑고 배열에 저장
    public void Rand(int index)
    {
        int min = 0, max = 0, rand;
        if(GameManager.instance.player.level >= 1 && GameManager.instance.player.level < 5)
        {
            min = 0;
            max = 5;
        }
        else if (GameManager.instance.player.level >= 5 && GameManager.instance.player.level < 16)
        {
            // 권총만 가지고 있을 경우
            if (GameManager.instance.player.hasHandgun && !GameManager.instance.player.hasShotgun)
            {
                min = 0;
                max = 11;
            }
            // 샷건만 가지고 있을 경우
            else if (!GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                min = 23;
                max = 34;
            }
            // 둘다 있을 경우
            else if(GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                min = 0;
                max = 16;
            }
        }

        while (true)
        {
            rand = Random.Range(min, max);
            int num = 0;
            for (int j = 0; j < 3; j++)
            {
                if (choose[j] == rand)
                {
                    num++;
                }
            }
            if (num == 0)
                break;
        }
        choose[index] = rand;
    }

    // 입력받은 upgrade[i]값에 맞는 업그레이드 적용
    public void Apply(int indexx)
    {
        //Debug.Log("적용된 업그레이드 번호 "+indexx.ToString());
        switch(indexx)
        {
            // Melee
            case (int)Upgrade.MeleeDamage:
                GameManager.instance.playerAtk.meleeDamage += 1f;
                //Debug.Log("MeleeDamage");
                break;
            case (int)Upgrade.MeleeAtkSpeed:
                GameManager.instance.playerAtk.meleeCooldown -= 0.1f;
                //Debug.Log("MeleeAtkSpeed");
                break;
            case (int)Upgrade.MeleeCritChance:
                GameManager.instance.playerAtk.meleeCritChance += 5f;
                //Debug.Log("MeleeCritChance");
                break;
            case (int)Upgrade.MeleeCritDamage:
                GameManager.instance.playerAtk.meleeCritDamage += 0.5f;
                //Debug.Log("MeleeCritDamage");
                break;
            case (int)Upgrade.MeleeSize:
                GameManager.instance.hand.transform.localScale = Vector3.one * 2f;
                //Debug.Log("MeleeSize");
                break;

            // Handgun
            case (int)Upgrade.HgDamage:
                //Debug.Log("HgDamage");
                break;
            case (int)Upgrade.HgAtkSpeed:
                //Debug.Log("HgAtkSpeed");
                break;
            case (int)Upgrade.HgExtraAmmoChance:
                //Debug.Log("HgExtraAmmoChance");
                break;
            case (int)Upgrade.HgExtraAmmo:
                //Debug.Log("HgExtraAmmo");
                break;
            case (int)Upgrade.HgCritChance:
                //Debug.Log("HgCritChance");
                break;
            case (int)Upgrade.HgCritDamage:
                //Debug.Log("HgCritDamage");
                break;

            // Shotgun
            case (int)Upgrade.SgDamage:
                //Debug.Log("SgDamage");
                break;
            case (int)Upgrade.SgAtkSpeed:
                //Debug.Log("SgAtkSpeed");
                break;
            case (int)Upgrade.SgExtraAmmoChance:
                //Debug.Log("SgExtraAmmoChance");
                break;
            case (int)Upgrade.SgExtraAmmo:
                //Debug.Log("SgExtraAmmo");
                break;
            case (int)Upgrade.SgCritChance:
                //Debug.Log("SgCritChance");
                break;
            case (int)Upgrade.SgCritDamage:
                //Debug.Log("SgCritDamage");
                break;

            // Rifle
            case (int)Upgrade.ArDamage:
                //Debug.Log("ArDamage");
                break;
            case (int)Upgrade.ArAtkSpeed:
                //Debug.Log("ArAtkSpeed");
                break;
            case (int)Upgrade.ArExtraAmmoChance:
                //Debug.Log("ArExtraAmmoChance");
                break;
            case (int)Upgrade.ArExtraAmmo:
                //Debug.Log("ArExtraAmmo");
                break;
            case (int)Upgrade.ArCritChance:
                //Debug.Log("ArCritChance");
                break;
            case (int)Upgrade.ArCritDamage:
                //Debug.Log("ArCritDamage");
                break;
            
            // Melee + Shotgun
            // Melee
            case 23:
                GameManager.instance.playerAtk.meleeDamage += 1f;
                //Debug.Log("MeleeDamage");
                break;
            case 24:
                //Debug.Log("MeleeAtkSpeed");
                break;
            case 25:
                //Debug.Log("MeleeCritChance");
                break;
            case 26:
                //Debug.Log("MeleeCritDamage");
                break;
            case 27:
                //Debug.Log("MeleeSize");
                break;

            // Shotgun
            case 28:
                //Debug.Log("SgDamage");
                break;
            case 29:
                //Debug.Log("SgAtkSpeed");
                break;
            case 30:
                //Debug.Log("SgExtraAmmoChance");
                break;
            case 31:
                //Debug.Log("SgExtraAmmo");
                break;
            case 32:
                //Debug.Log("SgCritChance");
                break;
            case 33:
                //Debug.Log("SgCritDamage");
                break;
        }
    }
}
