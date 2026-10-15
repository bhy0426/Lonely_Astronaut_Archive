using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseMaster : MonoBehaviour
{
    public GameObject chooseMaster;
    public GameObject masterOX;
    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;
    public TextMeshProUGUI thirdText;

    public int select;
    public Player.playerWeapon wepType;

    public void Init(Player.playerWeapon type)
    {
        switch(type)
        {
            case Player.playerWeapon.melee:
                wepType = Player.playerWeapon.melee;
                firstText.text = "도끼";
                secondText.text = "방패";
                thirdText.text = "???";
                GameManager.instance.player.meleeMasterLevel = 0;
                break;
            case Player.playerWeapon.handGun:
                wepType = Player.playerWeapon.handGun;
                firstText.text = "점사 기능 추가";
                secondText.text = "연사 기능 추가";
                thirdText.text = "리볼버";
                GameManager.instance.player.hgMasterLevel = 0;
                break;
        }
    }

    public void FirstSelect()
    {
        select = 0;
        OXActive();
    }

    public void SecondSelect()
    {
        select = 1;
        OXActive();
    }

    public void ThirdSelect()
    {
        select = 2;
        OXActive();
    }

    public void Accept()
    {
        switch(wepType)
        {
            case Player.playerWeapon.melee:
                switch(select)
                {
                    case 0:
                        Debug.Log("도끼 마스터");
                        break;
                    case 1:
                        Debug.Log("방패 마스터");
                        break;
                    case 2:
                        Debug.Log("??? 마스터");
                        break;
                }
                break;
        }
        chooseMaster.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OXActive()
    {
        masterOX.SetActive(true);
    }
}
