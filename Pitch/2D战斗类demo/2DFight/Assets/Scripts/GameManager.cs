using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    }
}

public class MyTime
{
    public static float timescale = 1;
    public static float deltaTime
    {
        get
        {
            return Time.deltaTime * timescale;
        }
    }
}
