namespace AdventOfCode.Year2018.Day15
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var currentState = State.Parse(input);
            int rounds = 0;

            ////Console.WriteLine(currentState.ToString());

            while (true)
            {
                bool roundCompleted = currentState.Round();

                if (roundCompleted)
                {
                    rounds++;
                }

                if (currentState.IsCombatEnded())
                {
                    break;
                }

                ////Console.WriteLine();
                ////Console.WriteLine($"Round {rounds}");
                ////Console.WriteLine(currentState.ToString());
            }

            ////Console.WriteLine();
            ////Console.WriteLine($"Combat ended during round {rounds + 1}");
            ////Console.WriteLine(currentState.ToString());

            return (rounds * currentState.TotalRemainingHitPoints()).ToString();
        }
    }
}
