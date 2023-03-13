using System;
namespace inCommodities.test.logic.Models
{
    public class WindTurbineResponseModel
    {
        public string turbineName { get; set; } = String.Empty;
        public uint expectedProduction { get; set; }

        public static WindTurbineResponseModel WindTurbineResponseModelFromWindTurbineModel(WindTurbine turbine)
        {
            WindTurbineResponseModel model = new WindTurbineResponseModel();
            model.turbineName = turbine.getTurbineName();
            model.expectedProduction = turbine.getExpectedProduction();
            return model;
        }
    }
}


