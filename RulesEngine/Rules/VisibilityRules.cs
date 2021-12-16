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

        public NewPolicyVisibilityModel CalculateVisibility() => new NewPolicyVisibilityModel
        {
            CompanyDetailsVisible = _data.IsCompany,
            PersonDetailsVisible = !_data.IsCompany,
            DriverDetailsVisible = !_data.IsCompany && (_data.HasDriverLicence || _data.AnnualMilage > 20000),
            ChildrenDetailsVisible = !_data.IsCompany && _data.HasChildren

        };
    }
}
