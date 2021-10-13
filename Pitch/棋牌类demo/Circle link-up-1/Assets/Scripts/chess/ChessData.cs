[System.Serializable]
public class ChessData
{
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
