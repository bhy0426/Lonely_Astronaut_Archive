using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseUpgrade : MonoBehaviour
{
    [SerializeField]
    private int[] choose;
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;
    public TextMeshProUGUI thirdText;
    public TextMeshProUGUI upgradeTitle;
    public TextMeshProUGUI upgradeInfo;
    public Image firstImage;
    public Image secondImage;
    public Image thirdImage;
    public Sprite[] sprUpgrade;
    public int upgrade;

    private void Update()
    {
        ChangeString(ref upgradeTitle, ref upgradeInfo, upgrade);
    }
    private void OnEnable()
    {
        Init();
        ChangeImage(ref firstImage, choose[0]);
        ChangeImage(ref secondImage, choose[1]);
        ChangeImage(ref thirdImage, choose[2]);
    }

    void Init()
    {
        int min = 0, max = 0;
        if (GameManager.instance.player.level >= 1 && GameManager.instance.player.level < 5)
        {
            min = (int)Upgrade.UpgradeList.AddHealth;
            max = (int)Upgrade.UpgradeList.MeleeSize + 1;
        }
        else if (GameManager.instance.player.level >= 5 && GameManager.instance.player.level < 15)
        {
            // 권총만 가지고 있을 경우
            if (GameManager.instance.player.hasHandgun && !GameManager.instance.player.hasShotgun)
            {
                min = (int)Upgrade.UpgradeList.AddHealth;
                max = (int)Upgrade.UpgradeList.HgCritDamage + 1;
            }
            // 샷건만 가지고 있을 경우
            else if (!GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                min = (int)Upgrade.UpgradeList.AddHealth_shot;
                max = (int)Upgrade.UpgradeList.SgCritDamage_shot + 1;
            }
            // 둘다 있을 경우
            else if (GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                min = (int)Upgrade.UpgradeList.AddHealth;
                max = (int)Upgrade.UpgradeList.SgCritDamage + 1;
            }
        }
        else if (GameManager.instance.player.level >= 15 && GameManager.instance.player.level < 99)
        {
            // 라이플만 가지고 있을 경우
            if (GameManager.instance.player.hasRifle && !GameManager.instance.player.hasSniper)
            {
                min = (int)Upgrade.UpgradeList.AddHealth;
                max = (int)Upgrade.UpgradeList.ArCritDamage + 1;
            }
            // 스나이퍼만 가지고 있을 경우
            else if (!GameManager.instance.player.hasRifle && GameManager.instance.player.hasSniper)
            {
                min = (int)Upgrade.UpgradeList.AddHealth_shot;
                max = (int)Upgrade.UpgradeList.SrCritDamage_shot + 1;
            }
            // 둘다 있을 경우
            else if (GameManager.instance.player.hasRifle && GameManager.instance.player.hasSniper)
            {
                min = (int)Upgrade.UpgradeList.AddHealth;
                max = (int)Upgrade.UpgradeList.SrCritDamage + 1;
            }
        }
            //else if (GameManager.instance.player.level >= 12 && GameManager.instance.player.level < 15)
            //{
            //    if (GameManager.instance.player.hasRifle)
            //    {
            //        min = 0;
            //        max = 23;
            //    }
            //    else
            //    {
            //        min = 0;
            //        max = 17;
            //    }
            //}
        choose[0] = 99;
        choose[1] = 99;
        choose[2] = 99;
        upgrade = 99;

        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                choose[i] = Random.Range(min, max);
                int count = 0;
                for(int k = 0; k < 3; k++)
                {
                    if (choose[i] == choose[k])
                        count++;
                }
                if (count < 2)
                    break;
            }
        }
    }

    public void Choose()
    {
        if (first.GetComponent<Upgrade>().isSelect)
        {
            upgrade = choose[0];
        }
        else if (second.GetComponent<Upgrade>().isSelect)
        {
            upgrade = choose[1];
        }
        else if (third.GetComponent<Upgrade>().isSelect)
        {
            upgrade = choose[2];
        }
    }

    void ChangeImage(ref Image UIImage, int index)
    {
        switch (index)
        {
            // Public
            case (int)Upgrade.UpgradeList.AddHealth:
            case (int)Upgrade.UpgradeList.AddHealth_shot:
                UIImage.sprite = sprUpgrade[0];
                //UItext.text = "최대체력" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.AddSpeed:
            case (int)Upgrade.UpgradeList.AddSpeed_shot:
                UIImage.sprite = sprUpgrade[1];
                //UItext.text = "이동속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.AddExpChance:
            case (int)Upgrade.UpgradeList.AddExpChance_shot:
                UIImage.sprite = sprUpgrade[2];
                //UItext.text = "경험치" + "\n" + "드랍률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.AddItemChance:
            case (int)Upgrade.UpgradeList.AddItemChance_shot:
                UIImage.sprite = sprUpgrade[3];
                //UItext.text = "아이템" + "\n" + "드랍률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.AddAmmoChance:
            case (int)Upgrade.UpgradeList.AddAmmoChance_shot:
                UIImage.sprite = sprUpgrade[4];
                //UItext.text = "탄약" + "\n" + "드랍률" + "\n" + "증가";
                break;

            // Melee
            case (int)Upgrade.UpgradeList.MeleeDamage:
            case (int)Upgrade.UpgradeList.MeleeDamage_shot:
                UIImage.sprite = sprUpgrade[5];
                //UItext.text = "근접무기" + "\n" + "데미지" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed:
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed_shot:
                UIImage.sprite = sprUpgrade[6];
                //UItext.text = "근접무기" + "\n" + "공격속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.MeleeCritChance:
            case (int)Upgrade.UpgradeList.MeleeCritChance_shot:
                UIImage.sprite = sprUpgrade[7];
                //UItext.text = "근접무기" + "\n" + "치명타확률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.MeleeCritDamage:
            case (int)Upgrade.UpgradeList.MeleeCritDamage_shot:
                UIImage.sprite = sprUpgrade[8];
                //UItext.text = "근접무기" + "\n" + "치명타피해" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.MeleeSize:
            case (int)Upgrade.UpgradeList.MeleeSize_shot:
                UIImage.sprite = sprUpgrade[9];
                //UItext.text = "근접무기" + "\n" + "크기 증가";
                break;

            // Handgun
            case (int)Upgrade.UpgradeList.HgDamage:
                UIImage.sprite = sprUpgrade[10];
                //UItext.text = "권총" + "\n" + "데미지" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.HgAtkSpeed:
                UIImage.sprite = sprUpgrade[11];
                //UItext.text = "권총" + "\n" + "공격속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmoChance:
                UIImage.sprite = sprUpgrade[12];
                //UItext.text = "권총" + "\n" + "탄약획득개수" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmo:
                UIImage.sprite = sprUpgrade[13];
                //UItext.text = "권총" + "\n" + "탄창크기" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.HgCritChance:
                UIImage.sprite = sprUpgrade[14];
                //UItext.text = "권총" + "\n" + "치명타확률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.HgCritDamage:
                UIImage.sprite = sprUpgrade[15];
                //UItext.text = "권총" + "\n" + "치명타피해" + "\n" + "증가";
                break;

            // Shotgun
            case (int)Upgrade.UpgradeList.SgDamage:
            case (int)Upgrade.UpgradeList.SgDamage_shot:
                UIImage.sprite = sprUpgrade[16];
                //UItext.text = "샷건" + "\n" + "데미지" + "\n" + "증가"; 
                break;
            case (int)Upgrade.UpgradeList.SgAtkSpeed:
            case (int)Upgrade.UpgradeList.SgAtkSpeed_shot:
                UIImage.sprite = sprUpgrade[17];
                //UItext.text = "샷건" + "\n" + "공격속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance_shot:
                UIImage.sprite = sprUpgrade[18];
                //UItext.text = "샷건" + "\n" + "탄약획득개수" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmo:
            case (int)Upgrade.UpgradeList.SgExtraAmmo_shot:
                UIImage.sprite = sprUpgrade[19];
                //UItext.text = "샷건" + "\n" + "탄창크기" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SgCritChance:
            case (int)Upgrade.UpgradeList.SgCritChance_shot:
                UIImage.sprite = sprUpgrade[20];
                //UItext.text = "샷건" + "\n" + "치명타확률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SgCritDamage:
            case (int)Upgrade.UpgradeList.SgCritDamage_shot:
                UIImage.sprite = sprUpgrade[21];
                //UItext.text = "샷건" + "\n" + "치명타피해" + "\n" + "증가";
                break;

            // Rifle
            case (int)Upgrade.UpgradeList.ArDamage:
                UIImage.sprite = sprUpgrade[22];
                //UItext.text = "라이플" + "\n" + "데미지" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.ArAtkSpeed:
                UIImage.sprite = sprUpgrade[23];
                //UItext.text = "라이플" + "\n" + "공격속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmoChance:
                UIImage.sprite = sprUpgrade[24];
                //UItext.text = "라이플" + "\n" + "탄약획득개수" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmo:
                UIImage.sprite = sprUpgrade[25];
                //UItext.text = "라이플" + "\n" + "탄창크기" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.ArCritChance:
                UIImage.sprite = sprUpgrade[26];
                //UItext.text = "라이플" + "\n" + "치명타확률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.ArCritDamage:
                UIImage.sprite = sprUpgrade[27];
                //UItext.text = "라이플" + "\n" + "치명타피해" + "\n" + "증가";
                break;

            // Sniper
            case (int)Upgrade.UpgradeList.SrDamage:
            case (int)Upgrade.UpgradeList.SrDamage_shot:
                UIImage.sprite = sprUpgrade[28];
                //UItext.text = "스나이퍼" + "\n" + "데미지" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SrAtkSpeed:
            case (int)Upgrade.UpgradeList.SrAtkSpeed_shot:
                UIImage.sprite = sprUpgrade[29];
                //UItext.text = "스나이퍼" + "\n" + "공격속도" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance_shot:
                UIImage.sprite = sprUpgrade[30];
                //UItext.text = "스나이퍼" + "\n" + "탄약획득개수" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmo:
            case (int)Upgrade.UpgradeList.SrExtraAmmo_shot:
                UIImage.sprite = sprUpgrade[31];
                //UItext.text = "스나이퍼" + "\n" + "탄창크기" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SrCritChance:
            case (int)Upgrade.UpgradeList.SrCritChance_shot:
                UIImage.sprite = sprUpgrade[32];
                //UItext.text = "스나이퍼" + "\n" + "치명타확률" + "\n" + "증가";
                break;
            case (int)Upgrade.UpgradeList.SrCritDamage:
            case (int)Upgrade.UpgradeList.SrCritDamage_shot:
                UIImage.sprite = sprUpgrade[33];
                //UItext.text = "스나이퍼" + "\n" + "치명타피해" + "\n" + "증가";
                break;
        }
    }
    void ChangeString(ref TextMeshProUGUI UITitle, ref TextMeshProUGUI UIInfo, int index)
    {
        switch (index)
        {
            // Public
            case (int)Upgrade.UpgradeList.AddHealth:
            case (int)Upgrade.UpgradeList.AddHealth_shot:
                //UIImage.sprite = sprUpgrade[0];
                UITitle.text = "최대 체력 증가";
                UIInfo.text = "플레이어의 최대 체력을 10 증가시킵니다.(현재 " + GameManager.instance.player.maxHealth.ToString() + ")";
                break;
            case (int)Upgrade.UpgradeList.AddSpeed:
            case (int)Upgrade.UpgradeList.AddSpeed_shot:
                //UIImage.sprite = sprUpgrade[1];
                UITitle.text = "이동속도 증가";
                UIInfo.text = "플레이어의 이동 속도를 5% 증가시킵니다.(현재 " + (100 * GameManager.instance.player.speedValue).ToString() + "%)";
                break;
            case (int)Upgrade.UpgradeList.AddExpChance:
            case (int)Upgrade.UpgradeList.AddExpChance_shot:
                //UIImage.sprite = sprUpgrade[2];
                UITitle.text = "경험치 드랍률 증가";
                UIInfo.text = "플레이어의 경험치 드랍률을 1레벨 증가시킵니다.(현재 " + GameManager.instance.player.addExpChanceLevel.ToString() + "레벨)";
                break;
            case (int)Upgrade.UpgradeList.AddItemChance:
            case (int)Upgrade.UpgradeList.AddItemChance_shot:
                //UIImage.sprite = sprUpgrade[3];
                UITitle.text = "아이템 드랍률 증가";
                UIInfo.text = "플레이어의 아이템 드랍률을 2.5% 증가시킵니다.(현재 " + (2.5f + 2.5f * GameManager.instance.player.addItemChanceLevel * 5).ToString() + "%)";
                break;
            case (int)Upgrade.UpgradeList.AddAmmoChance:
            case (int)Upgrade.UpgradeList.AddAmmoChance_shot:
                //UIImage.sprite = sprUpgrade[4];
                UITitle.text = "탄약 드랍률 증가";
                UIInfo.text = "플레이어의 탄약 드랍률을 5% 증가시킵니다.(현재 " + (10 + 5 * GameManager.instance.player.addAmmoChanceLevel).ToString() + "%)";
                break;

            // Melee
            case (int)Upgrade.UpgradeList.MeleeDamage:
            case (int)Upgrade.UpgradeList.MeleeDamage_shot:
                //UIImage.sprite = sprUpgrade[5];
                UITitle.text = "근접무기 데미지 증가";
                UIInfo.text = "플레이어의 근접무기 데미지를 " + (1 + GameManager.instance.player.meleeDamageLevel).ToString() + "증가시킵니다.(현재 " + GameManager.instance.playerAtk.meleeDamage.ToString() + ")";
                break;
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed:
            case (int)Upgrade.UpgradeList.MeleeAtkSpeed_shot:
                //UIImage.sprite = sprUpgrade[6];
                UITitle.text = "근접무기 공격 속도 증가";
                UIInfo.text = "플레이어의 근접무기 공격 속도를 0.05 감소시킵니다.(현재 " + GameManager.instance.playerAtk.meleeCooldown.ToString() + ")";
                break;
            case (int)Upgrade.UpgradeList.MeleeCritChance:
            case (int)Upgrade.UpgradeList.MeleeCritChance_shot:
                //UIImage.sprite = sprUpgrade[7];
                UITitle.text = "근접무기 치명타 확률 증가";
                UIInfo.text = "플레이어의 근접무기 치명타 확률을 5% 증가시킵니다.(현재 " + GameManager.instance.playerAtk.meleeCritChance.ToString() + "%)";
                break;
            case (int)Upgrade.UpgradeList.MeleeCritDamage:
            case (int)Upgrade.UpgradeList.MeleeCritDamage_shot:
                //UIImage.sprite = sprUpgrade[8];
                UITitle.text = "근접무기 치명타 피해 증가"; // 두 줄로 나옴 
                UIInfo.text = "플레이어의 근접무기 치명타 피해를 10% 증가시킵니다.(현재 " + (100 * GameManager.instance.playerAtk.meleeCritDamage).ToString() + "%)";
                break;
            case (int)Upgrade.UpgradeList.MeleeSize:
            case (int)Upgrade.UpgradeList.MeleeSize_shot:
                //UIImage.sprite = sprUpgrade[9];
                UITitle.text = "근접무기 크기 증가";
                UIInfo.text = "플레이어의 근접무기 크기를 15% 증가시킵니다.(현재 " + (100 * GameManager.instance.playerAtk.meleeSize).ToString() + "%)";
                break;

            // Handgun
            case (int)Upgrade.UpgradeList.HgDamage:
                //UIImage.sprite = sprUpgrade[10];
                UITitle.text = "권총 데미지 증가";
                UIInfo.text = "플레이어의 권총 데미지를 2 증가시킵니다.(현재 " + GameManager.instance.playerAtk.bulletData[0].damage.ToString() + ")";
                break;
            case (int)Upgrade.UpgradeList.HgAtkSpeed:
                //UIImage.sprite = sprUpgrade[11];
                UITitle.text = "권총 공격 속도 증가";
                UIInfo.text = "플레이어의 권총 공격 속도를 0.05 감소시킵니다.(현재 " + GameManager.instance.playerAtk.handgunCooldown.ToString() + ")"; 
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmoChance:
                //UIImage.sprite = sprUpgrade[12];
                UITitle.text = "권총 탄약 획득 개수 증가";
                UIInfo.text = "플레이어의 권총 탄약 획득시 획득 개수를 2발 증가시킵니다.(현재 " + (11 + (2 * GameManager.instance.player.HgExtraAmmoChanceLevel)).ToString() + "발)";
                break;
            case (int)Upgrade.UpgradeList.HgExtraAmmo:
                //UIImage.sprite = sprUpgrade[13];
                UITitle.text = "권총 탄창 크기 증가";
                UIInfo.text = "플레이어의 권총 탄약 획득시 획득 개수를 2발 증가시킵니다.(현재 " + GameManager.instance.playerAtk.weaponMaxAmmo[(int)Player.playerWeapon.handGun] + "발)";
                break;
            case (int)Upgrade.UpgradeList.HgCritChance:
                //UIImage.sprite = sprUpgrade[14];
                UITitle.text = "권총 치명타 확률 증가";
                break;
            case (int)Upgrade.UpgradeList.HgCritDamage:
                //UIImage.sprite = sprUpgrade[15];
                UITitle.text = "권총 치명타 피해 증가";
                break;

            // Shotgun
            case (int)Upgrade.UpgradeList.SgDamage:
            case (int)Upgrade.UpgradeList.SgDamage_shot:
                //UIImage.sprite = sprUpgrade[16];
                UITitle.text = "샷건 데미지 증가"; 
                break;
            case (int)Upgrade.UpgradeList.SgAtkSpeed:
            case (int)Upgrade.UpgradeList.SgAtkSpeed_shot:
                //UIImage.sprite = sprUpgrade[17];
                UITitle.text = "샷건 공격 속도 증가";
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SgExtraAmmoChance_shot:
                //UIImage.sprite = sprUpgrade[18];
                UITitle.text = "샷건 탄약 획득 개수 증가";
                break;
            case (int)Upgrade.UpgradeList.SgExtraAmmo:
            case (int)Upgrade.UpgradeList.SgExtraAmmo_shot:
                //UIImage.sprite = sprUpgrade[19];
                UITitle.text = "샷건 탄창 크기 증가";
                break;
            case (int)Upgrade.UpgradeList.SgCritChance:
            case (int)Upgrade.UpgradeList.SgCritChance_shot:
                //UIImage.sprite = sprUpgrade[20];
                UITitle.text = "샷건 치명타 확률 증가";
                break;
            case (int)Upgrade.UpgradeList.SgCritDamage:
            case (int)Upgrade.UpgradeList.SgCritDamage_shot:
                //UIImage.sprite = sprUpgrade[21];
                UITitle.text = "샷건 치명타 피해 증가";
                break;

            // Rifle
            case (int)Upgrade.UpgradeList.ArDamage:
                //UIImage.sprite = sprUpgrade[22];
                UITitle.text = "라이플 데미지 증가";
                break;
            case (int)Upgrade.UpgradeList.ArAtkSpeed:
                //UIImage.sprite = sprUpgrade[23];
                UITitle.text = "라이플 공격 속도 증가";
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmoChance:
                //UIImage.sprite = sprUpgrade[24];
                UITitle.text = "라이플 탄약 획득 개수 증가";
                break;
            case (int)Upgrade.UpgradeList.ArExtraAmmo:
                //UIImage.sprite = sprUpgrade[25];
                UITitle.text = "라이플 탄창 크기 증가";
                break;
            case (int)Upgrade.UpgradeList.ArCritChance:
                //UIImage.sprite = sprUpgrade[26];
                UITitle.text = "라이플 치명타 확률 증가";
                break;
            case (int)Upgrade.UpgradeList.ArCritDamage:
                //UIImage.sprite = sprUpgrade[27];
                UITitle.text = "라이플 치명타 피해 증가";
                break;

            // Sniper
            case (int)Upgrade.UpgradeList.SrDamage:
            case (int)Upgrade.UpgradeList.SrDamage_shot:
                //UIImage.sprite = sprUpgrade[28];
                UITitle.text = "스나이퍼 데미지 증가";
                break;
            case (int)Upgrade.UpgradeList.SrAtkSpeed:
            case (int)Upgrade.UpgradeList.SrAtkSpeed_shot:
                //UIImage.sprite = sprUpgrade[29];
                UITitle.text = "스나이퍼 공격 속도 증가";
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance:
            case (int)Upgrade.UpgradeList.SrExtraAmmoChance_shot:
                //UIImage.sprite = sprUpgrade[30];
                UITitle.text = "스나이퍼 탄약 획득 개수 증가";
                break;
            case (int)Upgrade.UpgradeList.SrExtraAmmo:
            case (int)Upgrade.UpgradeList.SrExtraAmmo_shot:
                //UIImage.sprite = sprUpgrade[31];
                UITitle.text = "스나이퍼 탄창 크기 증가";
                break;
            case (int)Upgrade.UpgradeList.SrCritChance:
            case (int)Upgrade.UpgradeList.SrCritChance_shot:
                //UIImage.sprite = sprUpgrade[32];
                UITitle.text = "스나이퍼 치명타 확률 증가";
                break;
            case (int)Upgrade.UpgradeList.SrCritDamage:
            case (int)Upgrade.UpgradeList.SrCritDamage_shot:
                //UIImage.sprite = sprUpgrade[33];
                UITitle.text = "스나이퍼 치명타 피해 증가";
                break;
            default:
                UITitle.text = "업그레이드를 선택하세요";
                UIInfo.text = "레벨이 오를 때마다 3가지 중 하나의 업그레이드를\n선택할 수 있습니다.";
                break;
        }
    }
}
