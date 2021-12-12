using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    public GameObject walker;

    private bool popupDone;

    private void Update()
    {
        if (walker.activeSelf == false && !popupDone) 
        {
            GetComponent<BoxCollider2D>().enabled = false;
            popupDone = true;
            Popup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Popup()
    {
        SoundService.instance.Play("Player_jumpup");
        Time.timeScale = 0;

        var data = new ConfirmboxBehaviour.ConfirmBoxData();
        data.btnClose = false;
        data.btnBgClose = true;
        data.btnLeft = false;
        data.btnRight = false;
        data.title = "Throw Skill";
        data.content = "When you use execution to kill an enemy, the enemy will be dismembered and you can pick up the enemy's head and use 'E' to throw it." +
            "\n\nTap Anything To Continue";
        data.btnLeftTxt = "Sure";
        data.btnLeftAction = () =>
        {
            SoundService.instance.Play("Player_jumpup");
        };
        data.btnRightTxt = "jump";
        data.btnRightAction = () =>
        {
            SoundService.instance.Play("Player_jumpup");
        };
        ConfirmboxBehaviour.instance.Setup(data);
        ConfirmboxBehaviour.instance.Show();
    }
}
