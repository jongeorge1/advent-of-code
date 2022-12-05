namespace AdventOfCode.Year2015.Day15
{
    using System;

    public class Ingredient
    {
        public Ingredient(string input)
        {
            string[] components = input.Split(new[] { ' ', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            this.Name = components[0];
            this.Capacity = int.Parse(components[2]);
            this.Durability = int.Parse(components[4]);
            this.Flavor = int.Parse(components[6]);
            this.Texture = int.Parse(components[8]);
            this.Calories = int.Parse(components[10]);
        }

        public string Name { get; }

        public int Capacity { get; }

        public int Durability { get; }

        public int Flavor { get; }

        public int Texture { get; }

        public int Calories { get; }
    }
}
