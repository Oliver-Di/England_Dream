using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingBehaviour : MonoBehaviour
{
    public float waitTime;
    public GameObject EnemyBehaviour;

    private bool moving;
    private string lastTag;
    private Vector3 lastPos;
    private float lastTime;
    private GameObject lastObj;

    public static WaitingBehaviour instance;
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

    void Update()
    {
        if(moving)
            WaitObjStop();
    }

    public void WaitingMove(GameObject obj)
    {
        lastPos = obj.transform.position;
        lastTag = obj.tag;
        lastObj = obj;
        lastTime = Time.time;

        GameManager.instance.gameMode = GameManager.GameMode.Waiting;

        moving = true;
    }

    private void WaitObjStop()
    {
        if (lastPos != lastObj.transform.position)
        {
            lastPos = lastObj.transform.position;
            lastTime = Time.time;
        }
        if (Time.time - lastTime > waitTime)
        {
            lastTime = Time.time;
            if (lastTag == "Player")
            {
                GameManager.instance.gameMode = GameManager.GameMode.Enemy;
                EnemyBehaviour.GetComponent<EnemyBehaviour>().ChooseObjMove();
            }
            else
                GameManager.instance.gameMode = GameManager.GameMode.Player;

            //刷新占领清空
            WinAreaManager.instance.RefreshOccupySituation();
        }
    }
}
