using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPos : MonoBehaviour
{
    Vector2 mouse;

    void LateUpdate()
    {
        if(GameManager.instance.player.curPlayerWeapon == Player.playerWeapon.sr)
        {
            transform.localPosition = new Vector3(0.9f, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0.6f, 0, 0);
        }
    }
}
