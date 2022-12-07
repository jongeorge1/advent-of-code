namespace AdventOfCode.Year2021.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public record Region
    {
        public Region(long minX, long maxX, long minY, long maxY, long minZ, long maxZ)
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            this.MinZ = minZ;
            this.MaxZ = maxZ;
        }

        public long MinX { get; }

        public long MaxX { get; }

        public long MinY { get; }

        public long MaxY { get; }

        public long MinZ { get; }

        public long MaxZ { get; }

        public long Volume
        {
            get => (this.MaxX - this.MinX + 1) * (this.MaxY - this.MinY + 1) * (this.MaxZ - this.MinZ + 1);
        }

        public bool Contains(Region other)
        {
            return this.MinX <= other.MinX && this.MaxX >= other.MaxX
                && this.MinY <= other.MinY && this.MaxY >= other.MaxY
                && this.MinZ <= other.MinZ && this.MaxZ >= other.MaxZ;
        }

        public bool Intersects(Region other)
        {
            return !this.DoesNotIntersect(other);
        }

        public bool DoesNotIntersect(Region other)
        {
            return this.MinX > other.MaxX ||
                this.MaxX < other.MinX ||
                this.MinY > other.MaxY ||
                this.MaxY < other.MinY ||
                this.MinZ > other.MaxZ ||
                this.MaxZ < other.MinZ;
        }

        public IEnumerable<Region> RemoveIntersection(Region other)
        {
            if (other.Contains(this))
            {
                return Array.Empty<Region>();
            }

            if (other.DoesNotIntersect(this))
            {
                return new[] { this };
            }

            var results = new List<Region>();

            // Find the first intersecting plane and split into two.
            // Then rinse and repeat until we're done
            if (other.MinX > this.MinX && other.MinX <= this.MaxX)
            {
                // We need to split on other.MinX.
                results.AddRange(new Region(this.MinX, other.MinX - 1, this.MinY, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));

                results.AddRange(new Region(other.MinX, this.MaxX, this.MinY, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));
            }
            else if (other.MaxX >= this.MinX && other.MaxX < this.MaxX)
            {
                // We need to split on other.MaxX.
                results.AddRange(new Region(this.MinX, other.MaxX, this.MinY, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));

                results.AddRange(new Region(other.MaxX + 1, this.MaxX, this.MinY, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));
            }
            else if (other.MinY > this.MinY && other.MinY <= this.MaxY)
            {
                // We need to split on other.MinY.
                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, other.MinY - 1, this.MinZ, this.MaxZ).RemoveIntersection(other));

                results.AddRange(new Region(this.MinX, this.MaxX, other.MinY, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));
            }
            else if (other.MaxY >= this.MinY && other.MaxY < this.MaxY)
            {
                // We need to split on other.MaxY.
                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, other.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));

                results.AddRange(new Region(this.MinX, this.MaxX, other.MaxY + 1, this.MaxY, this.MinZ, this.MaxZ).RemoveIntersection(other));
            }
            else if (other.MinZ > this.MinZ && other.MinZ <= this.MaxZ)
            {
                // We need to split on other.MinZ.
                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, this.MaxY, this.MinZ, other.MinZ - 1).RemoveIntersection(other));

                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, this.MaxY, other.MinZ, this.MaxZ).RemoveIntersection(other));
            }
            else if (other.MaxZ >= this.MinZ && other.MaxZ < this.MaxZ)
            {
                // We need to split on other.MaxZ.
                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, this.MaxY, this.MinZ, other.MaxZ).RemoveIntersection(other));

                results.AddRange(new Region(this.MinX, this.MaxX, this.MinY, this.MaxY, other.MaxZ + 1, this.MaxZ).RemoveIntersection(other));
            }
            else
            {
                throw new Exception("Didn't expect to be here");
            }

            return results;
        }
    }
}
