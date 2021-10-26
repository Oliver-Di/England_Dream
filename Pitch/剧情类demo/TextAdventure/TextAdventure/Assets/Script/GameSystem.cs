using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    public PlotBehaviour plotBehaviour;

    public static GameSystem instance;
    public GameObject restartButton;
    public int score = 0;

    void Start()
    {
        instance = this;

        RestartGame();
    }

    public void RestartGame()
    {
        IconsBehaviour.instance.Clear();
        GlobalTimerBehaviour.instance.Hide();
        StartPlot(ConfigService.instance.startPlot);
        SetRestartButton(false);
        score = 0;
    }

    public void StartPlot(Plot plot)
    {
        Debug.Log("StartPlot");
        plotBehaviour.Setup(plot);
    }

    public void SetRestartButton(bool show)
    {
        restartButton.SetActive(show);
    }
}
