using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSpawnpoint : MonoBehaviour
{
    public Transform[] spawnPoint;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
}
