using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIcon : MonoBehaviour
{
    public static BuffIcon instance;
    public GameObject[] icons;

    private GameObject icon;
    private float time;

    private void Awake()
    {
        //单例
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void StartBuff(int num,float time)
    {
        switch (num)
        {
            case 0:
                icon = icons[0];
                break;
            case 1:
                icon = icons[1];
                break;
            case 2:
                icon = icons[2];
                break;
            case 3:
                icon = icons[3];
                break;
            case 4:
                icon = icons[4];
                break;
        }

        this.time = time;
        if (icon.activeSelf == true)
            StopCoroutine("BuffIconReveal");
        StartCoroutine("BuffIconReveal");
    }

    IEnumerator BuffIconReveal()
    {
        icon.SetActive(true);
        yield return new WaitForSeconds(time);
        icon.SetActive(false);
    }
}
