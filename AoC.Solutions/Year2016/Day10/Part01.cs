namespace AoC.Solutions.Year2016.Day10
{
    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var day10 = new Day10Common();
            day10.ProcessInput(input);

            bool doneSome = true;

            while (doneSome)
            {
                doneSome = false;

                foreach (Bot bot in day10.Bots.Values)
                {
                    if (bot.HasValues(17, 61))
                    {
                        return bot.Number.ToString();
                    }

                    if (bot.CanExecute())
                    {
                        bot.Execute();
                        doneSome = true;
                    }
                }
            }

            return string.Empty;
        }
    }
}
