using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public float time;
    public float hp;

    private void OnEnable()
    {
        TryToCreate();
    }

    private void Update()
    {
        
    }

    private void TryToCreate()
    {
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        GameManager.instance.CreateWalker(transform.position);
        yield return new WaitForSeconds(time);
        TryToCreate();
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
