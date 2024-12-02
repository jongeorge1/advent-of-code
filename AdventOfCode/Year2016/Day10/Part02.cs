namespace AdventOfCode.Year2016.Day10
{
    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var day10 = new Day10Common();
            day10.ProcessInput(input);

            bool doneSome = true;

            while (doneSome)
            {
                doneSome = false;

                foreach (Bot bot in day10.Bots.Values)
                {
                    if (bot.CanExecute())
                    {
                        bot.Execute();
                        doneSome = true;
                    }
                }
            }

            return (day10.GetOutput(0).Values[0] * day10.GetOutput(1).Values[0] * day10.GetOutput(2).Values[0]).ToString();
        }
    }
}
