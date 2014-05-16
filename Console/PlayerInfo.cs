using Model;

namespace Console
{
    public class PlayerInfo
    {
        public PlayerInfo(string name, PlayerType playerType)
        {
            Name = name;
            PlayerType = playerType;            
        }

        public string Name { get; set; }
        public PlayerType PlayerType { get; set; }                
    }
}