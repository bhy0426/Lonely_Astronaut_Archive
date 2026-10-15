using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneWep : MonoBehaviour
{
    public Drone drone;
    public Transform target;
    public GameObject bulletPrefab;
    [SerializeField]
    private Laser laser;
    public float curTime;
    public float atkSpeed;

    private void Awake()
    {
        target = GameManager.instance.player.transform;
    }

    void Update()
    {
        Vector3 dir = (target.position - transform.position);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        if (drone.atkReady)
        {
            laser.Play(new Vector3(0, 0, 0), dir);
            laser.ChangeColor(atkSpeed, curTime);
            if (curTime >= atkSpeed)
            {
                GameObject bullet = Instantiate(bulletPrefab, drone.transform.position, drone.transform.rotation);
                //Debug.Log("Shoot!");
                curTime = 0;
            }
            curTime += Time.deltaTime;
        }
        else
        {
            laser.Stop();
            curTime = 0;
        }

    }


}
