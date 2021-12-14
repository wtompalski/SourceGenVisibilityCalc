#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Expressions;
using Common.Assertions;
using Expressions.Terms;
using Expressions.Operators;
using Expressions.Operands;
#endregion

namespace ExpressionCompiler
{
	/// <summary>
	/// Compiles given expression to C# code.
	/// </summary>
	public class Expressions2CSharp : IExpressionVisitor
	{
		/// <summary>
		/// TODO comment this
		/// </summary>
		public const string GENERATED_NAMESPACE = "GeneratedClasses";

		/// <summary>
		/// TODO comment this
		/// </summary>
		public const string GENERATED_CLASS_NAME = "VisibilityRules";

		/// <summary>
		/// TODO comment this
		/// </summary>
		public const string METHOD_NAME_FORMAT = "FieldVisibility_{0}";

		#region Private Fields

		/// <summary>
		/// Buffer for function body.
		/// </summary>
		private StringBuilder _buffer = new StringBuilder();

		private readonly List<string> _generatedFields = new List<string>();

		private static readonly NumberFormatInfo _numberFormat = new NumberFormatInfo();

		#endregion

		#region Attributes and Properties

		/// <summary>
		/// Gets string with current function body code.
		/// </summary>
		public string GeneratedCode
		{
			get { return _buffer.ToString(); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Static constructor to initialize number format
		/// </summary>
		static Expressions2CSharp()
		{
			_numberFormat.NumberDecimalDigits = 0;
			_numberFormat.NumberDecimalSeparator = ".";
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Expressions2CSharp()
		{
		}

		#endregion

		#region IExpressionVisitor Members

		/// <summary>
		/// Visits logical expression.
		/// </summary>
		/// <remarks>
		/// Visits all nested expressions and calculates expression value by adding s.Push(s.Pop...) line
		/// with proper operators.
		/// </remarks>
		/// <param name="le">Logical expression to visit.</param>
		public void VisitLogicalExpression(LogicalExpression le)
		{
			foreach (Expression exp in le.Expressions)
			{
				exp.AcceptVisitor(this);
			}

			// building line as example: s.Push(s.Pop() & (s.Pop() | (s.Pop() | (s.Pop() | (s.Pop())))));
			// from original expression "A | B | C | D & E".
			// Note that there is NO priority of "&" over "|" (historical reasons: CUF States were formerly used
			// and didn't provide these operator priorities. MLP had adapted to this behaviour, so we must
			// conform to the CUF behaviour.
			if (le.Expressions.Count > 1) // s.Push(s.Pop()); is logically empty line
			{
				Assertion.AssertTrue(le.Expressions.Count == le.Operators.Count + 1, "VisitLogicalExpression");

				_buffer.Append("s.Push(s.Pop()");

				StringBuilder closingBraces = new StringBuilder();
				for (int i = le.Operators.Count - 1; i >= 0; i--)
				{
					_buffer.Append(" ");
					_buffer.Append(ConvertToString(le.Operators[i]));
					_buffer.Append(" (s.Pop()");
					closingBraces.Append(")");
				}

				_buffer.Append(closingBraces.ToString());
				_buffer.AppendLine(");");
			}
		}

		/// <summary>
		/// Visits term.
		/// </summary>
		/// <remarks>
		/// Considers 3 types of term: Field in [...], boolvalue = boolvalue and others
		/// consisting of field name, comparison operator and value.
		/// </remarks>
		/// <param name="term">Term to visit.</param>
		public void VisitTerm(Term term)
		{
			if (term.Operator.Type == ComparisonOperator.OpType.IsMember)
			{
				// building line as example: arr = new string[] {"eins", "zwei", "drei"};
				_buffer.Append("arr = ");
				term.Operand2.AcceptVisitor(this);
				_buffer.AppendLine(";");

				// building line as example: t = new List<string>(arr).Contains(fields["feld3"] as string);
				_buffer.Append("t = new List<string>(arr).Contains(");
				term.Operand1.AcceptVisitor(this);
				_buffer.AppendLine(" as string);");
			}
			else if (term.Operand1 is ConstBoolOperand && term.Operand2 is ConstBoolOperand)
			{
				_buffer.Append("t = ");
				bool value = ((term.Operand1 as ConstBoolOperand).BoolValue == (term.Operand2 as ConstBoolOperand).BoolValue);
				new ConstBoolOperand(value).AcceptVisitor(this);
				_buffer.AppendLine(";");
			}
			else
			{
				// building line as example: t = c.Compare(fields["bf1"], "Hallo") == 0;
				_buffer.Append("op1 = ");
				term.Operand1.AcceptVisitor(this);
				_buffer.AppendLine(";");
				_buffer.Append("op2 = ");
				term.Operand2.AcceptVisitor(this);
				_buffer.AppendLine(";");
				_buffer.AppendLine("if (op1 is bool) op2 = op2 ?? false;");
				_buffer.AppendLine("if (op2 is bool) op1 = op1 ?? false;");

				_buffer.Append("t = c.Compare(op1, op2) ");
				_buffer.Append(ConvertToString(term.Operator));
				_buffer.AppendLine(" 0;");
			}

			// s.Push(t);
			_buffer.AppendLine("s.Push(t);");
		}

		/// <summary>
		/// Visits field operand.
		/// </summary>
		/// <remarks>
		/// Depending on which property of the fiel should be evaluate, writes 
		/// gets proper value from fields dictionary or from scenarios dictionary
		/// (namely, wirtes fields["fieldname"] or scenarioUsed["fieldname"] respectively.
		/// </remarks>
		/// <param name="operand">Field operand to visit.</param>
		public void VisitFieldOperand(FieldOperand operand)
		{
			_buffer.Append(string.Format("_data.{0}", operand.FieldName));
		}

		/// <summary>
		/// Visits null operand.
		/// <remarks>
		/// Writes "null".
		/// </remarks>
		/// </summary>
		/// <param name="operand">Null operand to visit.</param>
		public void VisitNullOperand(NullOperand operand)
		{
			_buffer.Append("null");
		}

		/// <summary>
		/// Visits string array operand.
		/// </summary>
		/// <remarks>
		/// For given string array operand (['eins', 'zwei', 'drei']
		/// writes complete array definition (new string[] {"eins", "zwei", "drei"}).
		/// </remarks>
		/// <param name="operand">String Array operand to visit.</param>
		public void VisitStringArrayOperand(StringArrayOperand operand)
		{
			_buffer.Append("new string[] { ");

			int count = operand.values.Count;
			for (int i = 0; i < count; i++)
			{
				new ConstStringOperand(operand.values[i]).AcceptVisitor(this);

				if (i < count - 1)
				{
					_buffer.Append(", ");
				}
			}

			_buffer.Append(" }");
		}

		/// <summary>
		/// Visits const bool operand.
		/// </summary>
		/// <remarks>
		/// Writes "true" or "false" depending on operand's value.
		/// </remarks>
		/// <param name="operand">Const bool operand to visit.</param>
		public void VisitConstBoolOperand(ConstBoolOperand operand)
		{
			if (operand.BoolValue)
			{
				_buffer.Append("true");
			}
			else
			{
				_buffer.Append("false");
			}
		}

		/// <summary>
		/// Visits const string operand.
		/// </summary>
		/// <remarks>
		/// Writes string surrounded by quotation marks.
		/// If string consists of special characters as quotation marks, inserts slash
		/// before them.
		/// </remarks>
		/// <param name="operand"></param>
		public void VisitConstStringOperand(ConstStringOperand operand)
		{
			string value = operand.ConstValue;
			DateTime date;

			bool isDate = DateTime.TryParseExact(value, "dd.MM.yyyy", DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out date);

			if (isDate)
			{
				_buffer.AppendFormat("new DateTime({0}, {1}, {2})", date.Year, date.Month, date.Day);
			}
			else
			{
				_buffer.Append("\"");
				_buffer.Append(value.Replace("\"", "\\\""));
				_buffer.Append("\"");
			}
		}

		/// <summary>
		/// Visits const decimal operand.
		/// </summary>
		/// <remarks>
		/// Writes decimal value. Decimal separator is ".", if the number has no decimal part,
		/// only integer part is written.
		/// </remarks>
		/// <param name="operand"></param>
		public void VisitConstDecimalOperand(ConstDecimalOperand operand)
		{
			_buffer.Append(operand.ConstValue.ToString(_numberFormat));
			_buffer.Append("M");
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Begins the class.
		/// </summary>
		/// <remarks>Comment created by GhostDoc</remarks>
		public void BeginClass()
		{
			_buffer.AppendLine("using System;");
			_buffer.AppendLine("using System.Collections;");
			_buffer.AppendLine("using System.Collections.Generic;");
			_buffer.AppendLine("using Models;");
			_buffer.AppendLine();
			_buffer.AppendFormat("namespace {0}", GENERATED_NAMESPACE);
			_buffer.AppendLine(" {");
			_buffer.AppendFormat("public partial class {0}", GENERATED_CLASS_NAME);
			_buffer.AppendLine(" {");
		}

		/// <summary>
		/// Ends the class.
		/// </summary>
		/// <remarks>Comment created by GhostDoc</remarks>
		public void EndClass()
		{
			GenerateCalculateVisibility();
			_buffer.AppendLine("} }");
		}

		/// <summary>
		/// Transforms the expression.
		/// </summary>
		/// <param name="fieldId">The field id.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="fieldsUsed">The fields used.</param>
		/// <param name="scenarioFlagsUsed">The scenario flags used.</param>
		/// <param name="dialogScenarioUsed">if set to <c>true</c> [dialog scenario used].</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public void TransformExpression(
			string fieldId, Expression expression)
		{
			BeginMethod(fieldId);
			expression.AcceptVisitor(this);
			EndMethod();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Converts comparison operator to string equivalent for C#.
		/// </summary>
		/// <param name="oper">Operator.</param>
		/// <returns>String equivalent of comparision operator.</returns>
		private string ConvertToString(ComparisonOperator oper)
		{
			string result = string.Empty;
			switch (oper.Type)
			{
				case ComparisonOperator.OpType.Equal:
					result = "==";
					break;
				case ComparisonOperator.OpType.Greater:
					result = ">";
					break;
				case ComparisonOperator.OpType.GreaterOrEqual:
					result = ">=";
					break;
				case ComparisonOperator.OpType.IsMember:
					result = " in ";
					break;
				case ComparisonOperator.OpType.Less:
					result = "<";
					break;
				case ComparisonOperator.OpType.LessOrEqual:
					result = "<=";
					break;
				case ComparisonOperator.OpType.NotEqual:
					result = "!=";
					break;
			}

			return result;
		}

		/// <summary>
		/// Converts logical operator to string equivalent for C#.
		/// </summary>
		/// <param name="oper">Operator.</param>
		/// <returns>String equivalent of comparision operator.</returns>
		private string ConvertToString(LogicalOperator oper)
		{
			string result = string.Empty;
			switch (oper.Type)
			{
				case LogicalOperator.OpType.And:
					result = "&";
					break;
				case LogicalOperator.OpType.Or:
					result = "|";
					break;
			}

			return result;
		}

		/// <summary>
		/// Writes method header to the string buffer
		/// </summary>
		private void BeginMethod(string fieldName)
		{
			string methodName = string.Format(METHOD_NAME_FORMAT, fieldName);

			_buffer.AppendLine();
			_buffer.AppendFormat("public bool {0}()", methodName);
			_buffer.AppendLine(" {");
			_buffer.AppendLine("IComparer c = Comparer.Default;");
			_buffer.AppendLine("Stack<bool> s = new Stack<bool>();");
			_buffer.AppendLine("bool t;");
			_buffer.AppendLine("object op1, op2;");
			_buffer.AppendLine("string[] arr;");
			_buffer.AppendLine();

			_generatedFields.Add(fieldName);
		}

		/// <summary>
		/// Writes end of method to the string buffer.
		/// </summary>
		private void EndMethod()
		{
			_buffer.AppendLine();
			_buffer.AppendLine("return s.Pop(); }");
		}

		private void GenerateCalculateVisibility()
        {
			_buffer.AppendLine("public partial NewPolicyVisibilityModel CalculateVisibility() {");
			_buffer.AppendLine("return new NewPolicyVisibilityModel {");
			foreach (var fieldName in _generatedFields)
			{
				string methodName = string.Format(METHOD_NAME_FORMAT, fieldName);
				_buffer.AppendFormat("{0} = {1}(),", fieldName, methodName);
				_buffer.AppendLine();
			}
			_buffer.AppendLine(" }; }");
		}

		#endregion
	}
}
