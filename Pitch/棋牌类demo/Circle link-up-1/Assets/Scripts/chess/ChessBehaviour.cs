using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBehaviour : MonoBehaviour
{
    public static List<ChessBehaviour> instances;

    public ChessData data;

    public GameObject viewChess;
    public GameObject viewA;
    public GameObject viewB;
    public GameObject viewC;
    public GameObject viewD;
    public GameObject viewE;

    public GameObject viewCurrentTarget;
    public GameObject viewCanClick;
    public GameObject viewMultiPushFrom;
    public GameObject viewMultiPushTo;

    public ParticleSystem psErase;
    public ParticleSystem psSpawn;

    private bool _isCurrentTarget = false;
    private bool _isClickableGoal = false;
    private bool _isPushFrom = false;
    private bool _isPushTo = false;

    void Awake()
    {
        if (instances == null)
            instances = new List<ChessBehaviour>();
        instances.Add(this);
    }

    public void SetChessTypeNone()
    {
        SetChessType(ChessData.ChessType.None);
    }

    public void SetChessType(ChessData.ChessType type)
    {
        data.chessType = type;
        SetChessType();
    }

    public void SetChessType()
    {
        viewChess.SetActive(true);
        viewA.SetActive(false);
        viewB.SetActive(false);
        viewC.SetActive(false);
        viewD.SetActive(false);
        viewE.SetActive(false);

        switch (data.chessType)
        {
            case ChessData.ChessType.None:
                viewChess.SetActive(false);
                break;

            case ChessData.ChessType.A:
                viewA.SetActive(true);
                break;

            case ChessData.ChessType.B:
                viewB.SetActive(true);
                break;

            case ChessData.ChessType.C:
                viewC.SetActive(true);
                break;

            case ChessData.ChessType.D:
                viewD.SetActive(true);
                break;

            case ChessData.ChessType.E:
                viewE.SetActive(true);
                break;
        }
    }

    public void SetChessState(bool isCurrentTarget, bool isClickableGoal, bool isPushFrom, bool isPushTo)
    {
        SetCurrentTarget(isCurrentTarget);
        SetClickableGoal(isClickableGoal);
        SetPushFrom(isPushFrom);
        SetPushTo(isPushTo);
    }

    private void SetCurrentTarget(bool b)
    {
        _isCurrentTarget = b;
        viewCurrentTarget.SetActive(_isCurrentTarget);
    }

    private void SetClickableGoal(bool b)
    {
        _isClickableGoal = b;
        viewCanClick.SetActive(_isClickableGoal);
    }

    private void SetPushFrom(bool b)
    {
        _isPushFrom = b;
        viewMultiPushFrom.SetActive(_isPushFrom);
    }

    private void SetPushTo(bool b)
    {
        _isPushTo = b;
        viewMultiPushTo.SetActive(_isPushTo);
    }

    public void ResetChessState()
    {
        SetChessState(false, false, false, false);
    }

    public void Spawn()
    {
        psSpawn.Play(true);
        SetChessType(GetNewChessType());
        ResetChessState();
        //TODO dotween
    }

    private ChessData.ChessType GetNewChessType()
    {
        var cfg = GameService.instance.gameConfig;

        var list = new List<ChessData.ChessType>();

        list.Add(ChessData.ChessType.A);
        list.Add(ChessData.ChessType.A);
        list.Add(ChessData.ChessType.B);
        list.Add(ChessData.ChessType.B);

        if (cfg.hasChessTypeC)
        {
            list.Add(ChessData.ChessType.C);
        }
        if (cfg.hasChessTypeD)
        {
            list.Add(ChessData.ChessType.D);
        }
        if (cfg.hasChessTypeE)
        {
            list.Add(ChessData.ChessType.E);
        }
        return list[Random.Range(0, list.Count)];
    }

    public void Erase()
    {
        psErase.Play(true);
        SetChessTypeNone();
    }

    public void OnClick()
    {
        Debug.Log("OnClick " + data.chessType);
        switch (GameSystem.instance.gameState)
        {
            case GameSystem.GameState.None:
                //nothing to dp
                break;
            case GameSystem.GameState.ChessClicked:
                TrySetToClickableGoal();
                break;
            case GameSystem.GameState.Moving:
                //nothing to dp
                break;
            case GameSystem.GameState.Wait:
                TrySetToCurrentTarget();
                break;
        }
    }

    private void TrySetToClickableGoal()
    {
        Debug.Log("TrySetToMoveTarget");
        switch (data.chessType)
        {
            case ChessData.ChessType.None:
                return;
        }

        SetToClickableGoal();
    }

    private void SetToClickableGoal()
    {
        Debug.Log("SetToClickableGoal");
        SetClickableGoal(true);
    }

    public void TrySetToCurrentTarget()
    {
        Debug.Log("TrySetToCurrentTarget");
        SetToCurrentTarget();
    }

    private void SetToCurrentTarget()
    {
        Debug.Log("SetCurrentTarget");
        SetCurrentTarget(true);
    }

}
