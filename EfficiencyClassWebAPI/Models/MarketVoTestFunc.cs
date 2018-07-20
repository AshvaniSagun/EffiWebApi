using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class MarketVoTestFunc
    {
        //Input Param
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
        //VO
        [JsonProperty(PropertyName = "MYear")]
        public int MYear { get; set; }
        [JsonProperty(PropertyName = "MarketID")]
        public int MarketID { get; set; }
        [JsonProperty(PropertyName = "MarketToMarketTypeParametergroups")]
        public PseudoVoMarket2MarketTypeParameterGroup MarketToMarketTypeParametergroups { get; set; }
        [JsonProperty(PropertyName = "Variables")]
        public List<PseudoVoVariable> Variables { get; set; }
        [JsonProperty(PropertyName = "Formulae")]
        public List<PseudoVoFormula> Formulae { get; set; }
        [JsonProperty(PropertyName = "VariableTypes")]
        public List<PseudoVoVariableType> VariableTypes { get; set; }
        [JsonProperty(PropertyName = "RangeValues")]
        public List<PseudoVoRange> RangeValues { get; set; }
        [JsonProperty(PropertyName = "WeightSegmentCo2Values")]
        public List<PseudoVoWeightSegmentCo2> WeightSegmentCo2Values { get; set; }
        [JsonProperty(PropertyName = "FormulaDependencies")]
        public List<PseudoVoFormulaDependencyDetail> FormulaDependencies { get; set; }
    }
}
