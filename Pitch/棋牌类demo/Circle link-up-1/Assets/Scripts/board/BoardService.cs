using UnityEngine;
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

    public int IndexOfRing1(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring1);
    }

    public int IndexOfRing2(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring2);
    }

    public int IndexOfRing3(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, ring3);
    }

    public int IndexOfAxis1(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis1);
    }

    public int IndexOfAxis2(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis2);
    }

    public int IndexOfAxis3(SlotBehaviour cd)
    {
        return IndexOfRingOrAxis(cd, axis3);
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
    }

    public void DisplayGoals(SlotBehaviour target)
    {
        var indexAxis1 = IndexOfAxis1(target);
        var indexAxis2 = IndexOfAxis2(target);
        var indexAxis3 = IndexOfAxis3(target);

        var indexRing1 = IndexOfRing1(target);
        var indexRing2 = IndexOfRing2(target);
        var indexRing3 = IndexOfRing3(target);

        if (indexAxis1 > -1)
        {
            foreach (var slot in axis1)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }

        if (indexAxis2 > -1)
        {
            foreach (var slot in axis2)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }

        if (indexAxis3 > -1)
        {
            foreach (var slot in axis3)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }

        if (indexRing1 > -1)
        {
            foreach (var slot in ring1)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }

        if (indexRing2 > -1)
        {
            foreach (var slot in ring2)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }

        if (indexRing3 > -1)
        {
            foreach (var slot in ring3)
            {
                if (slot != target && slot.chess == null)
                    slot.SetClickableGoal(true);
            }
        }
    }

    public void SetClickableGoalSlot(SlotBehaviour target)
    {
        GameSystem.instance.gameState = GameSystem.GameState.Moving;

        foreach (var slot in allArea)
        {
            slot.ResetState();
        }
    }
}
