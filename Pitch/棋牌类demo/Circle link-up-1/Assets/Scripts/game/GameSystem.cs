using UnityEngine;
using System.Collections.Generic;
//game flow control is here

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        None,
        Wait,
        ChessClicked,
        Moving,
    }

    public GameState gameState;

    private void Start()
    {
        gameState = GameState.None;
    }

    public void RestartGame()
    {
        ClearBoard();

        int count = GameService.instance.gameConfig.startingChessCount;
        for (int i = 0; i < count; i++)
        {
            TrySpawnChess();
        }

        gameState = GameState.Wait;
    }

    private void ClearBoard()
    {
        Debug.Log("ClearBoard");
        foreach (var c in BoardService.instance.allArea)
        {
            c.RemoveChess();
        }
    }

    public void TestSpawnChess()
    {
        TrySpawnChess();
    }

    public bool TrySpawnChess()
    {
        var suc = false;
        var emptySlots = new List<SlotBehaviour>();
        foreach (var c in BoardService.instance.spawnArea)
        {
            if (c.chess == null)
            {
                emptySlots.Add(c);
            }
        }

        if (emptySlots.Count > 0)
        {
            var spawnChess = emptySlots[Random.Range(0, emptySlots.Count)];
            spawnChess.Spawn();
            suc = true;
        }

        return suc;
    }
}
