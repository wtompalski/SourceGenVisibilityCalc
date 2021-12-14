using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratedClasses
{
    public partial class VisibilityRules
    {
        private readonly NewPolicyModel _data;

        public VisibilityRules(NewPolicyModel data)
        {
            _data = data;
        }

        public partial NewPolicyVisibilityModel CalculateVisibility();
    }
}
