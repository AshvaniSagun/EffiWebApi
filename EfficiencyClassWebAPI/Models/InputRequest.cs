using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficiencyClassWebAPI.Models
{
    public class InputRequest
    {   //weight and error code added

        [JsonProperty(PropertyName = "specificationMarket")]
        [Required(ErrorMessage = "0000-specificationMarket is required")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "1101-specificationMarket should be numeric fixed length should be 3 (ex- 300).")]
        public string SpecMarket { get; set; }
        [JsonProperty(PropertyName = "modelYear")]
        [Required(ErrorMessage = "0000-modelYear is required")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "1101-modelYear should be numeric and fixed length should be 4 (ex- 2016).")]
        public string ModelYear { get; set; }
        [JsonProperty(PropertyName = "pno12")]

        public string Pno12 { get; set; }
        [JsonProperty(PropertyName = "fuelType")]
        public string FuelType { get; set; }
        [JsonProperty(PropertyName = "co2")]
        // [Required(ErrorMessage = "0000-co2 is required.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "1101-Value is invalid or empty from the external request.")]
        public string Co2 { get; set; }
        [JsonProperty(PropertyName = "fuelEfficiency")]
        public string FuelEfficiency { get; set; }
        [JsonProperty(PropertyName = "electricalEnergyConsumption")]
        public string ElectricalEnergyConsumption { get; set; }
        [JsonProperty(PropertyName = "electricalRange")]
        public string ElectricalRange { get; set; }
        [JsonProperty(PropertyName = "weightParameters")]
        public InputWeight WeightParameters { get; set; }
        public static explicit operator InputRequest(MarketVoTestFunc v)
        {
            InputRequest inputParam = new InputRequest();
            inputParam.SpecMarket = v.SpecMarket;
            inputParam.ModelYear = v.ModelYear;
            inputParam.Pno12 = v.Pno12;
            inputParam.Co2 = v.Co2;
            inputParam.FuelEfficiency = v.FuelEfficiency;
            inputParam.ElectricalEnergyConsumption = v.ElectricalEnergyConsumption;
            inputParam.ElectricalRange = v.ElectricalRange;
            inputParam.FuelType = v.FuelType;
            inputParam.WeightParameters = v.WeightParameters;
            return inputParam;
        }
    }
    // Weights definition
    public class InputWeight
    {
        [JsonProperty(PropertyName = "actualMass")]
        public int? ActualMass { get; set; }
        [JsonProperty(PropertyName = "testMassInd")]
        public int? TestMassInd { get; set; }
        [JsonProperty(PropertyName = "massInRunningOrderTotal")]
        public int? MassInRunningOrderTotal { get; set; }
        [JsonProperty(PropertyName = "massOfOptionalEquipmentTotal")]
        public int? MassOfOptionalEquipmentTotal { get; set; }
        [JsonProperty(PropertyName = "nedc_ActualMass")]
        public int? Nedc_ActualMass { get; set; }
        [JsonProperty(PropertyName = "homologationCurbWeightTotal")]
        public int? HomologationCurbWeightTotal { get; set; }
    }

}
