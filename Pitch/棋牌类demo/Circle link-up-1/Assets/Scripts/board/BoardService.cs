﻿using UnityEngine;
using System.Collections.Generic;

public class BoardService : MonoBehaviour
{
    public static BoardService instance;

    public Transform roundCenter;
    public float radius;

    public ChessBehaviour prefabChess;

    public SlotBehaviour center;

    public List<SlotBehaviour> ring1;//small 6
    public List<SlotBehaviour> ring2;//middle 12
    public List<SlotBehaviour> ring3;//big 18

    public List<SlotBehaviour> axis1;
    public List<SlotBehaviour> axis2;
    public List<SlotBehaviour> axis3;

    public List<SlotBehaviour> spawnArea;
    public List<SlotBehaviour> allArea;

    public bool stepAxis;
    public bool stepRing;
    private int _testAxis = 0;
    private int _testRing = 0;

    private SlotBehaviour _current;

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

    int ringComparer(SlotBehaviour c1, SlotBehaviour c2)
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

    int axisComparer(SlotBehaviour c1, SlotBehaviour c2)
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

    public void InitSlots()
    {
        allArea = new List<SlotBehaviour>();
        allArea.Add(center);
        allArea.InsertRange(0, ring1);
        allArea.InsertRange(0, ring2);
        allArea.InsertRange(0, ring3);

        spawnArea = new List<SlotBehaviour>();
        spawnArea.InsertRange(0, allArea);

        axis1 = new List<SlotBehaviour>();
        axis2 = new List<SlotBehaviour>();
        axis3 = new List<SlotBehaviour>();

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

    public SlotBehaviour GetChessRing1(bool clockwise, int originIndex)
    {
        return GetSlot(clockwise, originIndex, ring1, true);
    }

    public SlotBehaviour GetChessRing2(bool clockwise, int originIndex)
    {
        return GetSlot(clockwise, originIndex, ring2, true);
    }

    public SlotBehaviour GetChessRing3(bool clockwise, int originIndex)
    {
        return GetSlot(clockwise, originIndex, ring3, true);
    }

    public SlotBehaviour GetChessAxis1(bool forwardDirection, int originIndex)
    {
        return GetSlot(forwardDirection, originIndex, axis1, false);
    }

    public SlotBehaviour GetChessAxis2(bool forwardDirection, int originIndex)
    {
        return GetSlot(forwardDirection, originIndex, axis2, false);
    }

    public SlotBehaviour GetChessAxis3(bool forwardDirection, int originIndex)
    {
        return GetSlot(forwardDirection, originIndex, axis3, false);
    }

    private int IndexOfRingOrAxis(SlotBehaviour cd, List<SlotBehaviour> list)
    {
        return list.IndexOf(cd);
    }

    private SlotBehaviour GetSlot(bool dir, int originIndex, List<SlotBehaviour> list, bool canLoop)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int tpIndex = dir ? (originIndex + i) : (originIndex - i);
            if (tpIndex >= list.Count)
            {
                if (!canLoop)
                    return null;

                tpIndex -= list.Count;//ring can loop once
            }
            else if (tpIndex < 0)
            {
                if (!canLoop)
                    return null;

                tpIndex += list.Count;//ring can loop once
            }

            var tpSlot = list[tpIndex];
            if (tpSlot.chess == null)
            {
                return tpSlot;
            }
        }

        return null;
    }

    public void SetCurrentSlot(SlotBehaviour target)
    {
        GameSystem.instance.gameState = GameSystem.GameState.ChessClicked;

        foreach (var slot in allArea)
        {
            slot.ResetState();
        }

        target.SetCurrentTarget(true);
        DisplayGoals(target);
        _current = target;
    }

    public void DisplayGoals(SlotBehaviour target)
    {
        var chess = target.chess;
        Debug.Log("DisplayGoals " + chess.data.chessType);
        bool cw = true;
        foreach (var slot in axis1)
        {
            if (canReachThoughRingOrAxis(target, slot, axis1, false, ref cw))
                slot.SetClickableGoal(true);
        }

        foreach (var slot in axis2)
        {
            if (canReachThoughRingOrAxis(target, slot, axis2, false, ref cw))
                slot.SetClickableGoal(true);
        }

        foreach (var slot in axis3)
        {
            if (canReachThoughRingOrAxis(target, slot, axis3, false, ref cw))
                slot.SetClickableGoal(true);
        }

        foreach (var slot in ring1)
        {
            if (canReachThoughRingOrAxis(target, slot, ring1, true, ref cw))
                slot.SetClickableGoal(true);
        }

        foreach (var slot in ring2)
        {
            if (canReachThoughRingOrAxis(target, slot, ring2, true, ref cw))
                slot.SetClickableGoal(true);
        }

        foreach (var slot in ring3)
        {
            if (canReachThoughRingOrAxis(target, slot, ring3, true, ref cw))
                slot.SetClickableGoal(true);
        }
    }

    private bool canReachThoughRingOrAxis(SlotBehaviour chess, SlotBehaviour goal, List<SlotBehaviour> list, bool canLoop, ref bool cw)
    {
        var indexChess = list.IndexOf(chess);
        var indexGoal = list.IndexOf(goal);
        if (indexChess > -1 && indexGoal > -1)
        {
            //Debug.Log("canReachThoughRingOrAxis " + list.Count);
            //Debug.Log("indexes chess-goal " + indexChess + "-" + indexGoal);
            //Debug.Log(indexChess != indexGoal);
            //Debug.Log(goal.chess == null);
            //Debug.Log(isConnected(list, indexChess, indexGoal, canLoop));
            return indexChess != indexGoal
                && goal.chess == null
                 && isConnected(list, indexChess, indexGoal, canLoop, ref cw);
        }

        return false;
    }

    public bool isConnected(List<SlotBehaviour> list, int indexA, int indexB, bool canLoop, ref bool cw)
    {
        bool connectedCw = true;
        bool connectedAcw = true;
        int deltaCw = 0;
        int deltaAcw = 0;
        for (int i = 1; i < list.Count; i++)
        {
            int tpIndex = indexA + i;
            deltaCw = i;
            if (tpIndex >= list.Count)
            {
                if (!canLoop)
                {
                    connectedCw = false;
                    break;
                }
                else
                {
                    tpIndex -= list.Count;//ring can loop once
                }
            }
            if (tpIndex == indexB || tpIndex == indexA)
            {
                break;
            }
            var tpSlot = list[tpIndex];
            if (tpSlot.chess != null)
            {
                //Debug.Log("tpIndex " + tpIndex + " type" + tpSlot.chess.data.chessType);
                connectedCw = false;
                break;
            }
        }

        for (int i = 1; i < list.Count; i++)
        {
            int tpIndex = indexA - i;
            deltaAcw = i;
            if (tpIndex < 0)
            {
                if (!canLoop)
                {
                    connectedAcw = false;
                    break;
                }
                else
                {
                    tpIndex += list.Count;//ring can loop once
                }
            }
            if (tpIndex == indexB || tpIndex == indexA)
            {
                break;
            }
            var tpSlot = list[tpIndex];
            if (tpSlot.chess != null)
            {
                //Debug.Log("tpIndex " + tpIndex + " type" + tpSlot.chess.data.chessType);
                connectedAcw = false;
                break;
            }
        }
        //Debug.Log("isConnected " + connectedCw + " - " + connectedAcw);
        if (connectedCw && connectedAcw)
        {
            if (deltaAcw > deltaCw)
            {
                cw = true;
            }
            else
            {
                cw = false;
            }
        }
        else
        {
            if (connectedCw)
            {
                cw = true;
            }
            else if (connectedAcw)
            {
                cw = false;
            }
        }
        return connectedCw || connectedAcw;
    }

    public void SetClickableGoalSlot(SlotBehaviour goal)
    {
        Debug.Log("SetClickableGoalSlot");
        Debug.Log("will move " + _current.gameObject.name + " to " + goal.gameObject.name);
        bool suc = false;
        bool cw = true;
        if (!suc && canReachThoughRingOrAxis(_current, goal, axis1, false, ref cw))
        {
            MoveChess(_current, goal, axis1, false, cw);
            suc = true;
        }
        if (!suc && canReachThoughRingOrAxis(_current, goal, axis2, false, ref cw))
        {
            MoveChess(_current, goal, axis2, false, cw);
            suc = true;
        }
        if (!suc && canReachThoughRingOrAxis(_current, goal, axis3, false, ref cw))
        {
            MoveChess(_current, goal, axis3, false, cw);
            suc = true;
        }
        if (!suc && canReachThoughRingOrAxis(_current, goal, ring1, true, ref cw))
        {
            MoveChess(_current, goal, ring1, true, cw);
            suc = true;
        }
        if (!suc && canReachThoughRingOrAxis(_current, goal, ring2, true, ref cw))
        {
            MoveChess(_current, goal, ring2, true, cw);
            suc = true;
        }
        if (!suc && canReachThoughRingOrAxis(_current, goal, ring3, true, ref cw))
        {
            MoveChess(_current, goal, ring3, true, cw);
            suc = true;
        }

        if (suc)
        {
            GameSystem.instance.gameState = GameSystem.GameState.Moving;
            foreach (var slot in allArea)
            {
                slot.ResetState();
            }
        }
        else
        {
            Debug.Log("can not move there");
        }

    }

    private void MoveChess(SlotBehaviour from, SlotBehaviour to, List<SlotBehaviour> list, bool isRing, bool cw)
    {
        var chess = from.chess;
        int indexFrom = list.IndexOf(from);
        int indexTo = list.IndexOf(to);
        int delta = Mathf.Abs(indexFrom - indexTo);
        var cfg = GameService.instance.gameConfig;
        if (!isRing)
        {
            var timeAxis = cfg.animationTime_base + cfg.animationTime_interval * delta;
            chess.move.MoveAlongAxis(from.transform.position, to.transform.position, timeAxis, () =>
            {
                MoveEnd(from, to);
            });
            return;
        }

        //isRing
        if (cw)
        {
            delta = indexTo - indexFrom;
            if (delta < 0)
            {
                delta += list.Count;
            }
        }
        else
        {
            delta = indexFrom - indexTo;
            if (delta < 0)
            {
                delta += list.Count;
            }
        }
        var timeRing = cfg.animationTime_base + cfg.animationTime_interval * delta;
        chess.move.MoveAlongRing(from.transform.position, to.transform.position, cw, timeRing, () =>
        {
            MoveEnd(from, to);
        });
    }

    private void MoveEnd(SlotBehaviour from, SlotBehaviour to)
    {
        GameSystem.instance.gameState = GameSystem.GameState.Wait;
        var chess = from.chess;
        to.ReceiveChess(chess);
        from.ReleaseChess();
    }
}
