namespace AdventOfCode.Year2021.Day21
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public record DiracDiceGameState
    {
        // In three rolls of a three sided dice there are the following possible combos
        // 3 - 1 1 1

        // 4 - 1 1 2
        // 4 - 1 2 1
        // 4 - 2 1 1

        // 5 - 1 1 3
        // 5 - 1 3 1
        // 5 - 3 1 1
        // 5 - 2 2 1
        // 5 - 2 1 2
        // 5 - 1 2 2

        // 6 - 1 2 3
        // 6 - 1 3 2
        // 6 - 2 1 3
        // 6 - 2 3 1
        // 6 - 3 1 2
        // 6 - 3 2 1
        // 6 - 2 2 2

        // 7 - 1 3 3
        // 7 - 2 2 3
        // 7 - 2 3 2
        // 7 - 3 1 3
        // 7 - 3 3 1
        // 7 - 3 2 2

        // 8 - 2 3 3
        // 8 - 3 2 3
        // 8 - 3 3 2

        // 9 - 3 3 3
        private static readonly (int Combination, int Universes)[] DiceCombinations = new[]
        {
            (3, 1),
            (4, 3),
            (5, 6),
            (6, 7),
            (7, 6),
            (8, 3),
            (9, 1),
        };

        public ImmutableList<DiracDicePlayerState> Players { get; init; } = ImmutableList<DiracDicePlayerState>.Empty;

        public int NextPlayer { get; init; } = 0;

        public long BranchesRepresented { get; init; } = 1;

        public IEnumerable<DiracDiceGameState> TakeTurn()
        {
            DiracDicePlayerState playerState = this.Players[this.NextPlayer];

            foreach ((int Combination, int Universes) roll in DiceCombinations)
            {
                int newPosition = (playerState.Position + roll.Combination) % 10;
                var newPlayerStates = this.Players.ToBuilder();
                newPlayerStates[this.NextPlayer] = new DiracDicePlayerState { Position = newPosition, Score = playerState.Score + newPosition + 1 };
                yield return new DiracDiceGameState { Players = newPlayerStates.ToImmutable(), NextPlayer = this.NextPlayer == 0 ? 1 : 0, BranchesRepresented = this.BranchesRepresented * roll.Universes };
            }
        }

        public bool IsGameOver()
        {
            return this.Players.Any(x => x.Score >= 21);
        }

        public int Victor()
        {
            return (this.NextPlayer + 1) % 2;
        }
    }
}
