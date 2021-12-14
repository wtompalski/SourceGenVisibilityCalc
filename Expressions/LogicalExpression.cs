#region Imports

using Expressions.Operators;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Expressions
{
	/// <summary>
	/// Represents a logical expression
	/// </summary>
	/// <remarks>
	/// <para>A logical expression ist defined as:</para>
	/// <para>Expression LogicalOperator Expression [ LogicalOperator Expersion]* </para>
	/// </remarks>
	public class LogicalExpression : Expression
	{
		#region Attributes and Properties

		private readonly IList<Expression> _expressions = new List<Expression>();
		private readonly IList<LogicalOperator> _operators = new List<LogicalOperator>();

		/// <summary>
		/// Gets the expressions.
		/// </summary>
		/// <value>The expressions.</value>
		/// <remarks>Comment created by GhostDoc</remarks>
		public IList<Expression> Expressions
		{
			get { return _expressions; }
		}

		/// <summary>
		/// Gets the operators.
		/// </summary>
		/// <value>The operators.</value>
		/// <remarks>Comment created by GhostDoc</remarks>
		public IList<LogicalOperator> Operators
		{
			get { return _operators; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of LogicalExpression
		/// </summary>
		/// <param name="expression1">The expression1.</param>
		/// <param name="logicalOperator">logical operator</param>
		/// <param name="expression2">Second expression</param>
		public LogicalExpression(Expression expression1, LogicalOperator logicalOperator, Expression expression2)
		{
			_expressions.Add(expression1);
			_expressions.Add(expression2);
			_operators.Add(logicalOperator);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a further expression to this logical expression
		/// </summary>
		/// <param name="logicalOperator">logical operator</param>
		/// <param name="expression">expression</param>
		public void AddExpression(LogicalOperator logicalOperator, Expression expression)
		{
			_operators.Add(logicalOperator);
			_expressions.Add(expression);
		}

		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		/// <remarks>Comment created by GhostDoc</remarks>
		public override void AcceptVisitor(IExpressionVisitor visitor)
		{
			visitor.VisitLogicalExpression(this);
		}

		/// <summary>
		/// Returns a string representation of the LogicalExpression.
		/// </summary>
		/// <returns>a string representation of the LogicalExpression</returns>
		public override string ToString()
		{
			int countExpressions = _expressions.Count;
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append("(");
			for (int i = 0; i < countExpressions; i++)
			{
				stringBuilder.Append(Expressions[i].ToString());

				if (i < countExpressions - 1)
					stringBuilder.Append(Operators[i].ToString());

			}
			stringBuilder.Append(")");

			return stringBuilder.ToString();
		}

		#endregion
	}
}
