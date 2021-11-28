using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMove.instance.transform.position = transform.position;
    }
}
