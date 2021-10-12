using UnityEngine;
using System.Collections.Generic;

public class BoardService : MonoBehaviour
{
    public static BoardService instance;

    public Transform roundCenter;
    public float radius;

    public List<ChessBehaviour> ring1;//small 6
    public List<ChessBehaviour> ring2;//middle 12
    public List<ChessBehaviour> ring3;//big 18

    public List<ChessBehaviour> axis1;
    public List<ChessBehaviour> axis2;
    public List<ChessBehaviour> axis3;

    private List<ChessBehaviour> _spawnArea;
    private List<ChessBehaviour> _allArea;

    public ChessBehaviour center;

    private void Awake()
    {
        instance = this;
        InitChessLists();
    }

    public void InitChessLists()
    {
        _spawnArea = new List<ChessBehaviour>();
        _spawnArea.InsertRange(0, ring1);
        _spawnArea.InsertRange(0, ring2);
        _spawnArea.InsertRange(0, ring3);
        _spawnArea.Add(center);

        _allArea = new List<ChessBehaviour>();
        _allArea.InsertRange(0, ring1);
        _allArea.InsertRange(0, ring2);
        _allArea.InsertRange(0, ring3);
        _allArea.Add(center);
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

    public bool isBoardFull()
    {

        return false;
    }
}
