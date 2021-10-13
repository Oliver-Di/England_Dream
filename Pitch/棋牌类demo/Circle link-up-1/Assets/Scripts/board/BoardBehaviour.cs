using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{
    public ParticleSystem psStartGame;

    public bool test;
    private bool _isMoving;
    private float _movingTimer;
    private float _movingTime;
    private int _startIndex;
    private int _endIndex;
    public ChessBehaviour heroChess;// ring 3

    public void OnGameStart()
    {
        psStartGame.Play(true);
        SoundService.instance.Play("game start");
    }

    void Update()
    {
        MoveChess();

        if (test)
        {
            test = false;
            StartMoveHero();
        }

        MoveHero();
    }

    void MoveChess()
    {

    }

    void MoveHero()
    {
        var cfg = GameService.instance.gameConfig;
        if (_isMoving)
        {
            _movingTimer -= Time.deltaTime;
            var timeRatio = _movingTimer / _movingTime;
            if (timeRatio <= 0)
            {
                timeRatio = 0;
                _isMoving = false;
            }
            var timeRatioScaled = cfg.ac.Evaluate(1 - timeRatio);
            Debug.Log("--" + timeRatio + " - " + timeRatioScaled);
            var delta = _endIndex - _startIndex;
            if (delta < 0)
            {
                delta += 18;
            }
            var r = (_startIndex + timeRatioScaled * delta * 20) * Mathf.Deg2Rad;

            var x = Mathf.Cos(r);
            var z = Mathf.Sin(r);
            heroChess.transform.position = new Vector3(x * BoardService.instance.radius * 3, 0, z * BoardService.instance.radius * 3);
            // var timeRatio = cfg.ac
        }
    }

    void StartMoveHero()
    {
        var cfg = GameService.instance.gameConfig;
        heroChess.SetChessType();
        var centerPos = BoardService.instance.roundCenter.position;
        var chessPos = heroChess.transform.position;

        var deltaPos = chessPos - centerPos;
        deltaPos.y = 0;
        var rotation = Quaternion.LookRotation(deltaPos, Vector3.up);
        Debug.Log(rotation.eulerAngles.y);
        int index = Mathf.FloorToInt(rotation.eulerAngles.y / 20f);
        int push = Random.Range(3, 7);

        _startIndex = index;
        _endIndex = _startIndex + push;
        if (_endIndex >= 18)
        {
            _endIndex -= 18;
        }

        _isMoving = true;

        _movingTimer = cfg.animationTime_base + cfg.animationTime_interval * push;
        _movingTime = _movingTimer;
    }
}