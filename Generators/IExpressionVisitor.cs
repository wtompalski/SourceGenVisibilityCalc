using Expressions.Operands;
using Expressions.Terms;

namespace Expressions
{
	/// <summary>
	/// TODO comment this
	/// </summary>
	/// <remarks>Comment created by GhostDoc</remarks>
	public interface IExpressionVisitor
	{
		/// <summary>
		/// Visits the logical expression.
		/// </summary>
		/// <param name="le">The le.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitLogicalExpression(LogicalExpression le);

		/// <summary>
		/// Visits the term.
		/// </summary>
		/// <param name="term">The term.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitTerm(Term term);

		/// <summary>
		/// Visits the field operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitFieldOperand(FieldOperand operand);

		/// <summary>
		/// Visits the null operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitNullOperand(NullOperand operand);

		/// <summary>
		/// Visits the string array operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitStringArrayOperand(StringArrayOperand operand);

		/// <summary>
		/// Visits the const bool operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitConstBoolOperand(ConstBoolOperand operand);

		/// <summary>
		/// Visits the const string operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitConstStringOperand(ConstStringOperand operand);

		/// <summary>
		/// Visits the const decimal operand.
		/// </summary>
		/// <param name="operand">The operand.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		void VisitConstDecimalOperand(ConstDecimalOperand operand);
	}
}
