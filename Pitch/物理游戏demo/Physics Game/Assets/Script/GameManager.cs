using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] tables;
    public Transform[] playerBorn;
    public Transform[] enemyBorn;

    public GameObject player;
    public GameObject enemy;
    public int number;

    public GameMode gameMode;
    public enum GameMode
    {
        Player,
        Enemy,
    }

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        //开局玩家先动
        gameMode = GameMode.Player;
    }

    public void ChangeTable()
    {
        if (number < 2)
            number++;
        else if (number == 2)
            number = 0;

        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].SetActive(false);
        }
        tables[number].SetActive(true);

        player.transform.position = playerBorn[number].position;
        enemy.transform.position = enemyBorn[number].position;
    }
}
