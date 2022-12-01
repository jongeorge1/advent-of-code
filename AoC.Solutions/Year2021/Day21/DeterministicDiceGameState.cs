namespace AoC.Solutions.Year2021.Day21
{
    using System.Collections.Immutable;
    using System.Linq;

    public record DeterministicDiceGameState
    {
        public ImmutableList<DiracDicePlayerState> Players { get; init; }

        public int LastDiceRoll { get; init; } = 0;

        public int TotalDiceRolls { get; init; } = 0;

        public DeterministicDiceGameState TakeTurn(int player)
        {
            DiracDicePlayerState playerState = this.Players[player];

            // Get the next three rolls
            int newPosition = playerState.Position;
            int lastRoll = this.LastDiceRoll;
            int rollCount = this.TotalDiceRolls;

            for (int i = 0; i < 3; ++i)
            {
                lastRoll = lastRoll == 100 ? 1 : lastRoll + 1;
                ++rollCount;
                newPosition = (newPosition + lastRoll) % 10;
            }

            var newPlayerStates = this.Players.ToBuilder();
            newPlayerStates[player] = new DiracDicePlayerState { Position = newPosition, Score = playerState.Score + newPosition + 1 };

            return new DeterministicDiceGameState { Players = newPlayerStates.ToImmutable(), TotalDiceRolls = rollCount, LastDiceRoll = lastRoll };
        }

        public bool IsGameOver()
        {
            return this.Players.Any(x => x.Score >= 1000);
        }
    }
}
