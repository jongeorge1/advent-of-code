namespace AdventOfCode.Year2022.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using AdventOfCode;

    public class Part02 : ISolution
    {
        public string Solve(string[] input)
        {
            Dictionary<string, Valve> valves = new();
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

            PriorityQueue<PathState, int> states = new();
            HashSet<string> seenStates = new();

            var startingState = new PathState
            {
                PersonLocation = "AA",
                PersonLocationInPreviousMinute = "AA",
                ElephantLocation = "AA",
                ElephantLocationInPreviousMinute = "AA",
                TimeRemaining = 26,
                CurrentPressureReleaseRate = 0,
                OpenValves = new(),
            };

            states.Enqueue(startingState, 0);

            int maximumPressureReleaseSeen = 0;

            while (states.TryDequeue(out PathState current, out _))
            {
                // Have we seen this state before?
                string currentStateMemo = current.Memoize();
                if (seenStates.Contains(currentStateMemo))
                {
                    // We've been in this location before, with the same release rate, total released, time remaining, etc.
                    // This happens when we have made exactly the same moves, but reversing who does them. Cut this branch off.
                    continue;
                }

                seenStates.Add(currentStateMemo);

                if (current.TimeRemaining == 0)
                {
                    maximumPressureReleaseSeen = Math.Max(maximumPressureReleaseSeen, current.TotalPressureReleased);
                    return current.TotalPressureReleased.ToString();
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
                Valve valveAtPersonLocation = valves[current.PersonLocation];
                Valve valveAtElephantLocation = valves[current.ElephantLocation];

                // Now our possibilities get more confusing.
                // 1. We can both open (if we're in different places)
                // 2. We can open and the elephant can move
                // 3. The elephant can open and we can move
                // 4. We can both move.
                bool personCanOpenValve = !current.ValveAtPersonLocationIsOpen && valveAtPersonLocation.FlowRate != 0;
                bool elephantCanOpenValve = !current.ValveAtElephantLocationIsOpen && valveAtElephantLocation.FlowRate != 0;
                bool bothCanOpenValve = personCanOpenValve && elephantCanOpenValve && current.PersonLocation != current.ElephantLocation;

                if (bothCanOpenValve)
                {
                    current.WithOpenedValve(valveAtPersonLocation.Name, valveAtPersonLocation.FlowRate, true)
                        .WithOpenedValve(valveAtElephantLocation.Name, valveAtElephantLocation.FlowRate, false)
                        .Tick()
                        .EnqueueIn(states, valves);
                }

                // Now we need to look at all possible combos of moves
                foreach (string personMove in valveAtPersonLocation.Destinations)
                {
                    if (current.PersonLocationInPreviousMinute != personMove)
                    {
                        foreach (string elephantMove in valveAtElephantLocation.Destinations)
                        {
                            if (current.ElephantLocationInPreviousMinute != elephantMove)
                            {
                                current.WithPersonMove(personMove)
                                    .WithElephantMove(elephantMove)
                                    .Tick()
                                    .EnqueueIn(states, valves);
                            }
                        }
                    }
                }

                // Now all the possible options for the person staying still and the elephant moving
                if (personCanOpenValve)
                {
                    foreach (string elephantMove in valveAtElephantLocation.Destinations)
                    {
                        if (current.ElephantLocationInPreviousMinute != elephantMove)
                        {
                            current.WithOpenedValve(valveAtPersonLocation.Name, valveAtPersonLocation.FlowRate, true)
                                .WithElephantMove(elephantMove)
                                .Tick()
                                .EnqueueIn(states, valves);
                        }
                    }
                }

                // And for the elephant staying still and the person moving
                if (elephantCanOpenValve)
                {
                    foreach (string personMove in valveAtPersonLocation.Destinations)
                    {
                        if (current.PersonLocationInPreviousMinute != personMove)
                        {
                            current.WithPersonMove(personMove)
                                .WithOpenedValve(valveAtElephantLocation.Name, valveAtElephantLocation.FlowRate, false)
                                .Tick()
                                .EnqueueIn(states, valves);
                        }
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
            public string PersonLocation { get; set; }

            public string ElephantLocation { get; set; }

            public string PersonLocationInPreviousMinute { get; set; }

            public string ElephantLocationInPreviousMinute { get; set; }

            public int TimeRemaining { get; set; }

            public int CurrentPressureReleaseRate { get; set; }

            public int PressureReleaseRateAfterNextTick { get; set; }

            public int TotalPressureReleased { get; set; }

            public SortedSet<string> OpenValves { get; set; }

            public string Memoize()
            {
                StringBuilder memo = new();
                memo.Append(this.TimeRemaining);
                memo.Append(",");
                if (string.Compare(this.PersonLocation, this.ElephantLocation) < 0)
                {
                    memo.Append(this.PersonLocation);
                    memo.Append(",");
                    memo.Append(this.ElephantLocation);
                }
                else
                {
                    memo.Append(this.ElephantLocation);
                    memo.Append(",");
                    memo.Append(this.PersonLocation);
                }

                foreach (string current in this.OpenValves)
                {
                    memo.Append(current);
                    memo.Append(",");
                }

                memo.Append(this.TotalPressureReleased);
                memo.Append(",");
                memo.Append(this.CurrentPressureReleaseRate);

                return memo.ToString();
            }

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
                            if (this.PersonLocation == valve.Key || this.ElephantLocation == valve.Key)
                            {
                                // We're next to it, so it could start flowing in 1 minute
                                potentialFinalPressureRelease += (this.TimeRemaining - 1) * valve.Value.FlowRate;
                            }
                            else if (this.TimeRemaining > 2 && (allValves[this.PersonLocation].Destinations.Contains(valve.Key)
                                || allValves[this.ElephantLocation].Destinations.Contains(valve.Key)))
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

            public bool ValveAtPersonLocationIsOpen => this.OpenValves.Contains(this.PersonLocation);

            public bool ValveAtElephantLocationIsOpen => this.OpenValves.Contains(this.ElephantLocation);

            public PathState ToFinalState()
            {
                return new PathState
                {
                    PersonLocation = this.PersonLocation,
                    PersonLocationInPreviousMinute = this.PersonLocation,
                    ElephantLocation = this.ElephantLocation,
                    ElephantLocationInPreviousMinute = this.ElephantLocation,
                    TimeRemaining = 0,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    PressureReleaseRateAfterNextTick = this.PressureReleaseRateAfterNextTick,
                    TotalPressureReleased = this.TotalPressureReleased + (this.CurrentPressureReleaseRate * this.TimeRemaining),
                    OpenValves = new SortedSet<string>(this.OpenValves),
                };
            }

            public PathState Tick()
            {
                return new PathState
                {
                    PersonLocation = this.PersonLocation,
                    PersonLocationInPreviousMinute = this.PersonLocationInPreviousMinute,
                    ElephantLocation = this.ElephantLocation,
                    ElephantLocationInPreviousMinute = this.ElephantLocationInPreviousMinute,
                    TimeRemaining = this.TimeRemaining - 1,
                    CurrentPressureReleaseRate = this.PressureReleaseRateAfterNextTick,
                    PressureReleaseRateAfterNextTick = this.PressureReleaseRateAfterNextTick,
                    TotalPressureReleased = this.TotalPressureReleased + this.CurrentPressureReleaseRate,
                    OpenValves = new SortedSet<string>(this.OpenValves),
                };
            }

            public PathState WithOpenedValve(string valveName, int valvePressureReleaseRate, bool openedByPerson)
            {
                var newState = new PathState
                {
                    PersonLocation = this.PersonLocation,
                    PersonLocationInPreviousMinute = openedByPerson ? this.PersonLocation : this.PersonLocationInPreviousMinute,
                    ElephantLocation = this.ElephantLocation,
                    ElephantLocationInPreviousMinute = openedByPerson ? this.ElephantLocationInPreviousMinute : this.ElephantLocation,
                    TimeRemaining = this.TimeRemaining,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    PressureReleaseRateAfterNextTick = this.PressureReleaseRateAfterNextTick + valvePressureReleaseRate,
                    TotalPressureReleased = this.TotalPressureReleased,
                    OpenValves = new SortedSet<string>(this.OpenValves),
                };

                newState.OpenValves.Add(valveName);

                return newState;
            }

            public PathState WithPersonMove(string destination)
            {
                return new PathState
                {
                    PersonLocation = destination,
                    PersonLocationInPreviousMinute = this.PersonLocation,
                    ElephantLocation = this.ElephantLocation,
                    ElephantLocationInPreviousMinute = this.ElephantLocationInPreviousMinute,
                    TimeRemaining = this.TimeRemaining,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    PressureReleaseRateAfterNextTick = this.PressureReleaseRateAfterNextTick,
                    TotalPressureReleased = this.TotalPressureReleased,
                    OpenValves = new SortedSet<string>(this.OpenValves),
                };
            }

            public PathState WithElephantMove(string destination)
            {
                return new PathState
                {
                    PersonLocation = this.PersonLocation,
                    PersonLocationInPreviousMinute = this.PersonLocationInPreviousMinute,
                    ElephantLocation = destination,
                    ElephantLocationInPreviousMinute = this.ElephantLocation,
                    TimeRemaining = this.TimeRemaining,
                    CurrentPressureReleaseRate = this.CurrentPressureReleaseRate,
                    PressureReleaseRateAfterNextTick = this.PressureReleaseRateAfterNextTick,
                    TotalPressureReleased = this.TotalPressureReleased,
                    OpenValves = new SortedSet<string>(this.OpenValves),
                };
            }

            public void EnqueueIn(PriorityQueue<PathState, int> queue, Dictionary<string, Valve> allValves)
            {
                Debug.Assert(this.CurrentPressureReleaseRate == this.PressureReleaseRateAfterNextTick);
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
