using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    [Generator]
    public class VisibilityRulesSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            Expressions.ExpressionParser parser = new Expressions.ExpressionParser();

            var expCompany = parser.Parse("IsCompany");
            var expPerson = parser.Parse("!IsCompany");

            var code = new global::ExpressionCompiler.ExpressionCompiler().CompileExpressions(new Dictionary<string, Expressions.Expression>
            {
                { "CompanyDetailsVisible", expCompany },
                { "PersonDetailsVisible", expPerson }
            });

            context.AddSource("VisibilityRules.cs", SourceText.From(code, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
