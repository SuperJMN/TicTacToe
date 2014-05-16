namespace Model
{
    public abstract class MatchFactory
    {
        public abstract Match CreateMatch(MatchConfiguration configuration);
    }
}