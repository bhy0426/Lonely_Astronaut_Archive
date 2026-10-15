using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform[] spawnPoint;
    public ShotgunSpawnpoint sgSpawnPoint;
    public float speed;
    float angle;
    Vector2 mouse, target, targetOrigin, dest;

    void Awake()
    {
        sgSpawnPoint = GameManager.instance.hand.GetComponentInChildren<ShotgunSpawnpoint>();
        spawnPoint = sgSpawnPoint.spawnPoint;
        rigid = GetComponent<Rigidbody2D>();
        //mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector2(spawnPoint[Random.Range(1, spawnPoint.Length - 1)].transform.position.x - GameManager.instance.player.transform.position.x, spawnPoint[Random.Range(1, spawnPoint.Length - 1)].transform.position.y - GameManager.instance.player.transform.position.y);
        //angle = Mathf.Atan2(mouse.y - GameManager.instance.player.transform.position.y, mouse.x - GameManager.instance.player.transform.position.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        rigid.AddForce(target * speed, ForceMode2D.Impulse);
        //targetOrigin = target;
        //dest = new Vector2(targetOrigin.x + Random.Range(-10, 20), targetOrigin.x + Random.Range(-10, 20));
    }

    void Update()
    {

        //target = Vector2.SmoothDamp(target, dest, ref target, 1f);


        //transform.Translate(target.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Golem"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Border"))
            Destroy(gameObject);


    }


}
