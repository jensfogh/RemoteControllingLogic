using System;
namespace inCommodities.test.logic.Models
{
    public class WindTurbine
    {
        private string turbineName;
        private uint maxCapacity;
        private uint productionCost;
        private uint expectedProduction;

        public WindTurbine(string turbineName, uint maxCapacity, uint productionCost)
        {
            this.turbineName = turbineName;
            this.maxCapacity = maxCapacity;
            this.productionCost = productionCost;
        }

        public uint getMaxCapacity()
        {
            return maxCapacity;
        }

        public String getTurbineName()
        {
            return turbineName;
        }

        public uint getExpectedProduction()
        {
            return expectedProduction;
        }

        public uint getProductionCost()
        {
            return productionCost;
        }

        public void setExpectedProduction(uint expectedProduction)
        {
            this.expectedProduction = expectedProduction;
        }
    }
}


