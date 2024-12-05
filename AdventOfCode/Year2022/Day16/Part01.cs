namespace AdventOfCode.Year2022.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AdventOfCode;

    public class Part01 : ISolution
    {
        public string Solve(string[] input)
        {
            Dictionary<string, Valve> valves = [];
            int countOfValvesWorthOpening = 0;
            foreach (string line in input)
            {
                int flowRateEnd = line.IndexOf(';');

                var valve = new Valve
                {
                    Name = line[6..8].ToString(),
                    FlowRate = int.Parse(line[23..flowRateEnd]),
                    Destinations = line[(flowRateEnd + 24) ..].ToString().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries),
                };

                if (valve.FlowRate > 0)
                {
                    ++countOfValvesWorthOpening;
                }

                valves.Add(valve.Name, valve);
            }

            // this.OutputMermaid(valves);

            // BFS with a priority queue. No point tracking visited steps as we need to be able to backtrack.
            PriorityQueue<PathState, int> states = new();

            var startingState = new PathState
            {
                Location = "AA",
                LocationInPreviousMinute = "AA",
                TimeRemaining = 30,
                CurrentPressureReleaseRate = 0,
                OpenValves = [],
            };

            states.Enqueue(startingState, 0);

            int maximumPressureReleaseSeen = 0;

            while (states.TryDequeue(out PathState current, out _))
            {
                if (current.TimeRemaining == 0)
                {
                    maximumPressureReleaseSeen = Math.Max(maximumPressureReleaseSeen, current.TotalPressureReleased);
                    //return current.TotalPressureReleased.ToString();
                }

                int maximumPossibleTotalPressureRelease = current.MaximumPossibleTotalPressureRelease(valves);

                // If the maximum possible pressure release is less than the max we've seen
                // so far, cut this branch off.
                if (maximumPossibleTotalPressureRelease <= maximumPressureReleaseSeen)
                {
                    continue;
                }

                // If all the valves are open, just wait
                if (current.OpenValves.Count == countOfValvesWorthOpening)
                {
                    current.ToFinalState().EnqueueIn(states, valves);
                    continue;
                }

                // Add new states for valve opening
                Valve valve = valves[current.Location];

                // Don't open valves whose flow rate is 0.
                if (!current.CurrentValveIsOpen && valve.FlowRate != 0)
                {
                    current.WithOpenedValve(valve.Name, valve.FlowRate).EnqueueIn(states, valves);
                }

                // Add new states for move
                // TODO: Avoid immediate backtracking without opening a valve
                foreach (string move in valve.Destinations)
                {
                    if (current.LocationInPreviousMinute != move)
                    {
                        current.WithMove(move).EnqueueIn(states, valves);
                    }
                }
            }

            return maximumPressureReleaseSeen.ToString();
        }

        private void OutputMermaid(List<Valve> valves)
        {
            Console.WriteLine("graph TD");
            foreach (Valve valve in valves)
            {
                Console.Write("    ");
                Console.Write(valve.Name);
                Console.Write("[");
                Console.Write(valve.Name);
                Console.Write(" - flow rate ");
                Console.Write(valve.FlowRate);
                Console.WriteLine("]");

                foreach (string target in valve.Destinations)
                {
                    Console.Write("    ");
                    Console.Write(valve.Name);
                    Console.Write(" --> ");
                    Console.WriteLine(target);
                }
            }
        }

        public struct PathState
        {
            public string Location { get; set; }

            public string LocationInPreviousMinute { get; set; }

            public int TimeRemaining { get; set; }

            public int CurrentPressureReleaseRate { get; set; }

            public int TotalPressureReleased { get; set; }

            public List<string> OpenValves { get; set; }

            public readonly bool CurrentValveIsOpen => this.OpenValves.Contains(this.Location);

            public int MaximumPossibleTotalPressureRelease(Dictionary<string, Valve> allValves)
            {
                // The priority should be the maximum possible pressure release based on the
                // current state - what could we release if all remaining valves were opened.
                // This is the key to the solution because as we home in on the end state, the
                // predicted max will become more accurate meaning that the first time we
                // actually hit an end state we should be at the one which will give us the
                // best result.
                int potentialFinalPressureRelease = this.TotalPressureReleased + (this.TimeRemaining * this.CurrentPressureReleaseRate);

                if (this.TimeRemaining > 1)
                {
                    foreach (KeyValuePair<string, Valve> valve in allValves)
                    {
                        if (valve.Value.FlowRate != 0 && !this.OpenValves.Contains(valve.Key))
                        {
                            if (this.Location == valve.Key)
                            {
                                // We're next to it, so it could start flowing in 1 minute
                                potentialFinalPressureRelease += (this.TimeRemaining - 1) * valve.Value.FlowRate;
                            }
                            else if (this.TimeRemaining > 2 && allValves[this.Location].Destinations.Contains(valve.Key))
                            {
                                // It's next to us so it could start flowing in 2 minutes.
                                potentialFinalPressureRelease += (this.TimeRemaining - 2) * valve.Value.FlowRate;
                            }
                            else if (this.TimeRemaining > 4)
                            {
                                // It's further away; the soonest we could open it is 4 minutes from now.
                                potentialFinalPressureRelease += (this.TimeRemaining - 4) * valve.Value.FlowRate;
                            }
                        }
                    }
                }

                Debug.Assert(potentialFinalPressureRelease >= 0);

                return potentialFinalPressureRelease;
            }

            public PathState ToFinalState()
            {
                return new PathState
                {
                    Location = this.Location,
                    LocationInPreviousMinute = this.Location,
                    TimeRemaining = 0,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    TotalPressureReleased = this.TotalPressureReleased + (this.CurrentPressureReleaseRate * this.TimeRemaining),
                    OpenValves = new List<string>(this.OpenValves),
                };
            }

            public PathState WithOpenedValve(string valveName, int valvePressureReleaseRate)
            {
                var newState = new PathState
                {
                    Location = this.Location,
                    LocationInPreviousMinute = this.Location,
                    TimeRemaining = this.TimeRemaining - 1,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate + valvePressureReleaseRate,
                    TotalPressureReleased = this.TotalPressureReleased + this.CurrentPressureReleaseRate,
                    OpenValves = new List<string>(this.OpenValves),
                };

                newState.OpenValves.Add(valveName);

                return newState;
            }

            public PathState WithMove(string destination)
            {
                return new PathState
                {
                    Location = destination,
                    LocationInPreviousMinute = this.Location,
                    TimeRemaining = this.TimeRemaining - 1,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    TotalPressureReleased = this.TotalPressureReleased + this.CurrentPressureReleaseRate,
                    OpenValves = new List<string>(this.OpenValves),
                };
            }

            public void EnqueueIn(PriorityQueue<PathState, int> queue, Dictionary<string, Valve> allValves)
            {
                queue.Enqueue(this, int.MaxValue - this.MaximumPossibleTotalPressureRelease(allValves));
            }
        }

        public struct Valve
        {
            public string Name { get; set; }

            public int FlowRate { get; set; }

            public string[] Destinations { get; set; }
        }
    }
}
