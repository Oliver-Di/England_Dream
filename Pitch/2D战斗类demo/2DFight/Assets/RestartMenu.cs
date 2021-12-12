using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject bloodOverlay;

    public void RestartGame()
    {
        //重置状态
        player.GetComponent<PlayerGetHit>().hp = player.GetComponent<PlayerGetHit>().maxHp;
        GameManager.instance.RefreshHp();
        player.GetComponent<Animator>().Play("Player_idle");
        player.GetComponent<Animator>().SetBool("running", false);
        player.GetComponent<Animator>().SetBool("rise", false);
        player.GetComponent<Animator>().SetBool("drop", false);
        player.layer = LayerMask.NameToLayer("Player");
        player.GetComponent<PlayerGetHit>().StopCoroutine("ContinuousBloodLoss");
        //恢复控制
        GameManager.instance.gameMode = GameManager.GameMode.Normal;
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
