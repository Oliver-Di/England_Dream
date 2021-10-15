using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public ChessBehaviour prefabChess;

    public Transform parent;

    void Start()
    {
        CreateAllChess();
    }

    void CreateAllChess()
    {
        prefabChess.gameObject.SetActive(true);

        var chess1 = Instantiate(prefabChess, BoardService.instance.roundCenter);//center 1
        chess1.transform.SetParent(parent);
        chess1.gameObject.name = "c-center";

        BoardService.instance.center = chess1;

        for (int i = 0; i < 6; i++)
        {
            var deg = i * 60 - 180;
            var x = Mathf.Cos(deg * Mathf.Deg2Rad) * BoardService.instance.radius * 1;
            var z = Mathf.Sin(-deg * Mathf.Deg2Rad) * BoardService.instance.radius * 1;
            var chess = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 1
            chess.transform.SetParent(parent);
            chess.gameObject.name = "c-r1-" + i;
            BoardService.instance.ring1.Add(chess);
        }

        for (int i = 0; i < 12; i++)
        {
            var deg = i * 30 - 180;
            var x = Mathf.Cos(deg * Mathf.Deg2Rad) * BoardService.instance.radius * 2;
            var z = Mathf.Sin(-deg * Mathf.Deg2Rad) * BoardService.instance.radius * 2;
            var chess = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 2
            chess.transform.SetParent(parent);
            chess.gameObject.name = "c-r2-" + i;
            BoardService.instance.ring2.Add(chess);
        }

        for (int i = 0; i < 18; i++)
        {
            var deg = i * 20 - 180;
            var x = Mathf.Cos(deg * Mathf.Deg2Rad) * BoardService.instance.radius * 3;
            var z = Mathf.Sin(-deg * Mathf.Deg2Rad) * BoardService.instance.radius * 3;
            var chess = Instantiate(prefabChess, BoardService.instance.roundCenter.position + new Vector3(x, 0, z), Quaternion.identity);//ring 3
            chess.transform.SetParent(parent);
            chess.gameObject.name = "c-r3-" + i;
            BoardService.instance.ring3.Add(chess);
        }

        BoardService.instance.InitChessLists();

        prefabChess.gameObject.SetActive(false);
    }
}
