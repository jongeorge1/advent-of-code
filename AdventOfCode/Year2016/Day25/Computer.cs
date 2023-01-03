namespace AdventOfCode.Year2016.Day25
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Computer
    {
        private int location = 0;

        private string[][] program;

        private Dictionary<string, Action<string[], bool>> instructionMap;
        
        private bool verbose;

        public Computer(string program, bool verbose = false)
        {
            this.program = program.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            this.verbose = verbose;
            this.instructionMap = new ()
            {
                { "cpy", this.Cpy },
                { "inc", this.Inc },
                { "dec", this.Dec },
                { "jnz", this.Jnz },
                { "tgl", this.Tgl },
                { "out", this.Out },
            };
        }

        public void Reset()
        {
            this.location = 0;
            this.ToggledLocations.Clear();
        }

        public Dictionary<string, int> Registers { get; } = new ()
        {
            { "a", 0 },
            { "b", 0 },
            { "c", 0 },
            { "d", 0 },
        };

        public List<int> ToggledLocations { get; } = new ();

        public Func<int, bool> Output { get; set; }

        public void Execute()
        {
            while (this.location < this.program.Length)
            {
                this.instructionMap[this.program[this.location][0]](this.program[this.location], false);
            }
        }

        private void Tgl(string[] instruction, bool bypassToggleCheck = false)
        {
            if (!bypassToggleCheck && this.ToggledLocations.Contains(this.location))
            {
                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Toggled, treating as INC");
                }

                this.Inc(instruction, true);

            }
            else
            {
                int target = this.location + this.GetValueOrRegister(instruction[1]);
                if (this.ToggledLocations.Contains(target))
                {
                    this.ToggledLocations.Remove(target);
                    if (this.verbose)
                    {
                        Console.WriteLine($"{this.location}: Untoggling {target}");
                    }
                }
                else
                {
                    this.ToggledLocations.Add(target);
                    if (this.verbose)
                    {
                        Console.WriteLine($"{this.location}: Toggling {target}");
                    }
                }

                ++this.location;
            }
        }

        private void Out(string[] instruction, bool bypassToggleCheck = false)
        {
            var outputValue = this.GetValueOrRegister(instruction[1]);

            if (this.verbose)
            {
                Console.WriteLine($"{this.location}: Outputting {outputValue}");
            }

            if (this.Output(outputValue))
            {
                ++this.location;
            }
            else
            {
                this.location = int.MaxValue;
            }
        }

        private void Cpy(string[] instruction, bool bypassToggleCheck = false)
        {
            if (!bypassToggleCheck && this.ToggledLocations.Contains(this.location))
            {
                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Toggled, treating as JNZ");
                }
                
                this.Jnz(instruction, true);
            }
            else
            {
                string targetRegister = instruction[2];

                // The target register might be invalid...
                if (this.Registers.ContainsKey(targetRegister))
                {
                    this.Registers[targetRegister] = this.GetValueOrRegister(instruction[1]);
                    if (this.verbose)
                    {
                        Console.WriteLine($"{this.location}: Copying {this.Registers[targetRegister]} to {targetRegister}");
                    }
                }

                ++this.location;
            }
        }

        private void Inc(string[] instruction, bool bypassToggleCheck = false)
        {
            if (!bypassToggleCheck && this.ToggledLocations.Contains(this.location))
            {
                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Toggled, treating as DEC");
                }

                this.Dec(instruction, true);
            }
            else
            {
                ++this.Registers[instruction[1]];
                ++this.location;

                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Incrementing register {instruction[1]} to {this.Registers[instruction[1]]}");
                }
            }
        }

        private void Dec(string[] instruction, bool bypassToggleCheck = false)
        {
            if (!bypassToggleCheck && this.ToggledLocations.Contains(this.location))
            {
                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Toggled, treating as INC");
                }

                this.Inc(instruction, true);
            }
            else
            {
                --this.Registers[instruction[1]];
                ++this.location;

                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Decrementing register {instruction[1]} to {this.Registers[instruction[1]]}");
                }
            }
        }

        private void Jnz(string[] instruction, bool bypassToggleCheck = false)
        {
            if (!bypassToggleCheck && this.ToggledLocations.Contains(this.location))
            {
                if (this.verbose)
                {
                    Console.WriteLine($"{this.location}: Toggled, treating as CPY");
                }

                this.Cpy(instruction, true);
            }
            else
            {
                int condition = this.GetValueOrRegister(instruction[1]);
                int offset = this.GetValueOrRegister(instruction[2]);

                if (condition != 0)
                {
                    if (this.verbose)
                    {
                        Console.WriteLine($"{this.location}: Jumping by {offset}");
                    }

                    this.location += offset;
                }
                else
                {
                    if (this.verbose)
                    {
                        Console.WriteLine($"{this.location}: Ignoring jump");
                    }

                    ++this.location;
                }
            }
        }

        private int GetValueOrRegister(string instruction)
        {
            if (!int.TryParse(instruction, out int result))
            {
                result = this.Registers[instruction];
            }

            return result;
        }
    }
}
