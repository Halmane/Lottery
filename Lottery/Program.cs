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
    for (int i = 0; i < players.Count(); i++)
    {
        var player = players[i];
        for (int j = 0; j < player.Boards.Count(); j++)
        {
            if (player.Boards[j].Win)
            {
                Console.WriteLine(
                    $"Player {player.PlayerNumber} with board number {player.Boards[j].BoardNumber} win!"
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
