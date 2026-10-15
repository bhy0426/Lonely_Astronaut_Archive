using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAtkStop : MonoBehaviour
{
    Golem golem;

    private void Awake()
    {
        golem = GetComponentInParent<Golem>();
    }

    public void EndAtk()
    {
        golem.isAtk = false;
    }
}
