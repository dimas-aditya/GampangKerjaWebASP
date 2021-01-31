using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCMSSMI.Utils
{
    public class ServerApi
    {

        //...........................ROUTING SERVER
        private static string URL_STAGING = "-stage-api.com/";
        private static string URL_PROD = "-api.com/";

        private static string URL_IP = "http://localhost";


        //...........................BASE
        public static string URL_Cases = "http://www.smi-cases" + URL_STAGING;
        public static string URL_Beneficiary = "http://www.smi-beneficiary" + URL_STAGING;
        public static string URL_BillingCalculation = "http://www.smi-billing-calculation" + URL_STAGING;
        public static string URL_Billing = "http://www.smi-billing" + URL_STAGING;
        public static string URL_CaseMonitoring = "http://www.smi-case-monitoring" + URL_STAGING;
        public static string URL_Batch = "http://www.smi-claim-batch" + URL_STAGING;
        public static string URL_AuthGateway = URL_IP + ":8801/";
        public static string URL_Indexing = "http://www.smi-indexing" + URL_STAGING;
        public static string URL_ICD = "http://www.smi-icd" + URL_STAGING;
        public static string URL_Policy = "http://www.smi-policy" + URL_STAGING;
        public static string URL_Plan = "http://www.smi-plan" + URL_STAGING;
        public static string URL_Provider = "http://www.smi-provider" + URL_STAGING;
        public static string URL_Benefit = "http://www.smi-benefit" + URL_STAGING;
        public static string URL_PolicyClause = "http://www.smi-policy-clause" + URL_STAGING;
        public static string URL_Validation = "http://www.smi-validation" + URL_STAGING;
        public static string URL_RegistrationValidation = "http://www.smi-registration-validation" + URL_STAGING;
        public static string URL_Report = "http://www.smi-reports" + URL_PROD;


    }
}