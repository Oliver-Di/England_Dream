using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Up : MonoBehaviour
{
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
        }
    }
}
