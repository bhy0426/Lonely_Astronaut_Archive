using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrone : MonoBehaviour
{
    public float speed;
    float angle;
    Rigidbody2D rigid;
    Vector2 target, dir;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player.transform.position;
        dir = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, dir);
        rigid.MovePosition(rigid.position + (dir.normalized * speed));
        //transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.GetComponent<Player>().HitDrone();
        }
        else if (collision.CompareTag("Border"))
            Destroy(gameObject);
    }
}
