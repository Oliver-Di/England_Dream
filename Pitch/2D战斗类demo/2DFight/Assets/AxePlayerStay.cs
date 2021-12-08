using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePlayerStay : MonoBehaviour
{
    public PolygonCollider2D polyCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            polyCollider.enabled = false;
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            polyCollider.enabled = true;
        }
    }
}
