namespace AdventOfCode.Year2021.Day21
{
    using System.Collections.Immutable;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            DiracDicePlayerState[] playerStates = new[]
            {
                new DiracDicePlayerState { Position = int.Parse(input[0].Split(' ')[^1]) - 1, Score = 0 },
                new DiracDicePlayerState { Position = int.Parse(input[1].Split(' ')[^1]) - 1, Score = 0 },
            };

            var gameState = new DeterministicDiceGameState
            {
                LastDiceRoll = 0,
                TotalDiceRolls = 0,
                Players = playerStates.ToImmutableList(),
            };

            ////(int Position, int Score)[] playerStates = new[]
            ////{
            ////    (int.Parse(input[0].Split(' ')[^1]) - 1, 0),
            ////    (int.Parse(input[1].Split(' ')[^1]) - 1, 0),
            ////};

            ////(int Position, int Score)[] playerStates = new[]
            ////{
            ////    (int.Parse(input[0].Split(' ')[^1]) - 1, 0),
            ////    (int.Parse(input[1].Split(' ')[^1]) - 1, 0),
            ////};

            ////int lastRoll = 0;
            ////int rollCount = 0;

            ////Func<(int Position, int Score), (int Position, int Score)> takeTurn = state =>
            ////{
            ////    // Get the next three rolls
            ////    int newPosition = state.Position;
            ////    for (int i = 0; i < 3; ++i)
            ////    {
            ////        lastRoll = lastRoll == 100 ? 1 : lastRoll + 1;
            ////        ++rollCount;
            ////        newPosition = (newPosition + lastRoll) % 10;
            ////    }

            ////    return (newPosition, state.Score + newPosition + 1);
            ////};

            ////int turn = 0;
            ////do
            ////{
            ////    int index = turn % 2;
            ////    playerStates[index] = takeTurn(playerStates[index]);
            ////    ++turn;
            ////}
            ////while (playerStates.All(x => x.Score < 1000));

            int turn = 0;
            do
            {
                gameState = gameState.TakeTurn(turn % 2);
                ++turn;
            }
            while (!gameState.IsGameOver());

            ////return (rollCount * playerStates.Min(x => x.Score)).ToString();
            return (gameState.TotalDiceRolls * gameState.Players.Min(x => x.Score)).ToString();
        }
    }
}
