using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Golem : MonoBehaviour
{
    Rigidbody2D rigid;
    Rigidbody2D target;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    //public GameObject expPrefab;
    public GameObject DamageTextPrefab;
    //public GameObject supplyPrefab;
    public GameObject ammoPrefab;
    public Material[] materials;
    //Slider slider;
    //RectTransform rectTransform;
    //ParticleSystem particle;
    //SpriteRenderer spriter;
    public Sprite[] golemSpriteHead;
    public Sprite[] golemSpriteBody;
    public Sprite[] golemSpriteArm;
    public Sprite[] golemSpriteLegL;
    public Sprite[] golemSpriteLegR;
    public Animator animHead;
    public Animator animBody;
    public Animator animArmR;
    public Animator animArmL;
    public Animator animLegL;
    public Animator animLegR;
    public SpriteRenderer animHeadSpriter;
    public SpriteRenderer animBodySpriter;
    public SpriteRenderer animArmRSpriter;
    public SpriteRenderer animArmLSpriter;
    public SpriteRenderer animLegLSpriter;
    public SpriteRenderer animLegRSpriter;
    public BoxCollider2D atkAreaArm;
    public CapsuleCollider2D atkAreaLeg;
    public Animator animAtkArea;

    public bool isMove = false;
    public float health;
    public float maxHealth;
    public float speed;
    public float animSpeed;
    public float atk;
    public bool isHit;
    public bool isAtk;
    public int phase;

    Vector2 vel;

    void Start()
    {
        vel = Vector3.zero;
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //if (!isHit)
        //{

        if (health > 80)
        {
            phase = 0;
        }

        else if (health <= 80 && health >= 0)
        {
            phase = 1;
            speed = 2.5f;
            animSpeed = 2f;
        }

        else if(health < 0)
        {
            //animHead.gameObject.GetComponent<>
        }
        if (atkAreaArm.IsTouching(target.gameObject.GetComponent<Collider2D>()))
        {
            isAtk = true;
            animArmL.SetTrigger("isAtkArm");
            animArmR.SetTrigger("isAtk");
            animHead.SetTrigger("isAtk");
            animBody.SetTrigger("isAtk");
            animLegL.SetTrigger("isAtkArm");
            animLegR.SetTrigger("isAtk");
            animAtkArea.SetTrigger("isAtkArm");
        }
        else if (atkAreaLeg.IsTouching(target.gameObject.GetComponent<Collider2D>()))
        {
            isAtk = true;
            animArmL.SetTrigger("isAtkLeg");
            animArmR.SetTrigger("isAtk");
            animHead.SetTrigger("isAtk");
            animBody.SetTrigger("isAtk");
            animLegL.SetTrigger("isAtkLeg");
            animLegR.SetTrigger("isAtk");
            animAtkArea.SetTrigger("isAtkLeg");
        }
        if(!isAtk)
        {
            speed = 2f;
            animSpeed = 1.5f;
        }
        else
        {
            speed = 1f;
            animSpeed = 1f;
        }
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        isMove = true;
        //if (Vector2.Distance(target.position, rigid.position) >= 7f)
        //{
        //    //if (!isAtk)
        //    //{
        //        // 이동
        //
        //        //isAtk = false;
        //        animArmL.enabled = true;
        //        //animArmL.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        //    //}
        //}
        //else
        //{
            //Vector2 dirVec = target.position - rigid.position;
            //Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
            //animArmL.enabled = false;
            //animArmL.gameObject.GetComponent<Transform>().position = new Vector2(target.position.x, target.position.y + 5f);
            //animArmL.gameObject.GetComponent<Rigidbody2D>().MovePosition(rigid.position + nextVec);
            //animArmL.enabled = true;

            //isAtk = true;
            //speed = 1.5f;
            
            
            //animArmL.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            //isMove = false;
        //}
        //}
    }

    private void LateUpdate()
    {
        animHeadSpriter.sprite = golemSpriteHead[phase];
        animBodySpriter.sprite = golemSpriteBody[phase];
        animArmLSpriter.sprite = golemSpriteArm[phase];
        animArmRSpriter.sprite = golemSpriteArm[phase];
        animLegLSpriter.sprite = golemSpriteLegL[phase];
        animLegRSpriter.sprite = golemSpriteLegR[phase];
        animArmL.SetFloat("animSpeed", animSpeed);
        animArmR.SetFloat("animSpeed", animSpeed);
        animHead.SetFloat("animSpeed", animSpeed);
        animBody.SetFloat("animSpeed", animSpeed);
        animLegL.SetFloat("animSpeed", animSpeed);
        animLegR.SetFloat("animSpeed", animSpeed);
        animAtkArea.SetFloat("animSpeed", animSpeed);
        if (target.position.x > rigid.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (target.position.x > rigid.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        GameManager.instance.sliderBoss.value = health / maxHealth;
        animHead.SetBool("isMove", isMove);
        animBody.SetBool("isMove", isMove);
        animArmL.SetBool("isMove", isMove);
        animArmR.SetBool("isMove", isMove);
        animLegL.SetBool("isMove", isMove);
        animLegR.SetBool("isMove", isMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //rigid.isKinematic = false;
        //if (!atkAreaArm.IsTouching(target.gameObject.GetComponent<Collider2D>()) && !atkAreaLeg.IsTouching(target.gameObject.GetComponent<Collider2D>()))
        //{
            if (collision.gameObject.tag == "Bullet")
            {
                Hit(collision.GetComponent<BulletManager>().myBulletData.damage, collision.GetComponent<BulletManager>().myBulletData.critChance, collision.GetComponent<BulletManager>().myBulletData.critDamage, 0.25f, Player.playerWeapon.handGun, collision.transform);
            }
            else if (collision.gameObject.tag == "BulletShotgun")
            {
                Hit(collision.GetComponent<BulletManager>().myBulletData.damage, collision.GetComponent<BulletManager>().myBulletData.critChance, collision.GetComponent<BulletManager>().myBulletData.critDamage, 0.25f, Player.playerWeapon.sg, collision.transform);
            }
            else if (collision.gameObject.tag == "Melee")
            {
                Hit(GameManager.instance.playerAtk.meleeDamage, GameManager.instance.playerAtk.meleeCritChance, GameManager.instance.playerAtk.meleeCritDamage, 0.25f, Player.playerWeapon.melee, collision.transform);
            }
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Melee")
        {
            if (!isHit)
            {
                Hit(GameManager.instance.playerAtk.meleeDamage, GameManager.instance.playerAtk.meleeCritChance, GameManager.instance.playerAtk.meleeCritDamage, 0.25f, Player.playerWeapon.melee, collision.transform);
            }
        }
    }

    void Hit(float damage, float critChance, float critDamage, float cooldown, Player.playerWeapon audioType, Transform spawnPosition)
    {

        //animArmL.gameObject.GetComponent<SpriteRenderer>().material = materials[1];
        //Debug.Log("HIt!");
        switch (audioType)
        {
            case Player.playerWeapon.melee:
                GameManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip[0]);
                break;
            default:
                GameManager.instance.GetComponent<AudioSource>().PlayOneShot(audioClip[1]);
                break;
        }
        // 1 ~ 100
        GameObject damageText = Instantiate(DamageTextPrefab);
        damageText.transform.position = spawnPosition.position;
        int randCrit = Random.Range(1, 101);
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
        int randAmmo = Random.Range(1, 101);
        if(randAmmo >= 1 && randAmmo <= 5)
        {
            GameObject ammo = Instantiate(ammoPrefab);
            ammo.transform.position = transform.position;
            ammo.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 2),
                Random.Range(-1, 2)) * Random.Range(5.0f, 8.0f), ForceMode2D.Impulse);
        }
        // SpriteRenderer의 Material 변경
        //spriter.material = materials[1];
        //GetComponent<SpriteRenderer>().color = Color.red;
        // isHit를 참으로 변경
        isHit = true;
        // 포지션 고정
        rigid.constraints = ~RigidbodyConstraints2D.FreezePosition;
        Vector3 dirVec = target.position - rigid.position;
        //rigid.MovePosition(transform.position + (-dirVec.normalized * 0.1f));

        health -= damage;
        Invoke("HitCoolDown", cooldown);
        //GetComponent<SpriteRenderer>().color = Color.red;
        //particle.Play();
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();
        //damageText.GetComponent<RectTransform>().transform.position = transform.position;

    }

    void HitCoolDown()
    {
        isHit = false;
        //animArmL.gameObject.GetComponent<SpriteRenderer>().material = materials[0];
        rigid.velocity = Vector2.zero;
        //GetComponent<SpriteRenderer>().color = Color.white;
    }

    void EndAtk()
    {
        isAtk = false;
    }
}
