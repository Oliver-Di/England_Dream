using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodiesDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destory", 10);
    }

    private void Destroy()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
