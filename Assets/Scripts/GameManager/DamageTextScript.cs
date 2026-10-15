using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
    public void Update()
    {
        transform.position += Vector3.up * 0.002f;
    }
    public void Delete()
    {
        Destroy(gameObject);
    }
}
