using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    private float rotateZ;

    private void Update()
    {
        BladeSway();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //额外施加力
            Vector2 dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<PlayerGetHit>().GetHitBack(1, dir, 100);
            //眩晕主角
            collision.transform.GetComponent<PlayerGetHit>().Vertigo();
        }
    }

    private void BladeSway()
    {
        rotateZ = Mathf.Sin(Time.time);
        transform.rotation = Quaternion.Euler(0, 0, rotateZ * 60);
    }
}
