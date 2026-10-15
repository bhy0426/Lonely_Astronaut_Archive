using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void LateUpdate()
    {
        if(GameManager.instance.golemPrefab.phase == 0)
        {
            GameManager.instance.light.intensity = 1f;
        }
        else if (GameManager.instance.golemPrefab.phase == 1)
        {
            GameManager.instance.light.intensity = 0.8f;
        }
    }
}
