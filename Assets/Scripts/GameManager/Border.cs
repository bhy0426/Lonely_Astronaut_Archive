using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position;
    }
}
