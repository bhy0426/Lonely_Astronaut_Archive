using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack : MonoBehaviour
{

    public BulletData[] bulletData;
    public GameObject bulletPrefab;
    public GameObject bulletDebrisPrefab;
    public GameObject bulletPrefabSg;
    public Transform bulletPos;
    public Hand hand;
    public Crowbar crowbar;
    public AudioSource audioSource;
    public AudioClip[] audioClipFire;
    public AudioClip[] audioClipReload;
    public Transform shotPos;
    public GameObject ReloadUI;
    public GameObject textAmmoUI;
    public SpriteRenderer muzzleFlash;

    //////// 데미지
    public float meleeDamage;
    public float meleeCritChance = 5f;
    public float meleeCritDamage = 2f;
    public float meleeSize = 1f;

    //////// 쿨다운
    // 쿨다운 시간
    public float meleeCooldown;
    public float meleeAnimCooldown = 0.15f;
    public float handgunCooldown;
    private float handgunAnimCooldown = 0.125f;
    public float rifleCooldown;
    public float sgCooldown;
    public float srCooldown;
    // 공격 가능 여부
    public bool isAttackmelee = false;
    public bool isAttackAnimMelee = false;
    public bool isAttackHg = false;
    public bool isAttackAnimHg = false;
    public bool isAttackSg = false;
    public bool isAttackAr = false;
    public bool isAttackSr = false;
    // 장전 쿨다운
    public float reloadHg;
    public float reloadSg;
    public float reloadAr;
    public float reloadSr;
    public float curTime;
    public float reloadTime;
    public bool isReload;

    //////// <탄약>
    // 현재 탄약
    public int curAmmo;
    // 남은 탄약
    public int maxAmmo;
    // 무기별 현재 탄약 저장
    public int tempAmmoHg;
    public int tempAmmoSg;
    public int tempAmmoAr;
    public int tempAmmoSr;
    // 무기별 남은 탄약 저장
    public int saveAmmoHg;
    public int saveAmmoSg;
    public int saveAmmoAr;
    public int saveAmmoSr;
    public int saveAmmoRpg;
    // 무기별 최대 탄약
    // weaponMaxAmmo[GameManager.instance.player.curPlayerWeapon]
    public int[] weaponMaxAmmo = new int[7];
    public int maxAmmoHg;
    public int maxAmmoSg;
    public int maxAmmoAr;
    public int maxAmmoSr;

    // 드랍률
    public float handgunAmmoChance;
    public float shotgunAmmoChance;
    public float rifleAmmoChance;
    public float sniperAmmoChance;

    // 근접무기 애니메이션 속도
    public float meleeAnimSpeed;

    // 반동
    public float rifleRecoil;

    // 현재 무기 타입
    public Player.playerWeapon curWepType;

    private void Awake()
    {
        weaponMaxAmmo[(int)Player.playerWeapon.handGun] = 11;
        weaponMaxAmmo[(int)Player.playerWeapon.sg] = 5;
        weaponMaxAmmo[(int)Player.playerWeapon.rifle] = 30;
        weaponMaxAmmo[(int)Player.playerWeapon.sr] = 5;
        //maxAmmoHg = 11;
        //maxAmmoSg = 5;
        //maxAmmoAr = 30;
        saveAmmoHg = 66;
        saveAmmoSg = 30;
        saveAmmoAr = 180;
        saveAmmoSr = 30;
        saveAmmoRpg = 3;
        tempAmmoHg = 11;
        tempAmmoSg = 5;
        tempAmmoAr = 30;
        tempAmmoSr = 5;
        //reloadHg = 1f;
        //reloadSg = 1f;
        //reloadAr = 1f;
        //reloadSr = 1f;
        isReload = false;

    }

    void Update()
    {
        if (!GameManager.instance.player.isLevelUp && !GameManager.instance.player.isChooseWeapon)
        { 
            if (isReload)
            {
                //ReloadUI.SetActive(true);
                //if (GameManager.instance.player.transform.position.x > GameManager.instance.player.mouse.x)
                //{
                //    ReloadUI.transform.localPosition = new Vector3(40f, -15f, 0);
                //}
                //else if (GameManager.instance.player.transform.position.x < GameManager.instance.player.mouse.x)
                //{
                //    ReloadUI.transform.localPosition = new Vector3(-45f, -15f, 0);
                //}
                if (curTime > reloadTime)
                {
                    Reload();
                    isReload = false;
                }
                curTime += Time.deltaTime;
                //textAmmoUI.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.3f);
                //ReloadUI.GetComponent<Image>().fillAmount = curTime / reloadTime;
            }
            else
            {
                //textAmmoUI.GetComponent<TextMeshProUGUI>().color = Color.white;
                ReloadUI.SetActive(false);
                Attack();
                WeaponSelect();
                if (!(GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.hand) && !(GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.melee))
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        if (curAmmo < maxAmmo)
                        {
                            audioSource.PlayOneShot(audioClipReload[(int)GameManager.instance.player.curPlayerWeapon]);
                            curTime = 0;
                            isReload = true;
                            curWepType = GameManager.instance.player.curPlayerWeapon;
                        }
                    }
                }
            }
        }
        
        UtillSelect();
    }

    private void LateUpdate()
    {
        if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.melee)
        {
            hand.anim.SetBool("isAttackMelee", isAttackAnimMelee);
        }
        else if (GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.handGun)
            hand.anim.SetBool("isAttackHg", isAttackAnimHg);

        switch(GameManager.instance.player.curPlayerWeapon)
        {
            case Player.playerWeapon.handGun:
                muzzleFlash.gameObject.GetComponent<Animator>().SetBool("isAtk", isAttackHg);
                break;
            case Player.playerWeapon.sg:
                muzzleFlash.gameObject.GetComponent<Animator>().SetBool("isAtk", isAttackSg);
                break;
            case Player.playerWeapon.rifle:
                muzzleFlash.gameObject.GetComponent<Animator>().SetBool("isAtk", isAttackAr);
                break;
            case Player.playerWeapon.sr:
                muzzleFlash.gameObject.GetComponent<Animator>().SetBool("isAtk", isAttackSr);
                break;
        }
    }

    // 무기 선택 함수
    void WeaponSelect()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            //GameManager.instance.player.speed = 2.4f * GameManager.instance.player.speedValue;
            AmmoStore();
            GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.hand;
            crowbar.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //GameManager.instance.player.speed = 2.35f * GameManager.instance.player.speedValue;
            
            AmmoStore();
            GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.melee;
            crowbar.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameManager.instance.player.hasHandgun)
            {
                reloadTime = reloadHg;
                //GameManager.instance.player.speed = 2f * GameManager.instance.player.speedValue;
                crowbar.gameObject.SetActive(false);
                AmmoStore();
                GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.handGun;
                // 장전
                maxAmmo = saveAmmoHg;
                curAmmo = tempAmmoHg;
                maxAmmo -= tempAmmoHg;
                tempAmmoHg = 0;
                if(curAmmo == 0)
                {
                    curTime = 0;
                    isReload = true;
                }
            }
            else
                Debug.Log("아직 권총을 가지고 있지 않습니다.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GameManager.instance.player.hasShotgun)
            {
                reloadTime = reloadSg;
                //GameManager.instance.player.speed = 1.8f * GameManager.instance.player.speedValue;
                crowbar.gameObject.SetActive(false);
                AmmoStore();
                GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.sg;
                maxAmmo = saveAmmoSg;
                curAmmo = tempAmmoSg;
                maxAmmo -= tempAmmoSg;
                tempAmmoSg = 0;
                if (curAmmo == 0)
                {
                    curTime = 0;
                    isReload = true;
                }
            }
            else
                Debug.Log("아직 샷건을 가지고 있지 않습니다.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (GameManager.instance.player.hasRifle)
            {
                reloadTime = reloadAr;
                //GameManager.instance.player.speed = 1.9f * GameManager.instance.player.speedValue;
                crowbar.gameObject.SetActive(false);
                AmmoStore();
                GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.rifle;
                maxAmmo = saveAmmoAr;
                curAmmo = tempAmmoAr;
                maxAmmo -= tempAmmoAr;
                tempAmmoAr = 0;
                if (curAmmo == 0)
                {
                    curTime = 0;
                    isReload = true;
                }
            }
            else
                Debug.Log("아직 라이플을 가지고 있지 않습니다.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (GameManager.instance.player.hasSniper)
            {
                reloadTime = reloadSr;
                //GameManager.instance.player.speed = 1.9f * GameManager.instance.player.speedValue;
                crowbar.gameObject.SetActive(false);
                AmmoStore();
                GameManager.instance.player.curPlayerWeapon = Player.playerWeapon.sr;
                maxAmmo = saveAmmoSr;
                curAmmo = tempAmmoSr;
                maxAmmo -= tempAmmoSr;
                tempAmmoSr = 0;
                if (curAmmo == 0)
                {
                    curTime = 0;
                    isReload = true;
                }
            }
            else
                Debug.Log("아직 스나이퍼를 가지고 있지 않습니다.");
        }
    }

    void UtillSelect()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch(GameManager.instance.player.curPlayerItem)
            {
                case Player.ItemType.empty:
                    //Debug.Log("보유한 아이템이 없습니다.");
                    break;
                case Player.ItemType.hp:
                    GameManager.instance.player.curPlayerItem = Player.ItemType.shield;
                    //if (GameManager.instance.player.countHpPotion > 0)
                    //{
                    //    GameManager.instance.player.countHpPotion -= 1;
                    //    if (GameManager.instance.player.health <= GameManager.instance.player.maxHealth - 10)
                    //        GameManager.instance.player.health += 10;
                    //    else
                    //        GameManager.instance.player.health = GameManager.instance.player.maxHealth;
                    //    //Debug.Log("체력 회복");
                    //}
                    //else
                    //{
                    //    //Debug.Log("회복포션이 부족합니다.");
                    //}
                    break;
                case Player.ItemType.shield:
                    GameManager.instance.player.curPlayerItem = Player.ItemType.invisible;
                    //if (GameManager.instance.player.countShieldPotion > 0)
                    //{
                    //    GameManager.instance.player.countShieldPotion -= 1;
                    //    GameManager.instance.player.battery += 5;
                    //    //Debug.Log("실드 적용");
                    //}
                    //else
                    //{
                    //    //Debug.Log("실드포션이 부족합니다.");
                    //}
                    break;
                case Player.ItemType.invisible:
                    GameManager.instance.player.curPlayerItem = Player.ItemType.hp;
                    //if (GameManager.instance.player.countInvisiblePotion > 0)
                    //{
                    //    GameManager.instance.player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                    //    GameManager.instance.hand.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                    //    Invoke("InvisibleCoolDown", 1f);
                    //    GameManager.instance.player.countInvisiblePotion -= 1;
                    //    //Debug.Log("투명화 적용");
                    //}
                    //else
                    //{
                    //    //Debug.Log("투명포션이 부족합니다.");
                    //}
                    break;
            }
        }
    }

    // 공격 함수
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(GameManager.instance.player.curPlayerWeapon)
            {
                case Player.playerWeapon.hand:
                    
                    break;
                case Player.playerWeapon.melee:
                    if (!isAttackmelee)
                    {
                        crowbar.GetComponent<Collider2D>().enabled = true;
                        audioSource.clip = audioClipFire[(int)GameManager.instance.player.curPlayerWeapon];
                        audioSource.PlayOneShot(audioSource.clip);
                        isAttackmelee = true;
                        isAttackAnimMelee = true;
                        Invoke("CrowbarAtkCool", meleeCooldown);
                        Invoke("CrowbarAtkAnimCool", meleeAnimCooldown);
                    }
                    break;
                case Player.playerWeapon.handGun:
                    if (curAmmo > 1)
                    {
                        if (!isAttackHg) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[(int)GameManager.instance.player.curPlayerWeapon];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("HandgunCooldown", handgunCooldown);
                            Invoke("HandgunAnimCooldown", handgunAnimCooldown);

                            isAttackHg = true;
                            isAttackAnimHg = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    else if(curAmmo == 1)
                    {
                        if (!isAttackHg) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[(int)GameManager.instance.player.curPlayerWeapon];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("HandgunCooldown", handgunCooldown);
                            Invoke("HandgunAnimCooldown", handgunAnimCooldown);
                    
                            audioSource.PlayOneShot(audioClipReload[(int)GameManager.instance.player.curPlayerWeapon]);
                            isAttackHg = true;
                            isAttackAnimHg = true;
                            curTime = 0;
                            isReload = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    break;
                case Player.playerWeapon.sg:
                    if (curAmmo > 1)
                    {
                        if (!isAttackSg) // 공격을 하지 않았을 때
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                GameObject bullet = Instantiate(bulletPrefabSg, transform);
                                bullet.transform.position = shotPos.position;
                            }
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            audioSource.clip = audioClipFire[(int)GameManager.instance.player.curPlayerWeapon];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("SgCooldown", sgCooldown);

                            isAttackSg = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    else if (curAmmo == 1)
                    {
                        if (!isAttackSg) // 공격을 하지 않았을 때
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                GameObject bullet = Instantiate(bulletPrefabSg, transform);
                                bullet.transform.position = shotPos.position;
                            }
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            audioSource.clip = audioClipFire[(int)GameManager.instance.player.curPlayerWeapon];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("SgCooldown", sgCooldown);
                    
                            isAttackSg = true;
                            curTime = 0;
                            isReload = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    break;
                case Player.playerWeapon.sr:
                    if (curAmmo > 1)
                    {
                        if (!isAttackSr) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[7];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("SniperCooldown", srCooldown);
                            //Invoke("HandgunAnimCooldown", handgunAnimCooldown);

                            isAttackSr = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    else if (curAmmo == 1)
                    {
                        if (!isAttackSr) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[7];
                            audioSource.Play();
                            curAmmo -= 1;
                            Invoke("SniperCooldown", srCooldown);
                            //Invoke("HandgunAnimCooldown", handgunAnimCooldown);
                    
                            audioSource.PlayOneShot(audioClipReload[(int)GameManager.instance.player.curPlayerWeapon]);
                            isAttackSr = true;
                            curTime = 0;
                            isReload = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    break;
                    //case Player.playerWeapon.rifle:
                    //    if (curAmmo > 0)
                    //    {
                    //        if (!isAttackAr) // 공격을 하지 않았을 때
                    //        {
                    //            GameObject bullet = Instantiate(bulletPrefab, transform);
                    //            bullet.transform.position = GameManager.instance.player.transform.position;
                    //            //audioSource.clip = audioClip[(int)GameManager.instance.player.curPlayerWeapon];
                    //            //audioSource.Play();
                    //            curAmmo -= 1;
                    //            Invoke("RifleCooldown", handgunCooldown);
                    //
                    //            isAttackAr = true;
                    //        }
                    //    }
                    //    break;
            }

        }
        else if(Input.GetMouseButton(0))
        {
            switch (GameManager.instance.player.curPlayerWeapon)
            {
                case Player.playerWeapon.rifle:
                    if (curAmmo > 1)
                    {
                        if (!isAttackAr) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            //bullet.transform.position = GameManager.instance.player.transform.position;
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[Random.Range(4, 7)];
                            audioSource.Play();
                            //audioSource.PlayOneShot(audioSource.clip);
                            curAmmo -= 1;
                            Invoke("RifleCooldown", rifleCooldown);
                            muzzleFlash.enabled = true;
                            isAttackAr = true;
                        }
                    }
                    else if (curAmmo == 1)
                    {
                        if (!isAttackAr) // 공격을 하지 않았을 때
                        {
                            GameObject bullet = Instantiate(bulletPrefab, transform);
                            GameObject bulletDebris = Instantiate(bulletDebrisPrefab, transform);
                            //bullet.transform.position = GameManager.instance.player.transform.position;
                            bullet.transform.position = shotPos.position;
                            audioSource.clip = audioClipFire[Random.Range(4, 7)];
                            audioSource.Play();
                            //audioSource.PlayOneShot(audioSource.clip);
                            curAmmo -= 1;
                            Invoke("RifleCooldown", rifleCooldown);
                    
                            //audioSource.clip = audioClipReload[(int)GameManager.instance.player.curPlayerWeapon];
                            audioSource.PlayOneShot(audioClipReload[(int)GameManager.instance.player.curPlayerWeapon]);
                            isAttackAr = true;
                            curTime = 0;
                            isReload = true;
                            muzzleFlash.enabled = true;
                        }
                    }
                    break;
            }
        }
    }

    // 장전 함수
    void Reload()
    {
        switch (GameManager.instance.player.curPlayerWeapon)
        {
            case Player.playerWeapon.hand:

                break;
            case Player.playerWeapon.melee:

                break;
            case Player.playerWeapon.handGun:

                //audioSource.PlayOneShot(audioClip[0]);
                maxAmmo += curAmmo;
                if (maxAmmo >= weaponMaxAmmo[(int)Player.playerWeapon.handGun])
                {
                    maxAmmo -= weaponMaxAmmo[(int)Player.playerWeapon.handGun];
                    curAmmo = weaponMaxAmmo[(int)Player.playerWeapon.handGun];
                }
                else
                {
                    curAmmo = maxAmmo;
                    maxAmmo = 0;
                }

                break;
            case Player.playerWeapon.sg:

                maxAmmo += curAmmo;
                if (maxAmmo >= weaponMaxAmmo[(int)Player.playerWeapon.sg])
                {
                    maxAmmo -= weaponMaxAmmo[(int)Player.playerWeapon.sg];
                    curAmmo = weaponMaxAmmo[(int)Player.playerWeapon.sg];
                }
                else
                {
                    curAmmo = maxAmmo;
                    maxAmmo = 0;
                }

                break;
            case Player.playerWeapon.rifle:

                maxAmmo += curAmmo;
                if (maxAmmo >= weaponMaxAmmo[(int)Player.playerWeapon.rifle])
                {
                    maxAmmo -= weaponMaxAmmo[(int)Player.playerWeapon.rifle];
                    curAmmo = weaponMaxAmmo[(int)Player.playerWeapon.rifle];
                }
                else
                {
                    curAmmo = maxAmmo;
                    maxAmmo = 0;
                }

                break;
            case Player.playerWeapon.sr:

                maxAmmo += curAmmo;
                if (maxAmmo >= weaponMaxAmmo[(int)Player.playerWeapon.sr])
                {
                    maxAmmo -= weaponMaxAmmo[(int)Player.playerWeapon.sr];
                    curAmmo = weaponMaxAmmo[(int)Player.playerWeapon.sr];
                }
                else
                {
                    curAmmo = maxAmmo;
                    maxAmmo = 0;
                }

                break;
        }
    }

    // 남은 탄약 저장 함수(이게 왜 있는거지?)
    void AmmoStore()
    {
        switch (GameManager.instance.player.curPlayerWeapon)
        {
            case Player.playerWeapon.handGun:
                tempAmmoHg = curAmmo;
                saveAmmoHg = maxAmmo + curAmmo;
                break;
            case Player.playerWeapon.sg:
                tempAmmoSg = curAmmo;
                saveAmmoSg = maxAmmo + curAmmo;
                break;
            case Player.playerWeapon.rifle:
                tempAmmoAr = curAmmo;
                saveAmmoAr = maxAmmo + curAmmo;
                break;
            case Player.playerWeapon.sr:
                tempAmmoSr = curAmmo;
                saveAmmoSr = maxAmmo + curAmmo;
                break;
        }
    }
    void CrowbarAtkCool()
    {
        crowbar.GetComponent<Collider2D>().enabled = false;
        isAttackmelee = false;
    }

    void CrowbarAtkAnimCool()
    {
        isAttackAnimMelee = false;
    }

    void HandgunCooldown()
    {
        isAttackHg = false;
    }

    void SgCooldown()
    {
        isAttackSg = false;
    }

    void RifleCooldown()
    {
        isAttackAr = false;
        //muzzleFlash.enabled = false;
    }

    void SniperCooldown()
    {
        isAttackSr = false;
    }

    void HandgunAnimCooldown()
    {
        isAttackAnimHg = false;
    }
}
[System.Serializable]
public class BulletData
{
    public float damage;
    public float critChance;
    public float critDamage;
}