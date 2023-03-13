using System;
namespace inCommodities.test.logic.Models
{
    public class WindStation
    {
        private uint maxCapacity = 0;
        private IList<WindTurbine> turbines = new List<WindTurbine>();

        public WindStation() { }

        public void addTurbinesToStation(List<WindTurbine> turbines)
        {
            foreach (WindTurbine turbine in turbines)
            {
                maxCapacity += turbine.getMaxCapacity();
                this.turbines.Add(turbine);
            }

            // Ensure list of turbines are ordered by productionCost (it is specified that this is first prio), then by maxCap (This is not specified, so it is just a choice I have made) and the by name (To ensure it is deterministic).
            // This could also be done more effectively, by not resorting everything everytime some new turbines are added. Since the number of turbines are very low, I decide to keep the code simple.
            this.turbines = turbines.OrderBy(t => t.getProductionCost()).ThenBy(t => t.getMaxCapacity()).ThenBy(t => t.getTurbineName()).ToList();
        }

        public IList<WindTurbine> getTurbines()
        {
            return turbines;
        }

        public uint getMaxCapacity()
        {
            return maxCapacity;
        }

        public void updateStatusOfTurbines()
        {

        }
    }
}


