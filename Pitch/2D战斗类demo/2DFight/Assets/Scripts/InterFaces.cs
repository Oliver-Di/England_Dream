using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterFaces
{
    void GetHitBack(float damage, Vector3 dir, float force);
    void GetVertigo(float damage);
    void GetExecute(float damage);
}
