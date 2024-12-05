namespace AdventOfCode.Year2021.Day22
{
    using System.Collections.Generic;
    using System.Linq;

    public class Reactor
    {
        private List<Region> activeRegions = [];

        public void SwitchRegion(Region newRegion, bool on)
        {
            // Edge case: if the new region is "on" and is fully contained in any existing active region,
            // then we don't need to do anything.
            if (on && this.activeRegions.Any(region => region.Contains(newRegion)))
            {
                return;
            }

            var newActiveRegions = new List<Region>();

            // Work through the existing regions removing any areas that intersect with the new region.
            // This may result in an existing region being decomposed into multiple smaller regions to
            // allow the intersection to be removed.
            foreach (Region region in this.activeRegions)
            {
                newActiveRegions.AddRange(region.RemoveIntersection(newRegion));
            }

            // Now we know the new region doesn't overlap with any existing regions. If it's an "on" command, the
            // new region gets added to the list of active regions. If it's "off", we've already done all we need.
            if (on)
            {
                newActiveRegions.Add(newRegion);
            }

            this.activeRegions = newActiveRegions;
        }

        public long ActiveCubeCount() => this.activeRegions.Sum(x => x.Volume);
    }
}
