namespace AdventOfCode.Year2021.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    public class BurrowState
    {
        private const int CorridorLength = 11;

        private const int RoomsCount = 4;

        // The value we'll store in the corridor array to indicate an empty corridor space.
        private const int EmptyCorridorPosition = -1;

        // The indexes in the corridor array for the room entrances
        private static readonly int[] RoomEntryPositions = new[] { 2, 4, 6, 8 };

        private int roomSlotsCount = 0;

        // The array we'll use to track what's in the corridor
        private int[] corridor = new int[CorridorLength];

        // The array we'll use to track what's in the room. In the second dimension, 0 will be the "bottom" space and 1 the "top".
        private int[,] rooms;

        public BurrowState(BurrowState baseState)
        {
            this.corridor = (int[])baseState.corridor.Clone();
            this.rooms = (int[,])baseState.rooms.Clone();
            this.roomSlotsCount = baseState.roomSlotsCount;
            this.EnergyConsumed = baseState.EnergyConsumed;
        }

        public BurrowState(string[] input, string[] additionalRoomSlots)
        {
            var roomInputs = input
                .Skip(2)
                .Take(2)
                .Select(x => x.Replace(" ", string.Empty).Replace("#", string.Empty))
                .ToList();

            for (int i = 0; i < CorridorLength; ++i)
            {
                this.corridor[i] = EmptyCorridorPosition;
            }

            roomInputs.InsertRange(1, additionalRoomSlots);
            roomInputs.Reverse();

            this.roomSlotsCount = roomInputs.Count;
            this.rooms = new int[RoomsCount, this.roomSlotsCount];

            for (int room = 0; room < 4; ++room)
            {
                for (int roomSlot = 0; roomSlot < this.roomSlotsCount; ++roomSlot)
                {
                    this.rooms[room, roomSlot] = roomInputs[roomSlot][room] - 'A';
                }
            }
        }

        // The total energy consumed to get to the current state.
        public long EnergyConsumed { get; private set; } = 0;

        public IEnumerable<BurrowState> PossibleMoves()
        {
            // First, we need to look at each entity currently in the corridor and work out if it can
            // move into its target room. For that to be possible, there needs to be a clear path
            // to the target room, and the target room should be unoccupied or contain a single
            // crab of the correct type for the room. We'll look through the corridor one position at
            // a time.
            for (int corridorPosition = 0; corridorPosition < CorridorLength; ++corridorPosition)
            {
                int entity = this.corridor[corridorPosition];

                if (entity == EmptyCorridorPosition)
                {
                    // There's nothing here.
                    continue;
                }

                // Now see if the target room for this entity is available to be moved into.
                if (!this.TryGetFirstAvailableSlotInRoom(entity, out int? firstAvailableSlotInTargetRoom))
                {
                    continue;
                }

                if (this.TryGetStepsFromCorridorPositionToTargetRoomSlot(corridorPosition, entity, firstAvailableSlotInTargetRoom.Value, out int? stepsToTarget))
                {
                    // We can make it. We need to create a new room state to represent this.
                    var newState = new BurrowState(this);

                    // The position in the corridor in which this entity started is now empty...
                    newState.corridor[corridorPosition] = EmptyCorridorPosition;
                    newState.rooms[entity, firstAvailableSlotInTargetRoom!.Value] = entity;
                    newState.EnergyConsumed += stepsToTarget.Value * (int)Math.Pow(10, entity);

                    Debug.Assert(newState.IsValid());

                    yield return newState;
                }
            }

            // Next, we need to look at each entity that's in a room and look at where it can go.
            for (int room = 0; room < RoomsCount; ++room)
            {
                if (!this.TryGetFirstSlotOfEntityToMoveFromRoom(room, out int? roomSlotOfEntityToMove))
                {
                    continue;
                }

                // Now we need to generate new states in all the possible positions it can move to.
                // How far can we move left without hitting an issue?
                bool moveLeft = true;
                do
                {
                    // Start the step count as if we've moved up to the exit from the room.
                    int totalStepsTaken = this.roomSlotsCount - roomSlotOfEntityToMove.Value - 1;

                    for (int i = RoomEntryPositions[room]; moveLeft ? i >= 0 : i < CorridorLength; i += moveLeft ? -1 : 1)
                    {
                        ++totalStepsTaken;
                        if (RoomEntryPositions.Contains(i))
                        {
                            // Can't stop in front of a room
                            continue;
                        }

                        if (this.corridor[i] != EmptyCorridorPosition)
                        {
                            // We've hit a space that's occupied - can't go any further.
                            break;
                        }

                        // We can move to here.
                        int entityBeingMoved = this.rooms[room, roomSlotOfEntityToMove.Value];

                        var newState = new BurrowState(this);
                        newState.corridor[i] = entityBeingMoved;
                        newState.rooms[room, roomSlotOfEntityToMove.Value] = EmptyCorridorPosition;
                        newState.EnergyConsumed += totalStepsTaken * (int)Math.Pow(10, entityBeingMoved);

                        Debug.Assert(newState.IsValid());
                        yield return newState;
                    }

                    if (moveLeft)
                    {
                        moveLeft = false;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
            }
        }

        public bool IsComplete()
        {
            for (int room = 0; room < RoomsCount; ++room)
            {
                for (int roomSlot = 0; roomSlot < this.roomSlotsCount; ++roomSlot)
                {
                    if (this.rooms[room, roomSlot] != room)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsValid()
        {
            for (int i = 0; i < RoomsCount; ++i)
            {
                int entitiesFound = this.corridor.Count(x => x == i);

                foreach (int current in this.rooms)
                {
                    if (current == i)
                    {
                        ++entitiesFound;
                    }
                }

                if (entitiesFound != this.roomSlotsCount)
                {
                    return false;
                }
            }

            return true;
        }

        public string GetMemento()
        {
            StringBuilder builder = new();
            foreach (int current in this.corridor)
            {
                builder.Append(current);
            }

            foreach (int current in this.rooms)
            {
                builder.Append(current);
            }

            return builder.ToString();
        }

        private bool TryGetFirstAvailableSlotInRoom(int room, [NotNullWhen(true)] out int? firstAvailableSlot)
        {
            for (int roomSlot = 0; roomSlot < this.roomSlotsCount; ++roomSlot)
            {
                if (this.rooms[room, roomSlot] != room && this.rooms[room, roomSlot] != EmptyCorridorPosition)
                {
                    firstAvailableSlot = null;
                    return false;
                }
                else if (this.rooms[room, roomSlot] == EmptyCorridorPosition)
                {
                    firstAvailableSlot = roomSlot;
                    return true;
                }
            }

            throw new Exception("Why are we here?");
        }

        private bool TryGetStepsFromCorridorPositionToTargetRoomSlot(int corridorPosition, int targetRoom, int targetRoomSlot, [NotNullWhen(true)] out int? stepsToTarget)
        {
            stepsToTarget = 0;
            int direction = corridorPosition < RoomEntryPositions[targetRoom] ? 1 : -1;
            int currentPosition = corridorPosition;

            while (true)
            {
                currentPosition += direction;
                if (this.corridor[currentPosition] != EmptyCorridorPosition)
                {
                    // We're done; there's something in the way.
                    stepsToTarget = null;
                    return false;
                }

                // We can make the move.
                ++stepsToTarget;

                if (currentPosition == RoomEntryPositions[targetRoom])
                {
                    // We've made it.
                    stepsToTarget += this.roomSlotsCount - targetRoomSlot;
                    return true;
                }
            }

            throw new Exception("Why are we here?");
        }

        private bool TryGetFirstSlotOfEntityToMoveFromRoom(int room, [NotNullWhen(true)] out int? roomSlotOfEntityToMove)
        {
            int entityInSlot = EmptyCorridorPosition;

            for (roomSlotOfEntityToMove = this.roomSlotsCount - 1; roomSlotOfEntityToMove >= 0; --roomSlotOfEntityToMove)
            {
                entityInSlot = this.rooms[room, roomSlotOfEntityToMove.Value];
                if (entityInSlot != EmptyCorridorPosition)
                {
                    break;
                }
            }

            if (entityInSlot == EmptyCorridorPosition)
            {
                // The room is empty. Can't move anything from here.
                return false;
            }

            if (entityInSlot != room)
            {
                // It doesn't belong in this room, so it can move.
                return true;
            }
            else if (roomSlotOfEntityToMove.Value == 0)
            {
                // It's in the lowest position and it's the right entity for the room. It has to stay here.
                return false;
            }
            else
            {
                // It's the right entity. However, we need to check what's behind it; if there's an example of the
                // wrong entity for the room behind it, it needs to move, otherwise it has to stay.
                for (int otherRoomSlot = roomSlotOfEntityToMove.Value - 1; otherRoomSlot >= 0; --otherRoomSlot)
                {
                    if (this.rooms[room, otherRoomSlot] != room)
                    {
                        // It needs to move.
                        return true;
                    }
                }
            }

            // If we've got this far, it should stay where it is.
            return false;
        }
    }
}
