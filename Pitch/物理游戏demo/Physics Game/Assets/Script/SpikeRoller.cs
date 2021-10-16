using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRoller : MonoBehaviour
{
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().
            AddForce((other.transform.position - transform.position) * 10000 * force);
    }
}
