using UnityEngine;
using System.Collections;
using DG.Tweening;

public class OptionBehaviour : MonoBehaviour
{
    public TextBehaviour tb;
    private Option _option;
    public RectTransform rect;
    public string defaultSoundName;
    public float delay;

    public void Awake()
    {
        _toPos = rect.anchoredPosition;
    }

    public void OnClick()
    {
        SoundService.instance.Play(defaultSoundName);
        if (_option == null)
            return;

        if (_option.itemId != "")
        {
            IconsBehaviour.instance.AddItem(_option.itemId);
        }

        switch (_option.vfx)
        {
            case Option.VFX.ShowRestartButton:
                GameSystem.instance.SetRestartButton(true);
                break;

            case Option.VFX.StartShowTimer:
                GlobalTimerBehaviour.instance.Init(_option.timerFeedbackValue);
                GlobalTimerBehaviour.instance.Show();
                break;

            case Option.VFX.RedVignette:

                break;

            case Option.VFX.Fireworks:

                break;
        }

        switch (_option.timerFeedback)
        {
            case Option.TimerFeedback.Push:
                GlobalTimerBehaviour.instance.Add(_option.timerFeedbackValue);
                break;

            case Option.TimerFeedback.Set:
                GlobalTimerBehaviour.instance.SetValue(_option.timerFeedbackValue);
                break;
        }

        GameSystem.instance.score += _option.score;
        GameSystem.instance.StartPlot(_option.targetPlot);
    }

    public void Setup(Option option)
    {
        _option = option;
        if (option == null)
        {
            Hide();
            return;
        }

        tb.SetColor(option.color);
        Show(option.text);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(string s)
    {
        gameObject.SetActive(true);
        tb.ShowText(s);
    }

    public Vector2 fromPos;
    private Vector2 _toPos;

    public void ShowAnimIn()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        rect.anchoredPosition = fromPos;
        rect.DOAnchorPos(_toPos, 0.5f).SetEase(Ease.OutCubic).SetDelay(delay).OnComplete(OnAnimInFinish);
    }

    void OnAnimInFinish()
    {
        Debug.Log("OnAnimInFinish");
    }
}
