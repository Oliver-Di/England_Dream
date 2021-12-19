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
    private List<GameObject> canAttackObj;

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
        //ռ���������player
        if (WinAreaManager.instance.players.Count != 0)
        {
            //ѡ��Area��ĵ�һ��PlayerΪĿ�꣬�ҵ��������enemy
            FindNearestObj(WinAreaManager.instance.players[0].transform,enemys);
            //����ײ��
            selectedObj.GetComponent<Enemy>().target = WinAreaManager.instance.players[0];
            selectedObj.GetComponent<Enemy>().canAttack = true;
        }
        else
        {
            //������player������enemy
            if (WinAreaManager.instance.enemys.Count != 0)
            {
                //����һ������Area����
                canAttackObj = enemys;
                for (int i = 0; i < WinAreaManager.instance.enemys.Count; i++)
                {
                    canAttackObj.Remove(WinAreaManager.instance.enemys[i]);
                }
                if (canAttackObj.Count == 0)
                {
                    //�����Ϊ�գ������һ��Ŀ��
                    int rand = Random.Range(0, players.Count);
                    FindNearestObj(players[rand].transform, enemys);
                    //ײ��
                    selectedObj.GetComponent<Enemy>().target = players[rand];
                    selectedObj.GetComponent<Enemy>().canAttack = true;
                }
                else
                {
                    //����鲻Ϊ�գ������һ��Ŀ��������obj����
                    int rand = Random.Range(0, players.Count);
                    FindNearestObj(players[rand].transform, canAttackObj);
                    //ײ��
                    selectedObj.GetComponent<Enemy>().target = players[rand];
                    selectedObj.GetComponent<Enemy>().canAttack = true;
                }
            }
            else    //�������κ�obj
            {
                //�ҵ���Area�����enemy��ռ������
                FindNearestObj(areaPos,enemys);
                //����ײ��
                selectedObj.GetComponent<Enemy>().target = areaPos.gameObject;
                selectedObj.GetComponent<Enemy>().canAttack = true;
            }
        }
    }

    private void FindNearestObj(Transform target,List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            float dis = Vector3.Distance(list[i].transform.position, target.position);
            if (i == 0)
            {
                distance = dis;
                selectedObj = list[i];
            }
            else
            {
                if (dis < distance)
                {
                    distance = dis;
                    selectedObj = list[i];
                }
            }
        }
    }
}
