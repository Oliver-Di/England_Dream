using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroier : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 1);
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
