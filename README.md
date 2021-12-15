# Application
The application allows consultants of insurance company to enter customer data for new policy. New policy wizard is an interactive form that displays relevant fields based on already given data.

The visibility rules come from business in form of expressions fieldXVisibility = <expression>
Example:

- `PersonDetailsVisible = !IsCompany`
- `PersonJobDetailsVisible = !IsCompany AND IsEmployed`
- `DrivingDetailsVisible = !IsCompany AND HasDrivingLicence AND AnnualMilage > 20000`
- `ChildDetailVisible = !IsCompany AND (HasChild OR HasStepChild)`

 For each release the rules are coded by developers manualy in a class like:
  
 ``` csharp
namespace GeneratedClasses
{
    public class VisibilityRules
    {
        public NewPolicyVisibilityModel CalculateVisibility()
        {
            return new NewPolicyVisibilityModel
            {
                PersonDetailsVisible = !_data.IsCompany,
                PersonJobDetailsVisible = !_data.IsCompany && _data.IsEmployed,
                DrivingDetailsVisible = !_data.IsCompany && _data.HasDrivingLicence && _data.AnnualMilage > 20000,
                ChildDetailVisible = !_data.IsCompany && (_data.HasChild || _data.HasStepChild)
            };
        }
    }
}
 ```
# App structure
The following projects:
- `UI`
  
  Contains frontend of the application. For the sake of this assessment you don't have to worry about it.
- `Models`
  
  Contains two models:
 - `NewPolicyDataModel`
 
  Contains data of the new policy.
 - `NewPolicyVisibilityModel`
 
   Contains visibility information.
  ...
- `RulesEngine`
  
  Contains the logic for creating visibility model. The rules are executed on each change in the form.
# Problem definition
As the rules are becoming more and more complex, the development team searches for a solution to automate the process of converting the expressions to code.
Source generators can be handy here. Write a generator that:
- Generates `RuleAttribute` that will decorate the `VisibilityRules` class. Example:
 ``` csharp
 [Rule("DrivingDetailsVisible", "!IsCompany AND HasDrivingLicence AND AnnualMilage > 20000")
 [Rule("ChildDetailVisible", "!IsCompany AND (HasChild OR HasStepChild)")
 public partial class VisibilityRules {...}
 ```
- Generates the (partial) class `VisibilityRules` containing rules for each defined property and a `CreateVisibility` producing the `NewPolicyVisibilityModel` instance.
