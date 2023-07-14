using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SwEngHomework.Commissions
{
    public enum SeniorityPercentage
    {
        Senior = 100,
        Experienced = 50,
        Junior = 25,
    }

    public class Advisors
    {
        public string name { get; set; }
        public string level { get; set; }
    }

    public class Accounts
    {
        public string advisor { get; set; }
        public string presentValue { get; set; }
    }
    public class CommissionCalculator : ICommissionCalculator
    {
        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            Dictionary<string, double> agentsWithComission = new Dictionary<string, double>();
            JObject inputObject = JObject.Parse(jsonInput);

            var agentAdvisors = JsonConvert.DeserializeObject<List<Advisors>>(inputObject["advisors"].ToString());

            if (agentAdvisors != null && agentAdvisors.Any())
            {
                var agentAccounts = JsonConvert.DeserializeObject<List<Accounts>>(inputObject["accounts"].ToString());
                foreach (var advisor in agentAdvisors)
                {
                    var agentAccount = agentAccounts.Where(acc => acc.advisor == advisor.name).ToList();
                    agentsWithComission.Add(advisor.name, GetComissionBasedOnSeniority(advisor.level, agentAccount));
                }
                
            }

            return agentsWithComission;
        }

        private double GetComissionBasedOnSeniority(string seniority, List<Accounts> agenAccount)
        {
            double agentfinalComission = 0;
            double percentComission = 0;

            foreach (var account in agenAccount)
            {
                double basePoint = GetBasePointByAmount(Convert.ToDouble(account.presentValue));

                if (Enum.TryParse(seniority, out SeniorityPercentage seniorityPercent))
                {
                    switch (seniorityPercent)
                    {
                        case SeniorityPercentage.Senior:
                            percentComission = (Convert.ToDouble(account.presentValue)  * (int)SeniorityPercentage.Senior / 100);
                            agentfinalComission += PercentComissionToFinalComissionBasedOnBasePointValue(percentComission, basePoint);
                            break;
                        case SeniorityPercentage.Experienced:
                            percentComission  = (Convert.ToDouble(account.presentValue) * (int)SeniorityPercentage.Experienced / 100);
                            agentfinalComission += PercentComissionToFinalComissionBasedOnBasePointValue(percentComission, basePoint);
                            break;
                        case SeniorityPercentage.Junior:
                            percentComission = (Convert.ToDouble(account.presentValue) * (int)SeniorityPercentage.Junior / 100);
                            agentfinalComission += PercentComissionToFinalComissionBasedOnBasePointValue(percentComission, basePoint);
                            break;
                    }
                }
            }

            return agentfinalComission;
        }

        

        private double PercentComissionToFinalComissionBasedOnBasePointValue(double agentPercentageComission, double basePoint)
        {
            var comission = Math.Round((double)(agentPercentageComission * basePoint) / 100,2) ;
            return comission;
        }

        private double GetBasePointByAmount(double amount)
        {
            if (amount < 50000)
            {
                return (double)5 / 100;
            }
            else if (amount >= 50000 && amount < 100000)
            {
                return (double)6 / 100;
            }
            else if (amount >= 100000)
            {
                return (double)7 / 100;
            }

            return 0;
        }


    }
}
