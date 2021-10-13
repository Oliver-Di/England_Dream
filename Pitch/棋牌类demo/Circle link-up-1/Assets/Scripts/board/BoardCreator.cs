using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public GameObject prefabChess;

    public Transform parent;
    public bool toCreate;

    void Start()
    {
        if (toCreate)
            CreateAllChess();
    }

    void CreateAllChess()
    {
        var go1 = Instantiate(prefabChess, BoardService.instance.roundCenter);//center 1
        go1.SetActive(true);
        go1.transform.SetParent(parent);

        for (int i = 0; i < 6; i++)
        {
            var x = Mathf.Cos(i * 60 * Mathf.Deg2Rad) * BoardService.instance.radius * 1;
            var z = Mathf.Sin(i * 60 * Mathf.Deg2Rad) * BoardService.instance.radius * 1;
            var go = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 1
            go.SetActive(true);
            go.transform.SetParent(parent);
        }

        for (int i = 0; i < 12; i++)
        {
            var x = Mathf.Cos(i * 30 * Mathf.Deg2Rad) * BoardService.instance.radius * 2;
            var z = Mathf.Sin(i * 30 * Mathf.Deg2Rad) * BoardService.instance.radius * 2;
            var go = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 2
            go.SetActive(true);
            go.transform.SetParent(parent);
            go.transform.SetParent(parent);
        }

        for (int i = 0; i < 18; i++)
        {
            var x = Mathf.Cos(i * 20 * Mathf.Deg2Rad) * BoardService.instance.radius * 3;
            var z = Mathf.Sin(i * 20 * Mathf.Deg2Rad) * BoardService.instance.radius * 3;
            var go = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 3
            go.SetActive(true);
            go.transform.SetParent(parent);
        }
    }
}
