namespace AoC.Solutions.Year2016.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Area
    {
        private int elevator = 0;
        private Floor[] floors;

        public Area(string input)
        {
            this.elevator = 0;
            this.floors = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select((el, i) => new Floor(i + 1, el)).ToArray();
        }

        private Area(int elevator, IEnumerable<Floor> floors)
        {
            this.elevator = elevator;
            this.floors = floors.ToArray();
        }

        public bool IsStateValid => this.floors.All(x => x.IsStateValid);

        public bool IsStateFinished => this.floors.Take(this.floors.Length - 1).All(x => x.IsEmpty);

        public Area Clone()
        {
            return new Area(this.elevator, this.floors.Select(x => x.Clone()));
        }

        public string Serialize()
        {
            return this.elevator + "~" + string.Join("~", this.floors.Select(el => el.Serialize()));
        }

        public Area[] GetPossibleMoves()
        {
            List<Area> moves = new ();

            if (this.elevator != 0)
            {
                moves.AddRange(this.GetPossibleMovesInDirection(-1));
            }

            if (this.elevator != this.floors.Length - 1)
            {
                moves.AddRange(this.GetPossibleMovesInDirection(1));
            }

            return moves.ToArray();
        }

        public IEnumerable<Area> GetPossibleMovesInDirection(int direction)
        {
            int currentFloorIndex = this.elevator;
            int targetFloorIndex = this.elevator + direction;

            MovableCombination[] movableItems = this.floors[currentFloorIndex].GetAllMovableCombinations();

            foreach (MovableCombination combo in movableItems)
            {
                Area newArea = this.Clone();
                Floor currentFloor = newArea.floors[currentFloorIndex];
                Floor targetFloor = newArea.floors[targetFloorIndex];
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
