using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfficiencyClassWebAPI.EF;

namespace EfficiencyClass.UnitTests.MockData
{
    public class MockUnitOfWork:IDisposable
    {
        private readonly MockDataContext context;

        public MockUnitOfWork()
        {
            context = new MockDataContext();
        }
        private MockRepository<ParameterGroupMaster> parameterGroupRepository;
        private MockRepository<Market> marketRepository;
        private MockRepository<MarketType> marketTypeRepository;
        private MockRepository<Market2MarketTypeParameterGroup> market2MarketTypeParameterGroupRepository;
        private MockRepository<Variable> variableRepository;
        private MockRepository<VariableType> variableTypeRepository;
        private MockRepository<Formula> formulaRepository;
        private MockRepository<FormulaDependencyDetail> formulaDependencyDetail;

        private MockRepository<EfficiencyClassRange> marketRangeRepository;
        private MockRepository<WeightSegmentCo2> weightSegmentCo2Repository;
        private MockRepository<Role> roleRepository;
        private MockRepository<UserDetail> userDetailRepository;
        private MockRepository<UserMarket> userMarketRepository;
        private MockRepository<UserRole> userRoleRepository;
        private MockRepository<StagedEfficiencyClassRange> stagedEfficiencyClassRangeRepository;
        private MockRepository<StagedFormula> stagedFormulaRepository;
        private MockRepository<StagedFormulaDependencyDetail> stagedFormulaDependencyRepository;
        private MockRepository<StagedVariable> stagedVariableRepository;
        private MockRepository<StagedWeightSegmentCo2> stagedWeightSegmentCo2Repository;



        public MockRepository<EfficiencyClassRange> EfficiencyClassRangeRepository
        {
            get
            {
                if (this.marketRangeRepository == null)
                {
                    this.marketRangeRepository = new MockRepository<EfficiencyClassRange>(context.EfficiencyClassRange);
                }
                return marketRangeRepository;
            }
        }
        public MockRepository<WeightSegmentCo2> WeightSegmentCo2Repository
        {
            get
            {
                if (this.weightSegmentCo2Repository == null)
                {
                    this.weightSegmentCo2Repository = new MockRepository<WeightSegmentCo2>(context.WeightSegmentCo2);
                }
                return weightSegmentCo2Repository;
            }
        }

        public MockRepository<Formula> FormulaRepository
        {
            get
            {
                if (this.formulaRepository == null)
                {
                    this.formulaRepository = new MockRepository<Formula>(context.Formula);
                }
                return formulaRepository;
            }
        }
        public MockRepository<FormulaDependencyDetail> FormulaDependencyDetail
        {
            get
            {
                if (this.formulaDependencyDetail == null)
                {
                    this.formulaDependencyDetail = new MockRepository<FormulaDependencyDetail>(context.FormulaDependencyDetail);
                }
                return formulaDependencyDetail;
            }
        }
        public MockRepository<Variable> VariableRepository
        {
            get
            {
                if (this.variableRepository == null)
                {
                    this.variableRepository = new MockRepository<Variable>(context.Variable);
                }
                return variableRepository;
            }
        }
        public MockRepository<VariableType> VariableTypeRepository
        {
            get
            {
                if (this.variableTypeRepository == null)
                {
                    this.variableTypeRepository = new MockRepository<VariableType>(context.VariableType);
                }
                return variableTypeRepository;
            }
        }
        public MockRepository<ParameterGroupMaster> ParameterGroupRepository
        {
            get
            {
                if (this.parameterGroupRepository == null)
                {
                    this.parameterGroupRepository = new MockRepository<ParameterGroupMaster>(context.ParameterGroupMaster);
                }
                return parameterGroupRepository;
            }
        }
        public MockRepository<Market> MarketRepository
        {
            get
            {
                if (this.marketRepository == null)
                {
                    this.marketRepository = new MockRepository<Market>(context.Market);
                }
                return marketRepository;
            }
        }
        public MockRepository<MarketType> MarketTypeRepository
        {
            get
            {
                if (this.marketTypeRepository == null)
                {
                    this.marketTypeRepository = new MockRepository<MarketType>(context.MarketType);
                }
                return marketTypeRepository;
            }
        }

        public MockRepository<Market2MarketTypeParameterGroup> Market2MarketTypeParameterGroupRepository
        {
            get
            {
                if (this.market2MarketTypeParameterGroupRepository == null)
                {
                    this.market2MarketTypeParameterGroupRepository = new MockRepository<Market2MarketTypeParameterGroup>(context.Market2MarketTypeParameterGroup);
                }
                return market2MarketTypeParameterGroupRepository;
            }
        }

        public MockRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new MockRepository<Role>(context.Role);
                }
                return roleRepository;
            }
        }

        public MockRepository<UserDetail> UserDetailRepository
        {
            get
            {
                if (this.userDetailRepository == null)
                {
                    this.userDetailRepository = new MockRepository<UserDetail>(context.UserDetail);
                }
                return userDetailRepository;
            }
        }

        public MockRepository<UserMarket> UserMarketRepository
        {
            get
            {
                if (this.userMarketRepository == null)
                {
                    this.userMarketRepository = new MockRepository<UserMarket>(context.UserMarket);
                }
                return userMarketRepository;
            }
        }

        public MockRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                {
                    this.userRoleRepository = new MockRepository<UserRole>(context.UserRole);
                }
                return userRoleRepository;
            }
        }

        public MockRepository<StagedEfficiencyClassRange> StagedEfficiencyClassRangeRepository
        {
            get
            {
                if (this.stagedEfficiencyClassRangeRepository == null)
                {
                    this.stagedEfficiencyClassRangeRepository = new MockRepository<StagedEfficiencyClassRange>(context.StagedEfficiencyClassRange);
                }
                return stagedEfficiencyClassRangeRepository;
            }
        }

        public MockRepository<StagedFormula> StagedFormulaRepository
        {
            get
            {
                if (this.stagedFormulaRepository == null)
                {
                    this.stagedFormulaRepository = new MockRepository<StagedFormula>(context.StagedFormula);
                }
                return stagedFormulaRepository;
            }
        }

        public MockRepository<StagedFormulaDependencyDetail> StagedFormulaDependencyRepository
        {
            get
            {
                if (this.stagedFormulaDependencyRepository == null)
                {
                    this.stagedFormulaDependencyRepository = new MockRepository<StagedFormulaDependencyDetail>(context.StagedFormulaDependencyDetail);
                }
                return stagedFormulaDependencyRepository;
            }
        }

        public MockRepository<StagedVariable> StagedVariableRepository
        {
            get
            {
                if (this.stagedVariableRepository == null)
                {
                    this.stagedVariableRepository = new MockRepository<StagedVariable>(context.StagedVariable);
                }
                return stagedVariableRepository;
            }
        }

        public MockRepository<StagedWeightSegmentCo2> StagedWeightSegmentCo2Repository
        {
            get
            {
                if (this.stagedWeightSegmentCo2Repository == null)
                {
                    this.stagedWeightSegmentCo2Repository = new MockRepository<StagedWeightSegmentCo2>(context.StagedWeightSegmentCo2);
                }
                return stagedWeightSegmentCo2Repository;
            }
        }
        public void Save()
        {

        }
        protected virtual void Dispose(bool disposing)
        {
            
        }
        public void Dispose()
        {
           
        }
    }
}
