using Bingo;

var game = new Game();
var players = new List<Player>();
for(int i = 0; i < 150; i++)
{
    players.Add(new Player(i));
    game.RegisterObserver(players[i]);
}
while(!IsGameEnd())
{
    game.Play();
}

bool IsGameEnd()
{
    for(int i = 0; i < players.Count(); i++)
    {
        if (players[i].Win)
        {
            Console.WriteLine($"Player with board number {players[i].BoardNumber} win");
            players[i].WriteCard();
            return true;
        }
    }
    return false;
}