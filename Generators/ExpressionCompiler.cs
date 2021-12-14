#region Imports
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using ExpressionCompiler;
using Expressions;

#endregion

namespace ExpressionCompiler
{
	public class ExpressionCompiler
	{
		#region Static Attributes and Methods

		private const string GENERATED_TYPE =
			Expressions2CSharp.GENERATED_NAMESPACE + "." + Expressions2CSharp.GENERATED_CLASS_NAME;


		#endregion


		public string CompileExpressions(
			IDictionary<string, Expression> expr)
		{
			Expressions2CSharp generator = new Expressions2CSharp();

			generator.BeginClass();

			foreach (string fieldId in expr.Keys)
			{
				generator.TransformExpression(
					CSharpCompatibleName(fieldId), expr[fieldId]);
			}

			generator.EndClass();

			return generator.GeneratedCode;
		}

		private string CSharpCompatibleName(string fieldId)
		{
			string compatibleName = fieldId;

			compatibleName = compatibleName.Replace(" ", "_BLANK_");
			compatibleName = compatibleName.Replace("-", "_HYPHEN_");
			compatibleName = compatibleName.Replace("/", "_SLASH_");

			return compatibleName;
		}
	}
}
