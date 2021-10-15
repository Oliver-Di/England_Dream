[System.Serializable]
public class ChessData
{
    public ChessData(ChessType t = ChessType.None)
    {
        chessType = t;
    }
    public enum ChessType
    {
        None,
        A,
        B,
        C,
        D,
        E
    }

    public ChessType chessType;

    public ChessBehaviour behaviour
    {
        get
        {
            foreach (var instance in ChessBehaviour.instances)
            {
                if (instance.data == this)
                {
                    return instance;
                }
            }

            return null;
        }
    }
}
