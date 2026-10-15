using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isHit;

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag("Area"))
            isHit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
            isHit = false;
    }
}
