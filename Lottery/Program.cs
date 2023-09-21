using Bingo;

var game = new Game();
var players = new List<Player>();
for (int i = 0; i < 15; i++)
{
    players.Add(new Player(i, 3, 9, 4));
    game.RegisterObserver(players[i]);
}
while (!IsGameEnd())
{
    game.Play();
}

bool IsGameEnd()
{
    foreach(var player in players)
    {
        foreach(var board in player.Boards)
        {
            if (board.Win)
            {
                Console.WriteLine(
                    $"Player {player.PlayerNumber} with board number {board.BoardNumber} win!"
                );
                foreach (var card in player.Boards)
                {
                    card.WriteCard();
                }
                return true;
            }
        }
    }
    return false;
}
