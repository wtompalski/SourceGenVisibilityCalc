using GeneratedClasses;
using Models;
using System;

namespace RulesEngine
{
    public class RulesEngine
    {
        public NewPolicyVisibilityModel CalculateVisibility(NewPolicyModel data)
        {
            return new VisibilityRules(data).CalculateVisibility();
        }
    }
}
