using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GameObject blood0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            collision.GetComponent<GetHit>().GetVertigo(damage);
        }
        else if (collision.CompareTag("BOSS"))
        {
            collision.GetComponent<EvilWizardGetHit>().GetVertigo(damage);
        }
        if (collision.CompareTag("Ground") ||
            collision.CompareTag("Enemy") ||
            collision.CompareTag("BOSS")) 
        {
            GameObject blood = ObjectPool.Instance.GetObject(blood0);
            blood.transform.position = transform.position;
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
