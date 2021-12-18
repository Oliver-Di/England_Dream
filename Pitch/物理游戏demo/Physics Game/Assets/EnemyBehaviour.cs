using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform areaPos;
    public List<GameObject> enemys;
    public GameObject playerTeam;
    public List<GameObject> players;

    private float distance;
    private GameObject selectedObj;

    void Start()
    {
        FindAllObj();
    }

    private void FindAllObj()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Enemy")
                enemys.Add(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerTeam.transform.childCount; i++)
        {
            if (playerTeam.transform.GetChild(i).tag == "Player")
                players.Add(playerTeam.transform.GetChild(i).gameObject);
        }
    }

    public void ChooseObjMove()
    {
        //占领区域存在player
        if (WinAreaManager.instance.players.Count != 0)
        {
            //选定Area里的第一个Player为目标，找到其最近的enemy
            FindNearestObj(WinAreaManager.instance.players[0].transform);
            //进行撞击
            selectedObj.GetComponent<Enemy>().target = WinAreaManager.instance.players[0];
            selectedObj.GetComponent<Enemy>().canAttack = true;
        }
        else
        {
            //不存在player但存在enemy
            if (WinAreaManager.instance.enemys.Count != 0)
            {
                //随机一个目标
                int rand = Random.Range(0, players.Count);
                FindNearestObj(players[rand].transform);
                //撞击
                selectedObj.GetComponent<Enemy>().target = players[rand];
                selectedObj.GetComponent<Enemy>().canAttack = true;
            }
            else    //不存在任何obj
            {
                //找到离Area最近的enemy，占领区域
                FindNearestObj(areaPos);
                //进行撞击
                selectedObj.GetComponent<Enemy>().target = areaPos.gameObject;
                selectedObj.GetComponent<Enemy>().canAttack = true;
            }
        }
    }

    private void FindNearestObj(Transform target)
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            float dis = Vector3.Distance(enemys[i].transform.position, target.position);
            if (i == 0)
            {
                distance = dis;
                selectedObj = enemys[i];
            }
            else
            {
                if (dis < distance)
                {
                    distance = dis;
                    selectedObj = enemys[i];
                }
            }
        }
    }
}
