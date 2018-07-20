using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EF= EfficiencyClassWebAPI.EF;
namespace EfficiencyClassWebAPI.Repository
{
    public class UnitofWork : IDisposable
    {
        private readonly DbContext context;

        public UnitofWork()
        {
            context = new EF.efficiencyclassdevEntities();
        }
        private Repository<EF.ParameterGroupMaster> parameterGroupRepository;
        private Repository<EF.Market> marketRepository;
        private Repository<EF.MarketType> marketTypeRepository;
        private Repository<EF.Market2MarketTypeParameterGroup> market2MarketTypeParameterGroupRepository;
        private Repository<EF.Variable> variableRepository;
        private Repository<EF.VariableType> variableTypeRepository;
        private Repository<EF.Formula> formulaRepository;
        private Repository<EF.FormulaDependencyDetail> formulaDependencyDetail;
       
        private Repository<EF.EfficiencyClassRange> marketRangeRepository;
        private Repository<EF.WeightSegmentCo2> weightSegmentCo2Repository;
        private Repository<EF.Role> roleRepository;
        private Repository<EF.UserDetail> userDetailRepository;
        private Repository<EF.UserMarket> userMarketRepository;
        private Repository<EF.UserRole> userRoleRepository;
        private Repository<EF.StagedEfficiencyClassRange> stagedEfficiencyClassRangeRepository;
        private Repository<EF.StagedFormula> stagedFormulaRepository;
        private Repository<EF.StagedFormulaDependencyDetail> stagedFormulaDependencyRepository;
        private Repository<EF.StagedVariable> stagedVariableRepository;
        private Repository<EF.StagedWeightSegmentCo2> stagedWeightSegmentCo2Repository;


        
        public virtual Repository<EF.EfficiencyClassRange> EfficiencyClassRangeRepository
        {
            get
            {
                if (this.marketRangeRepository == null)
                {
                    this.marketRangeRepository = new Repository<EF.EfficiencyClassRange>(context);
                }
                return marketRangeRepository;
            }
        }
        public virtual Repository<EF.WeightSegmentCo2> WeightSegmentCo2Repository
        {
            get
            {
                if (this.weightSegmentCo2Repository == null)
                {
                    this.weightSegmentCo2Repository = new Repository<EF.WeightSegmentCo2>(context);
                }
                return weightSegmentCo2Repository;
            }
        }
       
        public virtual Repository<EF.Formula> FormulaRepository
        {
            get
            {
                if (this.formulaRepository == null)
                {
                    this.formulaRepository = new Repository<EF.Formula>(context);
                }
                return formulaRepository;
            }
        }
        public virtual Repository<EF.FormulaDependencyDetail> FormulaDependencyDetail
        {
            get
            {
                if (this.formulaDependencyDetail == null)
                {
                    this.formulaDependencyDetail = new Repository<EF.FormulaDependencyDetail>(context);
                }
                return formulaDependencyDetail;
            }
        }
        public virtual Repository<EF.Variable> VariableRepository
        {
            get
            {
                if (this.variableRepository == null)
                {
                    this.variableRepository = new Repository<EF.Variable>(context);
                }
                return variableRepository;
            }
        }
        public virtual Repository<EF.VariableType> VariableTypeRepository
        {
            get
            {
                if (this.variableTypeRepository == null)
                {
                    this.variableTypeRepository = new Repository<EF.VariableType>(context);
                }
                return variableTypeRepository;
            }
        }
        public virtual Repository<EF.ParameterGroupMaster> ParameterGroupRepository
        {
            get
            {
                if (this.parameterGroupRepository == null)
                {
                    this.parameterGroupRepository = new Repository<EF.ParameterGroupMaster>(context);
                }
                return parameterGroupRepository;
            }
        }
        public virtual Repository<EF.Market> MarketRepository
        {
            get
            {
                if (this.marketRepository == null)
                {
                    this.marketRepository = new Repository<EF.Market>(context);
                }
                return marketRepository;
            }
        }
        public virtual Repository<EF.MarketType> MarketTypeRepository
        {
            get
            {
                if (this.marketTypeRepository == null)
                {
                    this.marketTypeRepository = new Repository<EF.MarketType>(context);
                }
                return marketTypeRepository;
            }
        }

        public virtual Repository<EF.Market2MarketTypeParameterGroup> Market2MarketTypeParameterGroupRepository
        {
            get
            {
                if (this.market2MarketTypeParameterGroupRepository == null)
                {
                    this.market2MarketTypeParameterGroupRepository = new Repository<EF.Market2MarketTypeParameterGroup>(context);
                }
                return market2MarketTypeParameterGroupRepository;
            }
        }

        public virtual Repository<EF.Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new Repository<EF.Role>(context);
                }
                return roleRepository;
            }
        }

        public virtual Repository<EF.UserDetail> UserDetailRepository
        {
            get
            {
                if (this.userDetailRepository == null)
                {
                    this.userDetailRepository = new Repository<EF.UserDetail>(context);
                }
                return userDetailRepository;
            }
        }

        public virtual Repository<EF.UserMarket> UserMarketRepository
        {
            get
            {
                if (this.userMarketRepository == null)
                {
                    this.userMarketRepository = new Repository<EF.UserMarket>(context);
                }
                return userMarketRepository;
            }
        }

        public virtual Repository<EF.UserRole> UserRoleRepository
        {
            get
            {
                if (this.userRoleRepository == null)
                {
                    this.userRoleRepository = new Repository<EF.UserRole>(context);
                }
                return userRoleRepository;
            }
        }

        public virtual Repository<EF.StagedEfficiencyClassRange> StagedEfficiencyClassRangeRepository
        {
            get
            {
                if (this.stagedEfficiencyClassRangeRepository == null)
                {
                    this.stagedEfficiencyClassRangeRepository = new Repository<EF.StagedEfficiencyClassRange>(context);
                }
                return stagedEfficiencyClassRangeRepository;
            }
        }

        public virtual Repository<EF.StagedFormula> StagedFormulaRepository
        {
            get
            {
                if (this.stagedFormulaRepository == null)
                {
                    this.stagedFormulaRepository = new Repository<EF.StagedFormula>(context);
                }
                return stagedFormulaRepository;
            }
        }

        public virtual Repository<EF.StagedFormulaDependencyDetail> StagedFormulaDependencyRepository
        {
            get
            {
                if (this.stagedFormulaDependencyRepository == null)
                {
                    this.stagedFormulaDependencyRepository = new Repository<EF.StagedFormulaDependencyDetail>(context);
                }
                return stagedFormulaDependencyRepository;
            }
        }

        public virtual Repository<EF.StagedVariable> StagedVariableRepository
        {
            get
            {
                if (this.stagedVariableRepository == null)
                {
                    this.stagedVariableRepository = new Repository<EF.StagedVariable>(context);
                }
                return stagedVariableRepository;
            }
        }

        public virtual Repository<EF.StagedWeightSegmentCo2> StagedWeightSegmentCo2Repository
        {
            get
            {
                if (this.stagedWeightSegmentCo2Repository == null)
                {
                    this.stagedWeightSegmentCo2Repository = new Repository<EF.StagedWeightSegmentCo2>(context);
                }
                return stagedWeightSegmentCo2Repository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if ((!this.disposed) && (disposing))
            {

                context.Dispose();

            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
    