using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 플레이어 정보
    public float health;
    public float maxHealth;
    public float battery;
    public float maxBattery;
    public float speed;
    public float speedValue;
    public float atk;
    public float exp;
    public float maxExp;
    public int level;
    public bool isLevelUp;
    public bool isChooseWeapon;
    public bool chooseWeaponCooldown;
    // 플레이어 업그레이드 레벨
    // Master Level
    public int meleeMasterLevel;
    public int hgMasterLevel;
    public int sgMasterLevel;
    public int arMasterLevel;
    public int srMasterLevel;
    // Public
    public int addHealthLevel;
    public int addSpeedLevel;
    public int addExpChanceLevel;
    public int addItemChanceLevel;
    public int addAmmoChanceLevel;
    // Melee
    public int meleeDamageLevel;
    public int meleeAtkSpeedLevel;
    public int meleeCritChanceLevel;
    public int meleeCritDamageLevel;
    public int meleeSizeLevel;
    // Handgun
    public int HgDamageLevel;
    public int HgAtkSpeedLevel;
    public int HgExtraAmmoChanceLevel = 1;
    public int HgExtraAmmoLevel;
    public int HgCritChanceLevel;
    public int HgCritDamageLevel;
    // Shotgun
    public int SgDamageLevel;
    public int SgAtkSpeedLevel;
    public int SgExtraAmmoChanceLevel = 1;
    public int SgExtraAmmoLevel;
    public int SgCritChanceLevel;
    public int SgCritDamageLevel;
    // Rifle
    public int ArDamageLevel;
    public int ArAtkSpeedLevel;
    public int ArExtraAmmoChanceLevel = 1;
    public int ArExtraAmmoLevel;
    public int ArCritChanceLevel;
    public int ArCritDamageLevel;
    // Sniper
    public int SrDamageLevel;
    public int SrAtkSpeedLevel;
    public int SrExtraAmmoChanceLevel = 1;
    public int SrExtraAmmoLevel;
    public int SrCritChanceLevel;
    public int SrCritDamageLevel;
    // 플레이어 이동 벡터
    public Vector2 inputVec;
    // 플레이어 무기 열거형
    public enum playerWeapon { hand, melee, handGun, sg, rifle, sr, rpg};
    public enum ItemType {empty, hp, shield, invisible};
    public playerWeapon curPlayerWeapon;
    public ItemType curPlayerItem;
    // 플레이어 무기 보유 여부
    public bool hasHandgun = false;
    public bool hasShotgun = false;
    public bool hasRifle = false;
    public bool hasSniper = false;
    // 플레이어 아이템 보유 개수
    // 1 - hp
    // 2 - shield
    // 3 - invisible
    public int[] item = new int[3];
    // 컴포넌트 접근 관련
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    public Spawner spawner;
    public GameObject hand;
    public RuntimeAnimatorController[] animCon;

    // 마우스 이동 관련
    Camera cam;
    public Vector2 mouse;
    Vector2 dir;

    // 히트 판정
    public bool isHit;
    public float hitCooldown = 0.5f;

    void Awake()
    {
        addExpChanceLevel = 0;
        curPlayerWeapon = playerWeapon.hand;
        curPlayerItem = ItemType.empty;
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        if(health <= 0)
        {
            //Time.timeScale = 0;
            //rigid.constraints = RigidbodyConstraints2D.None;
        }
        //Debug.DrawRay(transform.position, mouse, Color.red);
        dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (exp >= maxExp)
        {
            level += 1;
            exp = 0;
            maxExp *= 1.15f;
            isLevelUp = true;
        }
        
        switch(level)
        {
            case 5:
                if (!chooseWeaponCooldown)
                {
                    GameManager.instance.player.isChooseWeapon = true;
                    chooseWeaponCooldown = true;
                }
                break;
            case 10:
                if (!chooseWeaponCooldown)
                {
                    GameManager.instance.player.isChooseWeapon = true;
                    chooseWeaponCooldown = true;
                }
                break;
            case 15:
                if (!chooseWeaponCooldown)
                {
                    GameManager.instance.player.isChooseWeapon = true;
                    chooseWeaponCooldown = true;
                }
                break;
            default:
                chooseWeaponCooldown = false;
                break;
        }

        switch(curPlayerWeapon)
        {
            case playerWeapon.hand:
                speed = 1.6f * speedValue;
                break;
            case playerWeapon.melee:
                speed = 1.5f * speedValue;
                break;
            case playerWeapon.handGun:
                speed = 1.4f * speedValue;
                break;
            case playerWeapon.sg:
                speed = 1.3f * speedValue;
                break;
            case playerWeapon.rifle:
                speed = 1.35f * speedValue;
                break;
            case playerWeapon.sr:
                speed = 1.3f * speedValue;
                break;
        }
    }

    void FixedUpdate()
    {

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate()
    {
        spriter.flipX = transform.position.x > mouse.x;
        anim.SetFloat("Speed", inputVec.magnitude);
        anim.runtimeAnimatorController = animCon[(int)curPlayerWeapon];
    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Item")
    //    {
    //        collision.gameObject.GetComponent<Supply>().infoText.SetActive(true);
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            item[(int)collision.gameObject.GetComponent<Supply>().itemType] += 1;
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            collision.gameObject.GetComponent<Supply>().infoText.SetActive(false);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Item")
    //    {
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            item[(int)collision.gameObject.GetComponent<Supply>().itemType] += 1;
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isHit)
            {
                if(collision.gameObject.GetComponent<Enemy>().atk > battery)
                {
                    health -= collision.gameObject.GetComponent<Enemy>().atk - battery;
                    battery = 0;
                }
                else
                {
                    battery -= collision.gameObject.GetComponent<Enemy>().atk;
                }
                isHit = true;
                Vector2 dirVec = collision.gameObject.GetComponent<Rigidbody2D>().position - rigid.position;
                rigid.AddForce(-dirVec.normalized * 0.5f, ForceMode2D.Impulse);
                Invoke("HitCooldown", hitCooldown);
                spriter.color = Color.red;
            }
        }
        else if (collision.gameObject.tag == "Golem")
        {
            if (!isHit)
            {
                if (20 > battery)
                {
                    health -= 20 - battery;
                    battery = 0;
                }
                else
                {
                    battery -= 20;
                }
                isHit = true;
                Vector2 dirVec = collision.gameObject.GetComponent<Rigidbody2D>().position - rigid.position;
                rigid.AddForce(-dirVec.normalized * 0.5f, ForceMode2D.Impulse);
                Invoke("HitCooldown", hitCooldown);
                spriter.color = Color.red;
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            if (!isHit)
            {
                isHit = true;
                health -= collision.gameObject.GetComponent<Enemy>().atk;
                Vector2 dirVec = collision.gameObject.GetComponent<Rigidbody2D>().position - rigid.position;
                rigid.AddForce(-dirVec.normalized * 2f, ForceMode2D.Impulse);
                Invoke("HitCooldown", hitCooldown);
                spriter.color = Color.red;
            }
        }
        
    }

    void HitCooldown()
    {
        isHit = false;
        spriter.color = Color.white;
    }

    public void HitDrone()
    {
        if (!isHit)
        {
            isHit = true;
            health -= spawner.spawnData[1].atk;
            //Vector2 dirVec = collision.gameObject.GetComponent<Rigidbody2D>().position - rigid.position;
            //rigid.AddForce(-dirVec.normalized * 0.5f, ForceMode2D.Impulse);
            Invoke("HitCooldown", hitCooldown);
            spriter.color = Color.red;
        }
    }
}
