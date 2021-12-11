using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossTimeLineStart : MonoBehaviour
{
    public PlayableDirector bossPlayable;
    public GameObject boss;
    public Animator blackLine;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.gameMode = GameManager.GameMode.TimeLine;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            blackLine.SetTrigger("start");
            bossPlayable.Play();

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
