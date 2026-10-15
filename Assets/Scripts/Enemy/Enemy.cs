using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Rigidbody2D target;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public GameObject expPrefab;
    public GameObject DamageTextPrefab;
    public GameObject supplyPrefab;
    public GameObject[] ammoPrefabs;
    public Material[] materials;
    Slider slider;
    RectTransform rectTransform;
    ParticleSystem particle;
    SpriteRenderer spriter;

    public float health;
    public float maxHealth;
    public float speed;
    public float atk;
    public bool isHit;
    public int randItem;
    public int randCrit;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        particle = GetComponent<ParticleSystem>();
        slider = GetComponentInChildren<Slider>();
        rectTransform = slider.GetComponent<RectTransform>();
        spriter = GetComponent<SpriteRenderer>();
        //audioSource.clip = audioClip[1];
        //audioSource.Play();
    }

    private void Update()
    {
        Disable();
    }

    private void LateUpdate()
    {
        slider.value = health / maxHealth;
        rectTransform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        rectTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //rigid.isKinematic = false;
        if (collision.gameObject.tag == "Bullet")
        {
            Hit(collision.GetComponent<BulletManager>().myBulletData.damage, collision.GetComponent<BulletManager>().myBulletData.critChance, 
                collision.GetComponent<BulletManager>().myBulletData.critDamage, 0.25f, Player.playerWeapon.handGun, collision.transform);
        }
        if (collision.gameObject.tag == "BulletShotgun")
        {
            Hit(collision.GetComponent<BulletManager>().myBulletData.damage, collision.GetComponent<BulletManager>().myBulletData.critChance, 
                collision.GetComponent<BulletManager>().myBulletData.critDamage, 0.25f, Player.playerWeapon.sg, collision.transform);
        }
        if (collision.gameObject.tag == "Melee")
        {
            if (!isHit)
            {
                Hit(GameManager.instance.playerAtk.meleeDamage, GameManager.instance.playerAtk.meleeCritChance, GameManager.instance.playerAtk.meleeCritDamage,
                    0.25f, Player.playerWeapon.melee, collision.transform);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            if (!isHit)
            {
                Hit(GameManager.instance.playerAtk.meleeDamage, GameManager.instance.playerAtk.meleeCritChance, GameManager.instance.playerAtk.meleeCritDamage,
                    0.25f, Player.playerWeapon.melee, collision.transform);
            }
        }
    }
    public void Init(SpawnData spawnData)
    {
        health = spawnData.health;
        maxHealth = spawnData.health;
        speed = spawnData.speed;
        atk = spawnData.atk;
    }

    void Hit(float damage, float critChance, float critDamage, float cooldown,
        Player.playerWeapon audioType, Transform spawnPos)
    {

        
        // 1 ~ 100
        GameObject damageText = Instantiate(DamageTextPrefab);
        damageText.transform.position = spawnPos.position;
        randCrit = Random.Range(1, 101);
        if (randCrit >= 1 && randCrit <= critChance)
        {
            damage *= critDamage;
            damageText.GetComponent<TextMeshPro>().color = Color.yellow;
            //Debug.Log("크리티컬!!");
        }
        else
        {
            damageText.GetComponent<TextMeshPro>().color = Color.white;
        }
        // SpriteRenderer의 Material 변경
        spriter.material = materials[1];
        //GetComponent<SpriteRenderer>().color = Color.red;
        // isHit를 참으로 변경
        isHit = true;
        // 포지션 고정
        rigid.constraints = ~RigidbodyConstraints2D.FreezePosition;
        Vector3 dirVec = target.position - rigid.position;
        rigid.MovePosition(transform.position + (-dirVec.normalized * 0.1f));
        switch (audioType)
        {
            case Player.playerWeapon.melee:
                GameManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip[0]);
                break;
            default:
                GameManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip[1]);
                break;
        }
        health -= damage;
        Invoke("HitCoolDown", cooldown);
        //GetComponent<SpriteRenderer>().color = Color.red;
        particle.Play();
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();
        //damageText.GetComponent<RectTransform>().transform.position = transform.position;

    }

    void HitCoolDown()
    {
        isHit = false;
        spriter.material = materials[0];
        rigid.velocity = Vector2.zero;
        //GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Disable()
    { 
        if (health <= 0)
        {
            //audioSource.clip = audioClip[0];
            //audioSource.Play();
            randItem = Random.Range(1, 101);
            ExpDrop();
            if (GameManager.instance.player.level >= 5 && GameManager.instance.player.level < 99)
            {
                
                if (randItem <= (10 + 5 * GameManager.instance.player.addAmmoChanceLevel))
                {
                    GameObject ammo = Instantiate(ammoPrefabs[0]);
                    ammo.transform.position = transform.position;
                    ammo.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.75f, 0.75f), 
                        Random.Range(-0.75f, 0.75f)), ForceMode2D.Impulse);
                }
            }
            
            if (randItem <= 2.5f + (2.5f * GameManager.instance.player.addItemChanceLevel))
            {
                GameObject supply = Instantiate(supplyPrefab);
                supply.transform.position = transform.position;
                Debug.Log("아이템 생성");
            }
            gameObject.SetActive(false);
        }
    }

    // 경험치 드랍 레벨에 따른 경험치 생성 개수 설정
    void ExpDrop()
    {
        // 1레벨 : 1개 90퍼, 2개 10퍼;
        // 2레벨 : 1개 70퍼, 2개 20퍼, 3개 10퍼;
        // 3레벨 : 1개 30퍼, 2개 40퍼, 3개 30퍼;
        // 4레벨 : 1개  0퍼, 2개 50퍼, 3개 30퍼, 4개 20퍼;
        // 5레벨 : 1개  0퍼, 2개  0퍼, 3개 60퍼, 4개 40퍼;
        int rand = Random.Range(1, 101);
        switch (GameManager.instance.player.addExpChanceLevel)
        {
            case 0:
                ExpChance(1);
                break;
            case 1:
                if (rand > 0 && rand <= 90)
                    ExpChance(1);
                else if (rand > 90 && rand <= 100)
                    ExpChance(2);
                break;
            case 2:
                if (rand > 0 && rand <= 70)
                    ExpChance(1);
                else if (rand > 70 && rand <= 90)
                    ExpChance(2);
                else if (rand > 90 && rand <= 100)
                    ExpChance(3);
                break;
            case 3:
                if (rand > 0 && rand <= 30)
                    ExpChance(1);
                else if (rand > 30 && rand <= 70)
                    ExpChance(2);
                else if (rand > 70 && rand <= 100)
                    ExpChance(3);
                break;
            case 4:
                if (rand > 0 && rand <= 50)
                    ExpChance(2);
                else if (rand > 50 && rand <= 80)
                    ExpChance(3);
                else if (rand > 80 && rand <= 100)
                    ExpChance(4);
                break;
            case 5:
                if (rand > 0 && rand <= 60)
                    ExpChance(3);
                else if (rand > 60 && rand <= 100)
                    ExpChance(4);
                break;
        }
    }

    void ExpChance(int count)
    {
        // count = 1이면 1개 생성 ....
        for (int i = 0; i < count; i++)
            ExpCreate();
    }

    void ExpCreate()
    {
        GameObject exp = Instantiate(expPrefab);
        exp.transform.position = transform.position;
        exp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.75f, 0.75f), 
            Random.Range(-0.75f, 0.75f)), ForceMode2D.Impulse);
    }
}
