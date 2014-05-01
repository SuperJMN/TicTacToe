namespace Console
{
    internal class PlayerInfo
    {
        public PlayerInfo(string name, PlayerType playerType, char piece)
        {
            Name = name;
            PlayerType = playerType;
            Piece = piece;
        }

        public string Name { get; set; }
        public PlayerType PlayerType { get; set; }        
        public char Piece { get; set; }
    }
}