using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject walkerPrefab;

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

    public void CreateWalker(Vector3 pos)
    {
        GameObject walker = ObjectPool.Instance.GetObject(walkerPrefab);
        walker.transform.position = pos;
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
