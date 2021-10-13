using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    public float animationTime_base;
    public float animationTime_interval;

    public AnimationCurve ac;
}