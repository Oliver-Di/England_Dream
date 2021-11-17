using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBead : MonoBehaviour
{
    public GameObject disappearVFXPrefab;
    public float addMp;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            collision.GetComponent<PlayerSkill>().KillRewardMp(addMp);
            DisappearVFX();
            DestroyThis();
        }
    }

    private void DisappearVFX()
    {
        GameObject dis = ObjectPool.Instance.GetObject(disappearVFXPrefab);
        dis.transform.position = transform.position;
    }

    private void DestroyThis()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
