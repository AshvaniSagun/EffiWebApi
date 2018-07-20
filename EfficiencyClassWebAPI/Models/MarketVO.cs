using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficiencyClassWebAPI.Models
{
    public class MarketVO
    {
        public PseudoMarketVO Markets { get; set; }
        public PseudoVoMarket2MarketTypeParameterGroup MarketToMarketTypeParametergroups { get; set; }
        public List<PseudoVoVariable> Variables { get; set; }
        public List<PseudoVoFormula> Formulae { get; set; }
        public List<PseudoVoVariableType> VariableTypes { get; set; }
        public List<PseudoVoRange> RangeValues { get; set; }
        public List<PseudoVoWeightSegmentCo2> WeightSegmentCo2Values { get; set; }
        public List<PseudoVoFormulaDependencyDetail> FormulaDependencies { get; set; }
        public static explicit operator MarketVO(MarketVoTestFunc v)
        {
            MarketVO mvo = new MarketVO();
            mvo.Markets = new PseudoMarketVO();
            mvo.Markets.MarketID = v.MarketID;
            mvo.MarketToMarketTypeParametergroups = new PseudoVoMarket2MarketTypeParameterGroup();
            mvo.MarketToMarketTypeParametergroups.Mmid = v.MarketToMarketTypeParametergroups.Mmid;
            mvo.Markets.MYear = v.MYear;
            mvo.MarketToMarketTypeParametergroups.MarketTypeId = v.MarketToMarketTypeParametergroups.MarketTypeId;
            mvo.Variables = v.Variables;
            mvo.Formulae = v.Formulae;
            mvo.VariableTypes = v.VariableTypes;
            mvo.RangeValues = v.RangeValues;
            mvo.WeightSegmentCo2Values = v.WeightSegmentCo2Values;
            mvo.FormulaDependencies = v.FormulaDependencies;
            return mvo;
        }
    }

    public class PseudoMarketVO
    {

        public int MYear { get; set; }
        public int MarketID { get; set; }

    }
    public class PseudoVoRange
    {
        public long Id { get; set; }
        public long Mmid { get; set; }
        public decimal StartRange { get; set; }
        public decimal EndRange { get; set; }
        public string EcValue { get; set; }
        public Nullable<int> VariableTypeId { get; set; }
    }
    public class PseudoVoVariable
    {
        public long Id { get; set; }
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public long Mmid { get; set; }
        public int VariableTypeId { get; set; }
    }
    public class PseudoVoMarket2MarketTypeParameterGroup
    {
        public long Mmid { get; set; }

        public int MarketTypeId { get; set; }
    }
    public class PseudoVoFormula
    {
        public int Id { get; set; }
        public long Mmid { get; set; }
        public string FormulaDefinition { get; set; }
        public long VariableId { get; set; }
        public int FormulaPriority { get; set; }
    }
    public class PseudoVoFormulaDependencyDetail
    {
        public int FormulaId { get; set; }
        public long VariableId { get; set; }
        public Nullable<long> Id { get; set; }
    }
    public class PseudoVoWeightSegmentCo2
    {

        public long EwId { get; set; }
        public long Mmid { get; set; }
        public string Pno12 { get; set; }
        public string PWeight { get; set; }
        public string SegmentCo2 { get; set; }
    }
    public class ErrorVo
    {
        public StringBuilder ErrorMessage { get; set; }
        public StringBuilder ErrorCode { get; set; }
    }
    public class PseudoVoVariableType
    {
        public int Id { get; set; }
        public string VariableTypeName { get; set; }
    }

}
