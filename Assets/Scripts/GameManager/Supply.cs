using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    public Rigidbody2D target;
    public GameObject infoText;
    public Sprite[] sprites = new Sprite[3];

    public Player.ItemType itemType;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        itemType = (Player.ItemType)(Random.Range(1, 4));
        
        spriter.sprite = sprites[(int)itemType];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, rigid.position) <= 1f)
        {
            if (itemType != Player.ItemType.hp)
                transform.position = Vector2.Lerp(rigid.position, target.position, Time.deltaTime * 5f);
            else
            {
                if (GameManager.instance.player.health < GameManager.instance.player.maxHealth)
                {
                    transform.position = Vector2.Lerp(rigid.position, target.position, Time.deltaTime * 5f);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(itemType)
            {
                case Player.ItemType.hp:
                    if (GameManager.instance.player.health < GameManager.instance.player.maxHealth)
                    {
                        if (GameManager.instance.player.health <= GameManager.instance.player.maxHealth - 10)
                            GameManager.instance.player.health += 10;
                        else
                            GameManager.instance.player.health = GameManager.instance.player.maxHealth;

                        Destroy(gameObject);
                    }
                    break;
                case Player.ItemType.shield:
                    GameManager.instance.player.battery += 5;
                    Destroy(gameObject);
                    break;
                case Player.ItemType.invisible:
                    GameManager.instance.player.curPlayerItem = Player.ItemType.invisible;
                    Debug.Log("투명화 적용");
                    Destroy(gameObject);
                    break;
            }

            
            
        }
    }
}
