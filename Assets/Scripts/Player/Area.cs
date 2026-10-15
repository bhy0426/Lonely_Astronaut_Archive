using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public Player player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
            collision.GetComponent<SpawnPoint>().isHit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
            collision.GetComponent<SpawnPoint>().isHit = false;
    }
}
