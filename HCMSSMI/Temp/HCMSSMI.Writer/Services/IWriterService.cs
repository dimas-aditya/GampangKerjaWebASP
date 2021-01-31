using HCMSSMI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMSSMI.Writer
{
    public interface IWriterService
    {
        #region Policy

        Task<Tuple<Policy, bool>> AddPolicy(List<Policy> policy, string clientKey = null, string apiKey = null);
        Task<Tuple<Policy, bool>> UpdatePolicy(int id, Policy policy, string clientKey = null, string apiKey = null);
        Task<bool> DeletePolicy(int id, string clientKey = null, string apiKey = null);

        #endregion

        #region Benefit

        Task<Tuple<BenefitMapping, bool>> AddBenefitMapping(List<BenefitMapping> benefit, string clientKey = null, string apiKey = null);
        Task<Tuple<Benefit, bool>> UpdateBenefitMapping(int id, BenefitMapping benefit, string clientKey = null, string apiKey = null);

        Task<Tuple<Benefit, bool>> AddBenefit(List<Benefit> benefit, string clientKey = null, string apiKey = null);
        Task<Tuple<Benefit, bool>> UpdateBenefit(int id, Benefit benefit, string clientKey = null, string apiKey = null);
        Task<bool> DeleteBenefit(int id, string clientKey = null, string apiKey = null);

        #endregion

        #region Benefit Master

        Task<Tuple<BenefitMaster, bool>> AddBenefitMaster(List<BenefitMaster> benefitMaster, string clientKey = null, string secretKey = null);
        Task<Tuple<BenefitMaster, bool>> UpdateBenefitMaster(int id, BenefitMaster benefitMaster, string clientKey = null, string secretKey = null);
        Task<bool> DeleteBenefitMaster(int id, string clientKey = null, string secretKey = null);

        #endregion


        #region Benefit Items

        Task<Tuple<BenefitItem, bool>> AddBenefitItem(List<BenefitItem> benefitItem, string clientKey = null, string secretKey = null);
        Task<Tuple<BenefitItem, bool>> UpdateBenefitItem(int id, BenefitItem benefitItem, string clientKey = null, string secretKey = null);
        Task<bool> DeleteBenefitItem(int id, string clientKey = null, string secretKey = null);

        #endregion

        #region Benefit Limit Condition

        Task<Tuple<BenefitLimitCondition, bool>> AddBenefitLimitCondition(List<BenefitLimitCondition> benefitLimitCondition, string clientKey = null, string secretKey = null);
        Task<Tuple<BenefitLimitCondition, bool>> UpdateBenefitLimitCondition(int id, BenefitLimitCondition benefitLimitCondition, string clientKey = null, string secretKey = null);
        Task<bool> DeleteBenefitLimitCondition(int id, string clientKey = null, string secretKey = null);

        #endregion


        #region PlanMaster Master

        Task<Tuple<PlanMaster, bool>> AddPlanMaster(List<PlanMaster> planMaster, string clientKey = null, string secretKey = null);
        Task<Tuple<PlanMaster, bool>> UpdatePlanMaster(int id, PlanMaster planMaster, string clientKey = null, string secretKey = null);
        Task<bool> DeletePlanMaster(int id, string clientKey = null, string secretKey = null);

        #endregion

        #region PlanMaster Limit Condition

        Task<Tuple<PlanLimitCondition, bool>> AddPlanLimitCondition(List<PlanLimitCondition> planLimitCondition, string clientKey = null, string secretKey = null);
        Task<Tuple<PlanLimitCondition, bool>> UpdatePlanLimitCondition(int id, PlanLimitCondition planLimitCondition, string clientKey = null, string secretKey = null);
        Task<bool> DeletePlanLimitCondition(int id, string clientKey = null, string secretKey = null);

        #endregion

        #region Benefit Mapping

        Task<Tuple<Benefit, bool>> AddBenefitMappingToBenefitItem(List<Benefit> benefitMapping, string clientKey = null, string secretKey = null);

        Task<Tuple<Benefit, bool>> AddBenefitMappingToSubMedicalService(List<Benefit> benefitMapping, string clientKey = null, string secretKey = null);

        #endregion



    }
}
