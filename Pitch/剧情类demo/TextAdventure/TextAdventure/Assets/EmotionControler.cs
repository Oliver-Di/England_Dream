using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionControler : MonoBehaviour
{
    public GameObject emotionKey;

    void Update()
    {
        if (gameObject.activeSelf == true && emotionKey.activeSelf == true) 
            emotionKey.SetActive(false);
    }
}
