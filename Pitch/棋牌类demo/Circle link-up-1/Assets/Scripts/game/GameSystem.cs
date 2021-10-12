using UnityEngine;
//game flow control is here

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RestartGame();
    }

    public enum GameState
    {
        None,
        Wait,
        ChessClicked,
        Moving,
    }

    public GameState gameState;

    public void RestartGame()
    {
        gameState = GameState.Wait;
        ClearAllChess();
        TryGenerateInitialChess();
        //EFF
    }

    private void ClearAllChess()
    {

    }

    private bool TryGenerateInitialChess()
    {
        var suc = false;
        return suc;
    }
}
