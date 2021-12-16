// See https://aka.ms/new-console-template for more information
using System.Diagnostics;


Expressions.ExpressionParser parser = new Expressions.ExpressionParser();

var code = new global::ExpressionCompiler.ExpressionCompiler().CompileExpressions(new Dictionary<string, Expressions.Expression>
            {
                { "CompanyDetailsVisible", parser.Parse("IsCompany") },
                { "PersonDetailsVisible", parser.Parse("!IsCompany") },
                { "ChildrenDetailsVisible", parser.Parse("!IsCompany AND HasChildren") },
                { "DriverDetailsVisible", parser.Parse("!IsCompany AND (HasDriverLicence OR AnnualMilage > 20000)") },
            });

Console.WriteLine(code);