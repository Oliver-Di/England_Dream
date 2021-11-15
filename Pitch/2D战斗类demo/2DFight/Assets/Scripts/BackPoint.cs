using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPoint : MonoBehaviour
{
    public Transform backPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = backPos.position;
        }
    }
}
