using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseWeapon : MonoBehaviour
{
    public GameObject chooseUpgrade;
    public GameObject OXUI;

    public GameObject firstWeapon;
    public GameObject secondWeapon;
    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;

    public bool firstSelect = false;
    public bool secondSelect = false;


    // 3레벨과 7레벨에는 권총(왼쪽), 샷건(오른쪽)으로 구성
    //
    private void OnEnable()
    {
        if (GameManager.instance.player.level >= 3 && GameManager.instance.player.level < 12)
        {
            if (!GameManager.instance.player.hasHandgun && !GameManager.instance.player.hasShotgun)
            {
                firstWeapon.GetComponent<Button>().interactable = true;
                secondWeapon.GetComponent<Button>().interactable = true;
                firstText.text = "권총";
                secondText.text = "샷건";
            }
            else if (GameManager.instance.player.hasHandgun && !GameManager.instance.player.hasShotgun)
            {
                firstWeapon.GetComponent<Button>().interactable = false;
                secondWeapon.GetComponent<Button>().interactable = true;
                firstText.text = "권총(보유중)";
                secondText.text = "샷건";
            }
            else if (!GameManager.instance.player.hasHandgun && GameManager.instance.player.hasShotgun)
            {
                firstWeapon.GetComponent<Button>().interactable = true;
                secondWeapon.GetComponent<Button>().interactable = false;
                firstText.text = "권총";
                secondText.text = "샷건(보유중)";
            }
        }
        else if (GameManager.instance.player.level >= 12 && GameManager.instance.player.level < 99)
        {
            if (!GameManager.instance.player.hasRifle && !GameManager.instance.player.hasSniper)
            {
                firstWeapon.GetComponent<Button>().interactable = true;
                secondWeapon.GetComponent<Button>().interactable = true;
                firstText.text = "라이플";
                secondText.text = "스나이퍼";
            }
            else if (GameManager.instance.player.hasRifle && !GameManager.instance.player.hasSniper)
            {
                firstWeapon.GetComponent<Button>().interactable = false;
                secondWeapon.GetComponent<Button>().interactable = true;
                firstText.text = "라이플(보유중)";
                secondText.text = "스나이퍼";
            }
            else if (!GameManager.instance.player.hasRifle && GameManager.instance.player.hasSniper)
            {
                firstWeapon.GetComponent<Button>().interactable = true;
                secondWeapon.GetComponent<Button>().interactable = false;
                firstText.text = "라이플";
                secondText.text = "스나이퍼(보유중)";
            }
        }
    }

    public void Accept()
    {
        //ApplyUpgrade();
        OXUI.SetActive(false);
        Time.timeScale = 0;
        //gameObject.SetActive(false);
        gameObject.SetActive(false);
        GameManager.instance.player.isChooseWeapon = false;
        if (GameManager.instance.player.level >= 3 && GameManager.instance.player.level < 12)
        {
            if (firstSelect)
            {
                GameManager.instance.player.hasHandgun = true;
            }
            else if (secondSelect)
            {
                GameManager.instance.player.hasShotgun = true;
            }
        }
        else if(GameManager.instance.player.level >= 12 && GameManager.instance.player.level < 99)
        {
            if (firstSelect)
            {
                GameManager.instance.player.hasRifle = true;
            }
            else if (secondSelect)
            {
                GameManager.instance.player.hasSniper = true;
            }
        }
    }

    public void ChooseFirst()
    {
        OXUI.SetActive(true);
        firstSelect = true;
        secondSelect = false;
    }

    public void ChooseSecond()
    {
        OXUI.SetActive(true);
        firstSelect = false;
        secondSelect = true;
    }

    public void Cancel()
    {
        OXUI.SetActive(false);
    }
}
