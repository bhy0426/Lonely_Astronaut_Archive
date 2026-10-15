using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform shotPos;
    public float speed;
    float angle;
    int sniperPentCount;
    Vector2 mouse, target, targetOrigin, dest;
    
    void Awake()
    {
        shotPos = GameManager.instance.hand.GetComponentInChildren<ShotPos>().transform;
        rigid = GetComponent<Rigidbody2D>();
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector2(shotPos.position.x - GameManager.instance.player.transform.position.x, shotPos.position.y - GameManager.instance.player.transform.position.y);
        angle = Mathf.Atan2(mouse.y - GameManager.instance.hand.transform.position.y, mouse.x - GameManager.instance.hand.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        //targetOrigin = target;
        //dest = new Vector2(targetOrigin.x + Random.Range(-10, 20), targetOrigin.x + Random.Range(-10, 20));
    }

    void FixedUpdate()
    {

        //target = Vector2.SmoothDamp(target, dest, ref target, 1f);
        rigid.MovePosition(rigid.position + (target.normalized * 30f * Time.fixedDeltaTime));

        //transform.Translate(target.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") || collision.CompareTag("Golem"))
        {
            sniperPentCount++;
            if (GameManager.instance.player.curPlayerWeapon != Player.playerWeapon.sr)
                Destroy(gameObject);
            else
            {
                if (sniperPentCount >= 2)
                    Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

    }
}

