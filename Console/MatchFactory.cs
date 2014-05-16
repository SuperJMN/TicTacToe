using Model;

namespace Console
{
    public abstract class MatchFactory
    {
        public abstract Match CreateMatch(MatchConfiguration configuration);
    }
}