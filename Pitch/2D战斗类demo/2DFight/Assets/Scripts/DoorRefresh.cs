using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRefresh : MonoBehaviour
{
    public GameObject door;

    private GameObject monster;

    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("Monster");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && door.GetComponent<Door>().noMonster == false) 
        {
            CheckMonster();
        }
    }

    private void CheckMonster()
    {
        if (monster.transform.childCount == 0)
        {
            door.GetComponent<Door>().noMonster = true;
            door.GetComponent<Animator>().SetTrigger("open");
        }
    }
}
