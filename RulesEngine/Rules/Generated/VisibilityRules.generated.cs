using System;
using System.Collections;
using System.Collections.Generic;
using Models;

namespace GeneratedClasses
{
    public partial class VisibilityRules
    {

        public bool FieldVisibility_CompanyDetailsVisible()
        {
            return _data.IsCompany;
        }

        public bool FieldVisibility_PersonDetailsVisible()
        {
            return !_data.IsCompany;
        }
        public partial NewPolicyVisibilityModel CalculateVisibility()
        {
            return new NewPolicyVisibilityModel
            {
                CompanyDetailsVisible = FieldVisibility_CompanyDetailsVisible(),
                PersonDetailsVisible = FieldVisibility_PersonDetailsVisible(),
            };
        }
    }
}
