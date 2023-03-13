using System;
using inCommodities.test.logic.Exceptions;

namespace inCommodities.test.logic.Models
{
    public class ControlStation
    {
        private uint currentMarkedPrice = 0;
        private uint currentProductionTarget = 0;
        private WindStation windStation_A = new WindStation(); // If we get multiple wind stations we can use, this should be a list or a hashmap, depending on the amount, and what we should do with them


        public ControlStation()
        {
            List<WindTurbine> windTurbines = new List<WindTurbine> {
                new WindTurbine("A", 2, 15),
                new WindTurbine("B", 2, 5),
                new WindTurbine("C", 6, 5),
                new WindTurbine("D", 6, 5),
                new WindTurbine("E", 5, 3)
            };
            windStation_A.addTurbinesToStation(windTurbines);
        }

        public void setCurrentMarketPrice(uint marketPrice)
        {
            currentMarkedPrice = marketPrice;
            UpdateWindStationStatus();
        }

        public uint getCurrentMarkedPrice()
        {
            return currentMarkedPrice;
        }

        public uint updateCurrentProductionTarget(int delta)
        {
            if (currentProductionTarget + delta < 0)
            {
                throw new InvalidDeltaException($"The provided delta ({delta}) was not valid, since the updated production target would be below 0 ({currentProductionTarget + delta})");
            }
            else if (currentProductionTarget + delta > windStation_A.getMaxCapacity()) // If we get multiple windstations we can use, this check should be updated
            {
                throw new InvalidDeltaException($"The provided delta ({delta}) was not valid, since the updated production target ({currentProductionTarget + delta}) would be above the maximum production target for the wind station ({windStation_A.getMaxCapacity()})");
            }
            currentProductionTarget = (uint)(currentProductionTarget + delta);

            UpdateWindStationStatus();

            return currentProductionTarget;
        }

        public uint getCurrentProductionTarget()
        {
            return currentProductionTarget;
        }

        public IList<WindTurbine> getTurbineStatusForWindStationA()
        {
            return windStation_A.getTurbines();
        }

        // This method, I would probably write an unit test for, in a production environment
        private void UpdateWindStationStatus()
        {
            var turbines = windStation_A.getTurbines();
            uint currentProduction = 0;
            foreach (WindTurbine turbine in turbines)
            {
                if (turbine.getProductionCost() >= currentMarkedPrice)
                {
                    turbine.setExpectedProduction(0);
                    continue;
                }
                if (turbine.getMaxCapacity() + currentProduction > currentProductionTarget)
                {
                    turbine.setExpectedProduction(0);
                    continue;
                }
                turbine.setExpectedProduction(turbine.getMaxCapacity());
                currentProduction += turbine.getExpectedProduction();
            }
        }
    }
}


