using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject activeCM_left;
    public GameObject activeCM_right;
    public GameObject activeCM_up;
    public GameObject activeCM_down;

    public GameObject[] allCM;

    private float distance;
    private int num;
    private Transform centerPoint;

    public List<GameObject> CM_left = new List<GameObject>();
    public List<GameObject> CM_right = new List<GameObject>();
    public List<GameObject> CM_up = new List<GameObject>();
    public List<GameObject> CM_down = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        centerPoint = GameObject.FindGameObjectWithTag("CenterPoint").transform;
    }

    public void CreatTeam()
    {
        //计算激活的棋子所在圈的棋子个数
        distance = Vector3.Distance(activeCM_left.transform.position, centerPoint.position);
        if (distance < 1.5f)
            num = 6;
        else if (distance > 1.5f && distance < 2.5f)
            num = 12;
        else if (distance > 2.5f)
            num = 18;
            
        //向左队列
        for (int i = 0; i < num; i++)
        {
            if (activeCM_left.GetComponent<Chessman>().hitInfo_left.collider != null &&
                !CM_left.Contains(activeCM_left.GetComponent<Chessman>().hitInfo_left.collider.gameObject))
            {
                activeCM_left = activeCM_left.GetComponent<Chessman>().hitInfo_left.collider.gameObject;
                CM_left.Add(activeCM_left);
            }
            else
                break;
        }

        //向右队列
        for (int i = 0; i < num; i++)
        {
            if (activeCM_right.GetComponent<Chessman>().hitInfo_right.collider != null &&
                !CM_right.Contains(activeCM_right.GetComponent<Chessman>().hitInfo_right.collider.gameObject))
            {
                activeCM_right = activeCM_right.GetComponent<Chessman>().hitInfo_right.collider.gameObject;
                CM_right.Add(activeCM_right);
            }
            else
                break;
        }

        //向上队列
        for (int i = 0; i < num; i++)
        {
            if (activeCM_up.GetComponent<Chessman>().hitInfo_up.collider != null &&
                !CM_up.Contains(activeCM_up.GetComponent<Chessman>().hitInfo_up.collider.gameObject))
            {
                activeCM_up = activeCM_up.GetComponent<Chessman>().hitInfo_up.collider.gameObject;
                CM_up.Add(activeCM_up);
            }
            else
                break;
        }

        //向下队列
        for (int i = 0; i < num; i++)
        {
            if (activeCM_down.GetComponent<Chessman>().hitInfo_down.collider != null &&
                !CM_down.Contains(activeCM_down.GetComponent<Chessman>().hitInfo_down.collider.gameObject))
            {
                activeCM_down = activeCM_down.GetComponent<Chessman>().hitInfo_down.collider.gameObject;
                CM_down.Add(activeCM_down);
            }
            else
                break;
        }
    }
    
    //向左队列旋转
    public void LeftTeamRotate()
    {
        for (int i = 0; i < num; i++)
        {
            if (activeCM_left.GetComponent<Chessman>().hitInfo_left.collider != null &&
                !CM_left.Contains(activeCM_left.GetComponent<Chessman>().hitInfo_left.collider.gameObject))
            {
                activeCM_left.GetComponent<Chessman>().hitInfo_left.collider.GetComponent<Chessman>().rotate = true;
            }
            else
                break;
        }
    }

    //向右队列旋转
    public void RightTeamRotate()
    {
        for (int i = 0; i < num; i++)
        {
            if (activeCM_right.GetComponent<Chessman>().hitInfo_right.collider != null &&
                !CM_right.Contains(activeCM_right.GetComponent<Chessman>().hitInfo_right.collider.gameObject))
            {
                activeCM_right.GetComponent<Chessman>().hitInfo_right.collider.GetComponent<Chessman>().rotate = true;
            }
            else
                break;
        }
    }

    public void ClearTeam()
    {
        //清空移动组
        CM_left.Clear();
        CM_right.Clear();
        CM_up.Clear();
        CM_down.Clear();
        //关闭旋转，调整位置
        for (int i = 0; i < allCM.Length; i++)
        {
            allCM[i].GetComponent<Chessman>().AdjustPosition();
        }
    }
}
