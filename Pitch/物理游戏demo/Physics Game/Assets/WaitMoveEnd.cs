using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitMoveEnd : MonoBehaviour
{
    public float waitTime;

    private bool moving;
    private string lastTag;
    private Vector3 lastPos;
    private float lastTime;
    private GameObject lastObj;

    public static WaitMoveEnd instance;
    private void Awake()
    {
        //µ¥Àý
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
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
                GameManager.instance.gameMode = GameManager.GameMode.Enemy;
            else
                GameManager.instance.gameMode = GameManager.GameMode.Player;
        }
    }
}
