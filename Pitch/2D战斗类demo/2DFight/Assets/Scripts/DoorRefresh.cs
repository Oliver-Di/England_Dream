using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRefresh : MonoBehaviour
{
    private GameObject monster;
    public GameObject door;

    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    private void CheckMonster()
    {
        if (monster.transform.childCount == 0)
        {
            door.GetComponent<Door>().noMonster = true;
        }
    }
}
