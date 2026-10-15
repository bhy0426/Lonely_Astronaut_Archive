using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 target;
    float speed = 1f;
    int offsetX;
    int offsetY;
    int offsetSize = 3;
    float borderX = 10.5f;
    float borderY = 5.5f;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z);
        transform.position = target;
    }

    void LerpMove()
    {
        target = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);

        if (GameManager.instance.player.transform.position.x + borderX < GameManager.instance.player.mouse.x)
        {
            offsetX = offsetSize;
        }
        else if (GameManager.instance.player.transform.position.x + borderX > GameManager.instance.player.mouse.x && GameManager.instance.player.transform.position.x - borderX < GameManager.instance.player.mouse.x)
        {
            offsetX = 0;
        }
        else if (GameManager.instance.player.transform.position.x - borderX > GameManager.instance.player.mouse.x)
        {
            offsetX = -offsetSize;
        }
        if (GameManager.instance.player.transform.position.y + borderY < GameManager.instance.player.mouse.y)
        {
            offsetY = offsetSize;
        }
        else if (GameManager.instance.player.transform.position.y + borderY > GameManager.instance.player.mouse.y && GameManager.instance.player.transform.position.y - borderY < GameManager.instance.player.mouse.y)
        {
            offsetY = 0;
        }
        else if (GameManager.instance.player.transform.position.y - borderY > GameManager.instance.player.mouse.y)
        {
            offsetY = -offsetSize;
        }
        if (offsetX != 0 || offsetY != 0)
            speed = 1f;
        else
            speed = 5f;
        transform.position = Vector3.Lerp(transform.position, new Vector3(GameManager.instance.player.transform.position.x + offsetX, GameManager.instance.player.transform.position.y + offsetY, transform.position.z), speed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, target, 3f * Time.deltaTime);
    }
}
