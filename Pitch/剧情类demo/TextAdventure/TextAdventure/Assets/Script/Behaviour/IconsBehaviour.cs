using UnityEngine;
using System.Collections.Generic;

public class IconsBehaviour : MonoBehaviour
{
    public static IconsBehaviour instance;

    public List<IconBehaviour> icons;

    private void Awake()
    {
        instance = this;
    }

    public void Clear()
    {
        foreach (var i in icons)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void AddItem(string s)
    {
        foreach (var i in icons)
        {
            if (i.id == s)
            {
                i.gameObject.SetActive(true);
            }
        }
    }

    public bool HasItem(string s)
    {
        foreach (var i in icons)
        {
            if (i.gameObject.activeSelf)
            {
                if (i.id == s)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
