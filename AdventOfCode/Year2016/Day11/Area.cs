namespace AdventOfCode.Year2016.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Area
    {
        private int elevator = 0;

        public Area(string input)
        {
            this.elevator = 0;
            this.Floors = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select((el, i) => new Floor(i + 1, el)).ToArray();
        }

        private Area(int elevator, IEnumerable<Floor> floors)
        {
            this.elevator = elevator;
            this.Floors = floors.ToArray();
        }

        public Floor[] Floors { get; }

        public bool IsStateValid => this.Floors.All(x => x.IsStateValid);

        public bool IsStateFinished => this.Floors.Take(this.Floors.Length - 1).All(x => x.IsEmpty);

        public Area Clone()
        {
            return new Area(this.elevator, this.Floors.Select(x => x.Clone()));
        }

        public string Serialize()
        {
            StringBuilder builder = new ();
            builder.Append(this.elevator);

            Dictionary<string, char> map = new ();
            char current = 'a';

            Func<string, char> getMappedValue = (string element) =>
            {
                if (!map.TryGetValue(element, out char mapped))
                {
                    mapped = current;
                    map[element] = mapped;
                    ++current;
                }

                return mapped;
            };

            foreach (Floor floor in this.Floors)
            {
                builder.Append('~');
                builder.Append(floor.Level);
                builder.Append('|');

                foreach (string generator in floor.Generators)
                {
                    builder.Append(getMappedValue(generator));
                    builder.Append(',');
                }

                builder.Append("|");

                foreach (string chip in floor.Chips)
                {
                    builder.Append(getMappedValue(chip));
                    builder.Append(',');
                }
            }

            return builder.ToString();
        }

        public Area[] GetPossibleMoves()
        {
            List<Area> moves = new ();

            if (this.elevator != 0)
            {
                moves.AddRange(this.GetPossibleMovesInDirection(-1));
            }

            if (this.elevator != this.Floors.Length - 1)
            {
                moves.AddRange(this.GetPossibleMovesInDirection(1));
            }

            return moves.ToArray();
        }

        public IEnumerable<Area> GetPossibleMovesInDirection(int direction)
        {
            int currentFloorIndex = this.elevator;
            int targetFloorIndex = this.elevator + direction;

            MovableCombination[] movableItems = this.Floors[currentFloorIndex].GetAllMovableCombinations();

            foreach (MovableCombination combo in movableItems)
            {
                Area newArea = this.Clone();
                Floor currentFloor = newArea.Floors[currentFloorIndex];
                Floor targetFloor = newArea.Floors[targetFloorIndex];
                newArea.elevator = targetFloorIndex;

                currentFloor.RemoveGenerators(combo.Generators);
                targetFloor.AddGenerators(combo.Generators);
                currentFloor.RemoveChips(combo.Chips);
                targetFloor.AddChips(combo.Chips);

                if (newArea.IsStateValid)
                {
                    yield return newArea;
                }
            }
        }
    }
}
