
public class GoalScoredEventArgs : IEventArgs
{
    private Player _score;
    public Player Score
    {
        get
        {
            return _score;
        }
    }

    public GoalScoredEventArgs(Player scored)
    {
        _score = scored;
    }
}
