namespace AdventOfCode.Year2022.Day22
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Id}: [{CubeGridRow}, {CubeGridColumn}]")]
    public class CubeFaceDescriptor
    {
        public static int FaceSize { get; set; }

        public int Id { get; set; }

        public int CubeGridRow { get; set; }

        public int CubeGridColumn { get; set; }

        public (int Start, int End) MapGridXBounds { get; set; }

        public (int Start, int End) MapGridYBounds { get; set; }

        public CubeFaceDescriptor[] ConnectedFaces { get; } = new CubeFaceDescriptor[4];

        public int[] FaceTraversalDirectionChanges { get; } = new int[4];

        public ((int X, int Y) Location, CubeFaceDescriptor LocationCubeFace, int Direction) GetNextLocationFrom((int X, int Y) location, int direction)
        {
            // First ensure that the given location is within this cube face.
            if (location.X < this.MapGridXBounds.Start
                || location.X > this.MapGridXBounds.End
                || location.Y < this.MapGridYBounds.Start
                || location.Y > this.MapGridYBounds.End)
            {
                throw new Exception();
            }

            (int X, int Y) proposedDestination = (location.X + CubeMap.DirectionOffsets[direction].dX, location.Y + CubeMap.DirectionOffsets[direction].dY);
            CubeFaceDescriptor proposedCube = this;
            int proposedDirection = direction;

            // Handle wrapping.
            if (proposedDestination.X < this.MapGridXBounds.Start)
            {
                proposedCube = this.ConnectedFaces[2];
                proposedDirection = CubeMap.ChangeDirection(direction, this.FaceTraversalDirectionChanges[2]);
            }
            else if (proposedDestination.X > this.MapGridXBounds.End)
            {
                proposedCube = this.ConnectedFaces[0];
                proposedDirection = CubeMap.ChangeDirection(direction, this.FaceTraversalDirectionChanges[0]);
            }
            else if (proposedDestination.Y < this.MapGridYBounds.Start)
            {
                proposedCube = this.ConnectedFaces[3];
                proposedDirection = CubeMap.ChangeDirection(direction, this.FaceTraversalDirectionChanges[3]);
            }
            else if (proposedDestination.Y > this.MapGridYBounds.End)
            {
                proposedCube = this.ConnectedFaces[1];
                proposedDirection = CubeMap.ChangeDirection(direction, this.FaceTraversalDirectionChanges[1]);
            }

            if (proposedCube != this)
            {
                int originalXOffsetFromCubeStart = location.X - this.MapGridXBounds.Start;
                int originalYOffsetFromCubeStart = location.Y - this.MapGridYBounds.Start;

                // We are moving to a new cube face. This means the location will need to be adjusted accordingly.
                switch (direction, proposedDirection)
                {
                    case (0, 0):
                        proposedDestination = (proposedCube.MapGridXBounds.Start, proposedCube.MapGridYBounds.Start + originalYOffsetFromCubeStart);
                        break;
                    case (0, 1):
                        proposedDestination = (proposedCube.MapGridXBounds.End - originalYOffsetFromCubeStart, proposedCube.MapGridYBounds.Start);
                        break;
                    case (0, 2):
                        proposedDestination = (proposedCube.MapGridXBounds.End, proposedCube.MapGridYBounds.End - originalYOffsetFromCubeStart);
                        break;
                    case (0, 3):
                        proposedDestination = (proposedCube.MapGridXBounds.Start + originalYOffsetFromCubeStart, proposedCube.MapGridYBounds.End);
                        break;
                    case (1, 0):
                        proposedDestination = (proposedCube.MapGridXBounds.Start, proposedCube.MapGridYBounds.End - originalXOffsetFromCubeStart);
                        break;
                    case (1, 1):
                        proposedDestination = (proposedCube.MapGridXBounds.Start + originalXOffsetFromCubeStart, proposedCube.MapGridYBounds.Start);
                        break;
                    case (1, 2):
                        proposedDestination = (proposedCube.MapGridXBounds.End, proposedCube.MapGridYBounds.Start + originalXOffsetFromCubeStart);
                        break;
                    case (1, 3):
                        proposedDestination = (proposedCube.MapGridXBounds.End - originalXOffsetFromCubeStart, proposedCube.MapGridYBounds.End);
                        break;
                    case (2, 0):
                        proposedDestination = (proposedCube.MapGridXBounds.Start, proposedCube.MapGridYBounds.End - originalYOffsetFromCubeStart);
                        break;
                    case (2, 1):
                        proposedDestination = (proposedCube.MapGridXBounds.Start + originalYOffsetFromCubeStart, proposedCube.MapGridYBounds.Start);
                        break;
                    case (2, 2):
                        proposedDestination = (proposedCube.MapGridXBounds.End, proposedCube.MapGridYBounds.Start + originalYOffsetFromCubeStart);
                        break;
                    case (2, 3):
                        proposedDestination = (proposedCube.MapGridXBounds.End - originalYOffsetFromCubeStart, proposedCube.MapGridYBounds.End);
                        break;
                    case (3, 0):
                        proposedDestination = (proposedCube.MapGridXBounds.Start, proposedCube.MapGridYBounds.Start + originalXOffsetFromCubeStart);
                        break;
                    case (3, 1):
                        proposedDestination = (proposedCube.MapGridXBounds.End - originalXOffsetFromCubeStart, proposedCube.MapGridYBounds.Start);
                        break;
                    case (3, 2):
                        proposedDestination = (proposedCube.MapGridXBounds.End, proposedCube.MapGridYBounds.End - originalXOffsetFromCubeStart);
                        break;
                    case (3, 3):
                        proposedDestination = (proposedCube.MapGridXBounds.Start + originalXOffsetFromCubeStart, proposedCube.MapGridYBounds.End);
                        break;
                    default:
                        throw new Exception();
                }
            }

            return (proposedDestination, proposedCube, proposedDirection);
        }
    }
}
