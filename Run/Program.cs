// See https://aka.ms/new-console-template for more information
using System.Diagnostics;



Expressions.ExpressionParser parser = new();

var expCompany = parser.Parse("IsCompany");
var expPerson = parser.Parse("!IsCompany");


var code = new ExpressionCompiler.ExpressionCompiler().CompileExpressions(new Dictionary<string, Expressions.Expression>
{
    { "CompanyDetailsVisible", expCompany },
    { "PersonDetailsVisible", expPerson }
});

Console.WriteLine(code);