namespace AdventOfCode.Year2015.Day12
{
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            var doc = JToken.Parse(input[0]);

            int numbers = SumNumbersInToken(doc);
            return numbers.ToString();
        }

        private static int SumNumbersInToken(JToken token)
        {
            if (token is JArray)
            {
                return token.Sum(SumNumbersInToken);
            }

            if (token is JObject)
            {
                // Make sure it's not red...
                if (token.Children().Any(x => x is JProperty prop && prop.Value.Type == JTokenType.String && prop.Value.Value<string>() == "red"))
                {
                    return 0;
                }

                return token.Children().Sum(SumNumbersInToken);
            }

            if (token is JProperty prop)
            {
                return SumNumbersInToken(prop.Value);
            }

            if (token.Type == JTokenType.Integer)
            {
                return token.Value<int>();
            }

            return 0;
        }
    }
}
