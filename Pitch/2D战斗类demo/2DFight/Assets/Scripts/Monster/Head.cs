using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public int headInt;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
