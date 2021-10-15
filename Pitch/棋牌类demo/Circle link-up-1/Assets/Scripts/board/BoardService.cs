using UnityEngine;
using System.Collections.Generic;

public class BoardService : MonoBehaviour
{
    public static BoardService instance;

    public Transform roundCenter;
    public float radius;

    public ChessBehaviour center;

    public List<ChessBehaviour> ring1;//small 6
    public List<ChessBehaviour> ring2;//middle 12
    public List<ChessBehaviour> ring3;//big 18

    public List<ChessBehaviour> axis1;
    public List<ChessBehaviour> axis2;
    public List<ChessBehaviour> axis3;

    public List<ChessBehaviour> spawnArea;
    public List<ChessBehaviour> allArea;

    public bool stepAxis;
    public bool stepRing;
    private int _testAxis = 0;
    private int _testRing = 0;

    private void Update()
    {
        if (stepAxis)
        {
            stepAxis = false;
            axis1[_testAxis].gameObject.SetActive(false);
            axis2[_testAxis].gameObject.SetActive(false);
            axis3[_testAxis].gameObject.SetActive(false);
            _testAxis++;
        }
        if (stepRing)
        {
            stepRing = false;
            ring1[_testRing].gameObject.SetActive(false);
            ring2[_testRing].gameObject.SetActive(false);
            ring3[_testRing].gameObject.SetActive(false);
            _testRing++;
        }
    }

    int ringComparer(ChessBehaviour c1, ChessBehaviour c2)
    {
        var x1 = c1.transform.position.x;
        var x2 = c2.transform.position.x;
        var z1 = c1.transform.position.z + 0.5f;
        var z2 = c2.transform.position.z + 0.5f;
        if (Mathf.Approximately(x1, 0))
            x1 = 0;
        if (Mathf.Approximately(x2, 0))
            x2 = 0;
        if (Mathf.Approximately(z1, 0))
            z1 = 0;
        if (Mathf.Approximately(z2, 0))
            z2 = 0;
        if (Mathf.Atan2(z1, x1) < Mathf.Atan2(z2, x2))
        {
            return 1;
        }
        if (Mathf.Atan2(z1, x1) > Mathf.Atan2(z2, x2))
        {
            return -1;
        }
        return 0;
    }

    int axisComparer(ChessBehaviour c1, ChessBehaviour c2)
    {
        var x1 = c1.transform.position.x;
        var x2 = c2.transform.position.x;
        var z1 = c1.transform.position.z;
        var z2 = c2.transform.position.z;
        if (x1 - z1 > x2 - z2)
        {
            return 1;
        }
        if (x1 - z1 < x2 - z2)
        {
            return -1;
        }
        return 0;
    }

    private void Awake()
    {
        instance = this;
    }

    public void InitChessLists()
    {
        allArea = new List<ChessBehaviour>();
        allArea.Add(center);
        allArea.InsertRange(0, ring1);
        allArea.InsertRange(0, ring2);
        allArea.InsertRange(0, ring3);

        spawnArea = new List<ChessBehaviour>();
        spawnArea.InsertRange(0, allArea);

        axis1 = new List<ChessBehaviour>();
        axis2 = new List<ChessBehaviour>();
        axis3 = new List<ChessBehaviour>();

        foreach (var c in allArea)
        {
            var radian = Mathf.Atan2(c.transform.position.x, c.transform.position.z) / Mathf.PI;
            // Debug.Log(c.gameObject.name + " " + radian);
            if (radian < 0)
            {
                radian += 1;
            }
            if (Mathf.Approximately(radian, 0.5f) || c == center)
            {
                axis1.Add(c);
            }
            if (Mathf.Approximately(radian, 5f / 6f) || c == center)
            {
                axis2.Add(c);
            }
            if (Mathf.Approximately(radian, 1f / 6f) || c == center)
            {
                axis3.Add(c);
            }
        }

        //re-order
        // ring1.Sort(ringComparer);
        // ring2.Sort(ringComparer);
        // ring3.Sort(ringComparer);
        axis1.Sort(axisComparer);
        axis2.Sort(axisComparer);
        axis3.Sort(axisComparer);
    }

    public ChessBehaviour GetChessRing1(bool clockwise, int originIndex)
    {
        return GetChess(clockwise, originIndex, ring1, true);
    }

    public ChessBehaviour GetChessRing2(bool clockwise, int originIndex)
    {
        return GetChess(clockwise, originIndex, ring2, true);
    }

    public ChessBehaviour GetChessRing3(bool clockwise, int originIndex)
    {
        return GetChess(clockwise, originIndex, ring3, true);
    }

    public ChessBehaviour GetChessAxis1(bool forwardDirection, int originIndex)
    {
        return GetChess(forwardDirection, originIndex, axis1, false);
    }

    public ChessBehaviour GetChessAxis2(bool forwardDirection, int originIndex)
    {
        return GetChess(forwardDirection, originIndex, axis2, false);
    }

    public ChessBehaviour GetChessAxis3(bool forwardDirection, int originIndex)
    {
        return GetChess(forwardDirection, originIndex, axis3, false);
    }

    public int IndexOfRing1(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring1);
    }

    public int IndexOfRing2(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring2);
    }

    public int IndexOfRing3(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring3);
    }

    public int IndexOfAxis1(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis1);
    }

    public int IndexOfAxis2(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis2);
    }

    public int IndexOfAxis3(ChessBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis3);
    }

    private int IndexOfRingOrAxis(ChessBehaviour cd, List<ChessBehaviour> list)
    {
        return list.IndexOf(cd);
    }

    private ChessBehaviour GetChess(bool dir, int originIndex, List<ChessBehaviour> list, bool canLoop)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int tpIndex = dir ? (originIndex + i) : (originIndex - i);
            if (tpIndex >= list.Count)
            {
                if (!canLoop)
                {
                    return null;//no chess match
                }

                tpIndex -= list.Count;//ring can loop once
            }
            else if (tpIndex < 0)
            {
                if (!canLoop)
                {
                    return null;//no chess match
                }

                tpIndex += list.Count;//ring can loop once
            }

            var tpChess = list[tpIndex];
            if (tpChess.data.chessType != ChessData.ChessType.None)
            {
                return tpChess;
            }
        }

        return null;
    }

    public void SetCurrentChess(ChessBehaviour target)
    {
        GameSystem.instance.gameState = GameSystem.GameState.ChessClicked;

        foreach (var chess in allArea)
        {
            chess.ResetChessState();
        }

        target.SetCurrentTarget(true);
        DisplayGoals(target);
    }

    public void DisplayGoals(ChessBehaviour target)
    {
        var indexAxis1 = IndexOfAxis1(target);
        var indexAxis2 = IndexOfAxis2(target);
        var indexAxis3 = IndexOfAxis3(target);

        var indexRing1 = IndexOfRing1(target);
        var indexRing2 = IndexOfRing2(target);
        var indexRing3 = IndexOfRing3(target);

        if (indexAxis1 > -1)
        {
            foreach (var chess in axis1)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }

        if (indexAxis2 > -1)
        {
            foreach (var chess in axis2)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }

        if (indexAxis3 > -1)
        {
            foreach (var chess in axis3)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }

        if (indexRing1 > -1)
        {
            foreach (var chess in ring1)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }

        if (indexRing2 > -1)
        {
            foreach (var chess in ring2)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }

        if (indexRing3 > -1)
        {
            foreach (var chess in ring3)
            {
                if (chess != target && chess.data.chessType == ChessData.ChessType.None)
                    chess.SetClickableGoal(true);
            }
        }
    }

    public void SetClickableGoal(ChessBehaviour target)
    {
        GameSystem.instance.gameState = GameSystem.GameState.Wait;

        foreach (var chess in allArea)
        {
            chess.ResetChessState();
        }

        //  target.SetCurrentTarget(true);
    }
}
