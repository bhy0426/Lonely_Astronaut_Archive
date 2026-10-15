using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    public GameObject OXUI;

    public void CancelUpgrade()
    {
        OXUI.SetActive(false);
    }
}
