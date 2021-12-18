using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreaManager : MonoBehaviour
{
    private bool playerOccupy;
    private bool enemyOccupy;
    private int playerNum;
    private int enemyNum;
    private int playerOccupyNum;
    private int enemyOccupyNum;

    public List<GameObject> players;
    public List<GameObject> enemys;

    public static WinAreaManager instance;
    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNum++;
            players.Add(other.gameObject);
            playerOccupy = true;
        }

        if (other.CompareTag("Enemy"))
        {
            enemyNum++;
            enemys.Add(other.gameObject);
            enemyOccupy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNum--;
            players.Remove(other.gameObject);
            if(playerNum==0)
                playerOccupy = false;
        }

        if (other.CompareTag("Enemy"))
        {
            enemyNum--;
            enemys.Remove(other.gameObject);
            if(enemyNum==0)
                enemyOccupy = false;
        }
    }

    public void RefreshOccupySituation()
    {
        if (playerOccupy)
        {
            if (!enemyOccupy)
            {
                //player占领回合+1
                playerOccupyNum++;
                //清空enemy回合数
                enemyOccupyNum = 0;
            }
        }
        else
        {
            if (enemyOccupy)
            {
                //enemy占领回合+1
                enemyOccupyNum++;
                //清空player回合数
                playerOccupyNum = 0;
            }
            else
            {
                //清空双方回合数
                enemyOccupyNum = 0;
                playerOccupyNum = 0;
            }
        }
    }
}
