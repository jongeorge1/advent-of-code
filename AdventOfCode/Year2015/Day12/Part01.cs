namespace AdventOfCode.Year2015.Day12
{
    using System.Linq;
    using Newtonsoft.Json.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var doc = JToken.Parse(input);

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
