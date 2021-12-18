using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject table;
    [Header("位置信息")]
    public GameObject[] tables;
    public Transform[] playerBorn;
    public Transform[] enemyBorn;
    public GameObject[] objects;
    [Header("玩家信息")]
    public GameObject player;
    public GameObject enemy;
    public int tableNum;
    public int playerNum;
    [Header("UI")]
    public GameObject victory;
    public GameObject fail;
    public GameObject ready;
    [Header("摄像机")]
    public GameObject folllowCam;
    public GameObject StartCam;

    public GameMode gameMode;
    public enum GameMode
    {
        Menu,
        Ready,
        Player,
        Enemy,
        Waiting,
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

        //开局等待
        gameMode = GameMode.Menu;
        Time.timeScale = 0;
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        ready.SetActive(false);
        StartCam.SetActive(false);

        //激活选中物块
        for (int i = 0; i < objects.Length; i++)
        {
            if (i == playerNum)
                objects[i].SetActive(true);
            else
                objects[i].SetActive(false);
        }
        //玩家操作模式
        gameMode = GameMode.Player;
        Time.timeScale = 1;

        findObject();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void findObject()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        //相机寻找玩家
        folllowCam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }

    public void ChangeTable()
    {
        if (tableNum < tables.Length-1)
            tableNum++;
        else if (tableNum == tables.Length-1)
            tableNum = 0;
        //换桌子
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i].SetActive(false);
        }
        tables[tableNum].SetActive(true);
        //结算界面关闭
        victory.SetActive(false);
        fail.SetActive(false);

        //重新寻找人物和敌人
        findObject();

        //人物和敌人激活
        player.SetActive(true);
        enemy.SetActive(true);
        //人物和敌人速度清零
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //人物和敌人位置归正
        player.transform.position = playerBorn[tableNum].position;
        enemy.transform.position = enemyBorn[tableNum].position;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        enemy.transform.rotation = Quaternion.Euler(0, 0, 0);

        Debug.Log("change");
    }

    public void RestartTable()
    {
        //重新寻找人物和敌人
        findObject();
        //人物和敌人激活
        player.SetActive(true);
        enemy.SetActive(true);
        //人物和敌人速度清零
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //人物和敌人位置归正
        player.transform.position = playerBorn[tableNum].position;
        enemy.transform.position = enemyBorn[tableNum].position;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        enemy.transform.rotation= Quaternion.Euler(0, 0, 0);

        //结算界面关闭
        victory.SetActive(false);
        fail.SetActive(false);

        Debug.Log("restart");
    }

    public void WinGame()
    {
        victory.SetActive(true);
        gameMode = GameMode.Player;
    }

    public void FailGame()
    {
        fail.SetActive(true);
        gameMode = GameMode.Player;
    }
}
