namespace AdventOfCode.Year2024.Day14;

public readonly record struct Robot
{
    public Robot(string input)
    {
        string[] components = input.Split(['=', ',', ' ']);
        this.StartPosition = (int.Parse(components[1]), int.Parse(components[2]));
        this.Velocity = (int.Parse(components[4]), int.Parse(components[5]));
    }

    public (int X, int Y) StartPosition { get; }

    public (int DX, int DY) Velocity { get; }

    public (int X, int Y) GetPosition(int moves, int width, int height)
    {
        int positionX = this.StartPosition.X + (moves * this.Velocity.DX);
        int positionY = this.StartPosition.Y + (moves * this.Velocity.DY);

        positionX = positionX % width;
        if (positionX < 0)
        {
            positionX += width;
        }

        positionY = positionY % height;
        if (positionY < 0)
        {
            positionY += height;
        }

        return (positionX, positionY);
    }
}
