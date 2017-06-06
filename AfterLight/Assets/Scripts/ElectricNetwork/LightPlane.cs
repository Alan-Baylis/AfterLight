using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlane : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.JumpBack();
        }
    }
}
