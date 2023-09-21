using Bingo;

var game = new Game();
var players = new List<Player>();
for (int i = 0; i < 15; i++)
{
    players.Add(new Player(i,3,9,4));
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
        for (int j = 0; j < players[i].Boards.Count(); j++) 
        {
            if (players[i].Boards[j].Win)
            {
                Console.WriteLine($"Player {players[i].PlayerNumber} with board number {players[i].Boards[j].BoardNumber} win!");
                foreach(var card in players[i].Boards)
                {
                    card.WriteCard();
                }
                return true;
            }
        }
    }
    return false;
}
