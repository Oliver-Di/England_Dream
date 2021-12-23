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
    private List<GameObject> outOfArea = new List<GameObject>();

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
            //���������������enemyС����������-1
            if (WinAreaManager.instance.enemys.Count < enemys.Count - 1) 
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)//50%���ʹ���������Ŀ��
                {
                    FindNearestObjAttack(WinAreaManager.instance.players[0].transform, enemys);
                    Debug.Log("random attack player inside");
                }
                else//50%��һ��enemy��������
                {
                    EnterArea();
                    Debug.Log("random occupy");
                }
            }
            else
            {
                //����������Ŀ��
                FindNearestObjAttack(WinAreaManager.instance.players[0].transform, enemys);
                Debug.Log("attack player inside");
            }
        }
        else
        {
            //������player������enemy
            if (WinAreaManager.instance.enemys.Count != 0)
            {
                FindObjOutside();
                //�����Ϊ�գ������һ��Ŀ��
                if (outOfArea.Count == 0)
                {
                    int rand = Random.Range(0, players.Count);
                    FindNearestObjAttack(players[rand].transform, enemys);
                }
                else
                {
                    //����鲻Ϊ�գ������һ��Ŀ��������obj����
                    int rand = Random.Range(0, players.Count);
                    FindNearestObjAttack(players[rand].transform, outOfArea);
                }
                Debug.Log("attack player outside");
            }
            else    //�������κ�obj
            {
                EnterArea();
                Debug.Log("occupy");
            }
        }
    }
    //�ҵ����������ڵ�enemy
    private void FindObjOutside()
    {
        outOfArea.Clear();

        foreach (var obj in enemys)
        {
            if (!WinAreaManager.instance.enemys.Contains(obj))
                outOfArea.Add(obj);
        }
    }

    private void EnterArea()
    {
        FindObjOutside();
        //�������ҵ���Area�����enemy��ռ������
        FindNearestObjAttack(areaPos, outOfArea);
    }

    //�ҵ���ѡ��Ŀ�������enemy������Ŀ��
    private void FindNearestObjAttack(Transform target,List<GameObject> list)
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

        //����ײ��
        selectedObj.GetComponent<Enemy>().target = target.gameObject;
        selectedObj.GetComponent<Enemy>().force *= 0.5f;
        selectedObj.GetComponent<Enemy>().Attack();
        selectedObj.GetComponent<Enemy>().force *= 2;
    }
}
