using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Image[] upgradeProgressImage;
    public Sprite[] upgradeProgressSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        upgradeProgressImage[0].sprite = upgradeProgressSprite[GameManager.instance.player.addHealthLevel];
        upgradeProgressImage[1].sprite = upgradeProgressSprite[GameManager.instance.player.addSpeedLevel];
        upgradeProgressImage[2].sprite = upgradeProgressSprite[GameManager.instance.player.addExpChanceLevel];
        upgradeProgressImage[3].sprite = upgradeProgressSprite[GameManager.instance.player.addItemChanceLevel];
        upgradeProgressImage[4].sprite = upgradeProgressSprite[GameManager.instance.player.addAmmoChanceLevel];

        upgradeProgressImage[5].sprite = upgradeProgressSprite[GameManager.instance.player.meleeDamageLevel];
        upgradeProgressImage[6].sprite = upgradeProgressSprite[GameManager.instance.player.meleeAtkSpeedLevel];
        upgradeProgressImage[7].sprite = upgradeProgressSprite[GameManager.instance.player.meleeCritChanceLevel];
        upgradeProgressImage[8].sprite = upgradeProgressSprite[GameManager.instance.player.meleeCritDamageLevel];
        upgradeProgressImage[9].sprite = upgradeProgressSprite[GameManager.instance.player.meleeSizeLevel];

        upgradeProgressImage[10].sprite = upgradeProgressSprite[GameManager.instance.player.HgDamageLevel];
        upgradeProgressImage[11].sprite = upgradeProgressSprite[GameManager.instance.player.HgAtkSpeedLevel];
        upgradeProgressImage[12].sprite = upgradeProgressSprite[GameManager.instance.player.HgExtraAmmoLevel];
        upgradeProgressImage[13].sprite = upgradeProgressSprite[GameManager.instance.player.HgExtraAmmoChanceLevel - 1];
        upgradeProgressImage[14].sprite = upgradeProgressSprite[GameManager.instance.player.HgCritChanceLevel];
        upgradeProgressImage[15].sprite = upgradeProgressSprite[GameManager.instance.player.HgCritDamageLevel];

        upgradeProgressImage[16].sprite = upgradeProgressSprite[GameManager.instance.player.SgDamageLevel];
        upgradeProgressImage[17].sprite = upgradeProgressSprite[GameManager.instance.player.SgAtkSpeedLevel];
        upgradeProgressImage[18].sprite = upgradeProgressSprite[GameManager.instance.player.SgExtraAmmoLevel];
        upgradeProgressImage[19].sprite = upgradeProgressSprite[GameManager.instance.player.SgExtraAmmoChanceLevel - 1];
        upgradeProgressImage[20].sprite = upgradeProgressSprite[GameManager.instance.player.SgCritChanceLevel];
        upgradeProgressImage[21].sprite = upgradeProgressSprite[GameManager.instance.player.SgCritDamageLevel];

        upgradeProgressImage[22].sprite = upgradeProgressSprite[GameManager.instance.player.ArDamageLevel];
        upgradeProgressImage[23].sprite = upgradeProgressSprite[GameManager.instance.player.ArAtkSpeedLevel];
        upgradeProgressImage[24].sprite = upgradeProgressSprite[GameManager.instance.player.ArExtraAmmoLevel];
        upgradeProgressImage[25].sprite = upgradeProgressSprite[GameManager.instance.player.ArExtraAmmoChanceLevel - 1];
        upgradeProgressImage[26].sprite = upgradeProgressSprite[GameManager.instance.player.ArCritChanceLevel];
        upgradeProgressImage[27].sprite = upgradeProgressSprite[GameManager.instance.player.ArCritDamageLevel];

        upgradeProgressImage[28].sprite = upgradeProgressSprite[GameManager.instance.player.SrDamageLevel];
        upgradeProgressImage[29].sprite = upgradeProgressSprite[GameManager.instance.player.SrAtkSpeedLevel];
        upgradeProgressImage[30].sprite = upgradeProgressSprite[GameManager.instance.player.SrExtraAmmoLevel];
        upgradeProgressImage[31].sprite = upgradeProgressSprite[GameManager.instance.player.SrExtraAmmoChanceLevel - 1];
        upgradeProgressImage[32].sprite = upgradeProgressSprite[GameManager.instance.player.SrCritChanceLevel];
        upgradeProgressImage[33].sprite = upgradeProgressSprite[GameManager.instance.player.SrCritDamageLevel];
        //upgradeProgressImage[1].text = "이동 속도 업글 레벨 : " + GameManager.instance.player.addSpeedLevel;
        //upgradeProgressImage[2].text = "경험치 드랍률 업글 레벨 : " + GameManager.instance.player.addExpChanceLevel;
        //upgradeProgressImage[3].text = "아이템 드랍률 업글 레벨 : " + GameManager.instance.player.addItemChanceLevel;
        //upgradeProgressImage[4].text = "골드 드랍률 업글 레벨 : " + GameManager.instance.player.addGoldChanceLevel;

        //upgradeProgressImage[5].text = "근접 무기 데미지 업글 레벨: " + GameManager.instance.player.meleeDamageLevel;
        //upgradeProgressImage[6].text = "근접 무기 공격 속도 업글 레벨 : " + GameManager.instance.player.meleeAtkSpeedLevel;
        //upgradeProgressImage[7].text = "근접 무기 치명타 확률 업글 레벨 : " + GameManager.instance.player.meleeCritChanceLevel;
        //upgradeProgressImage[8].text = "근접 무기 치명타 피해 업글 레벨 : " + GameManager.instance.player.meleeCritDamageLevel;
        //upgradeProgressImage[9].text = "근접 무기 크기 업글 레벨 : " + GameManager.instance.player.meleeSizeLevel;

        //upgradeProgressImage[10].text = "권총 데미지 업글 레벨 : " + GameManager.instance.player.HgDamageLevel;
        //upgradeProgressImage[11].text = "권총 공격 속도 업글 레벨 : " + GameManager.instance.player.HgAtkSpeedLevel;
        //upgradeProgressImage[12].text = "권총 탄창 업글 레벨 : " + GameManager.instance.player.HgExtraAmmoLevel;
        //upgradeProgressImage[13].text = "권총 탄약 드랍률 업글 레벨 : " + GameManager.instance.player.HgExtraAmmoChanceLevel;
        //upgradeProgressImage[14].text = "권총 치명타 확률 업글 레벨 : " + GameManager.instance.player.HgCritChanceLevel;
        //upgradeProgressImage[15].text = "권총 치명타 피해 업글 레벨 : " + GameManager.instance.player.HgCritDamageLevel;

        //upgradeProgressImage[16].text = "샷건 데미지 업글 레벨 : " + GameManager.instance.player.SgDamageLevel;
        //upgradeProgressImage[17].text = "샷건 공격 속도 업글 레벨 : " + GameManager.instance.player.SgAtkSpeedLevel;
        //upgradeProgressImage[18].text = "샷건 탄창 업글 레벨 : " + GameManager.instance.player.SgExtraAmmoLevel;
        //upgradeProgressImage[19].text = "샷건 탄약 드랍률 업글 레벨 : " + GameManager.instance.player.SgExtraAmmoChanceLevel;
        //upgradeProgressImage[20].text = "샷건 치명타 확률 업글 레벨 : " + GameManager.instance.player.SgCritChanceLevel;
        //upgradeProgressImage[21].text = "샷건 치명타 피해 업글 레벨 : " + GameManager.instance.player.SgCritDamageLevel;

        //upgradeProgressImage[22].text = "라이플 데미지 업글 레벨 : " + GameManager.instance.player.ArDamageLevel;
        //upgradeProgressImage[24].text = "라이플 공격 속도 업글 레벨 : " + GameManager.instance.player.ArAtkSpeedLevel;
        //upgradeProgressImage[24].text = "라이플 탄창 업글 레벨 : " + GameManager.instance.player.ArExtraAmmoLevel;
        //upgradeProgressImage[25].text = "라이플 탄약 드랍률 업글 레벨 : " + GameManager.instance.player.ArExtraAmmoChanceLevel;
        //upgradeProgressImage[26].text = "라이플 치명타 확률 업글 레벨 : " + GameManager.instance.player.ArCritChanceLevel;
        //upgradeProgressImage[27].text = "라이플 치명타 피해 업글 레벨 : " + GameManager.instance.player.ArCritDamageLevel;
        //upgradeProgressImage[28].text = "라이플 반동 감소 업글 레벨 : ";
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
