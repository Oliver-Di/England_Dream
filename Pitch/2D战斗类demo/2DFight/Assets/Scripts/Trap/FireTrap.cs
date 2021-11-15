using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float timer;
    public GameObject fireColumn;
    
    private Animator anim;
    private bool isTriggering;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") &&
            isTriggering == false) 
        {
            isTriggering = true;
            Invoke("FireColumn", timer);
        }
    }

    private void FireColumn()
    {
        GameObject fire = ObjectPool.Instance.GetObject(fireColumn);
        fire.transform.position = transform.position;
        isTriggering = false;
    }
}
