using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public Slider sliderHp;
    public Slider sliderBat;
    public Slider sliderExp;
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textAmmo;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textHp;
    public TextMeshProUGUI textExp;
    public Image weaponUiImage;
    public Image utillUiImage;
    public Sprite[] ammoSprite;
    public Sprite[] weaponUiSprite;
    public Sprite[] utillUiSprite;
    public GameObject[] ammoBullet;
    public GameObject chooseUpgrade;
    public GameObject chooseWeapon;
    public GameObject Status;
    public GameObject UtillSelect;
    public GameObject ReloadUI;

    public bool isStatus;

    void Awake()
    {
        textAmmo.color = Color.white;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sliderHp.value = GameManager.instance.player.health / GameManager.instance.player.maxHealth;
        sliderBat.value = GameManager.instance.player.battery / GameManager.instance.player.maxBattery;
        sliderExp.value = GameManager.instance.player.exp / GameManager.instance.player.maxExp;
        weaponUiImage.sprite = weaponUiSprite[(int)GameManager.instance.player.curPlayerWeapon];
        utillUiImage.sprite = utillUiSprite[(int)GameManager.instance.player.curPlayerItem];
        if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.melee || GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.hand)
        {
            textAmmo.gameObject.SetActive(false);
        }
        else
        {
            textAmmo.gameObject.SetActive(true);
            if (GameManager.instance.player.transform.position.x < GameManager.instance.player.mouse.x)
            {
                textAmmo.alignment = TextAlignmentOptions.BottomLeft;
            }
            else if (GameManager.instance.player.transform.position.x > GameManager.instance.player.mouse.x)
            {
                textAmmo.alignment = TextAlignmentOptions.BottomRight;
            }
            textAmmo.gameObject.transform.position = Input.mousePosition;
            textAmmo.text = GameManager.instance.playerAtk.curAmmo.ToString("00") + "/" + GameManager.instance.playerAtk.maxAmmo.ToString("00");
            if (GameManager.instance.playerAtk.curAmmo > GameManager.instance.playerAtk.weaponMaxAmmo[(int)GameManager.instance.player.curPlayerWeapon] * 0.3f)
            {
                textAmmo.color = Color.white;
            }
            else
            {
                textAmmo.color = new Color(0.1f, 0.1f, 0.1f, 1);
                //textAmmo.color = new Color(1, 1, 1, 0.3f);
            }
        }
        textLevel.text = GameManager.instance.player.level.ToString();
        textTime.text = Mathf.FloorToInt(GameManager.instance.gameTime / 60).ToString("00") + ":" + Mathf.FloorToInt(GameManager.instance.gameTime % 60).ToString("00");
        if (GameManager.instance.player.battery <= 0)
        {
            textHp.text = GameManager.instance.player.health.ToString() + "/" + GameManager.instance.player.maxHealth.ToString();
        }
        else
        {
            textHp.text = GameManager.instance.player.health.ToString() + "(+" + GameManager.instance.player.battery + ")" + "/" + GameManager.instance.player.maxHealth.ToString();


        }
        textExp.text = ((GameManager.instance.player.exp / GameManager.instance.player.maxExp) * 100f).ToString("F2") + "%";

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isStatus)
            {
                Status.SetActive(true);
                isStatus = true;
            }
            else
            {
                Status.SetActive(false);
                isStatus = false;
            }
        }
        //if(Input.GetKey(KeyCode.Q))
        //{
        //    UtillSelect.SetActive(true);
        //}
        //if(!Input.GetKey(KeyCode.Q))
        //{
        //    UtillSelect.SetActive(false);
        //}
        if(GameManager.instance.player.isChooseWeapon)
        {
            Time.timeScale = 0;
            chooseWeapon.SetActive(true);
        }
        if(GameManager.instance.player.isLevelUp && !GameManager.instance.player.isChooseWeapon)
        {
            Time.timeScale = 0;
            chooseUpgrade.SetActive(true);
        }
    }
}
