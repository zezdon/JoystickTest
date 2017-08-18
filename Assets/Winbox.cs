using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //Call LevelManager and method Victory
            LevelManager.Instance.Victory();
        }
    }
}
