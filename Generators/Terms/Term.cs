#region Imports

using Common.Assertions;
using Expressions.Operands;
using Expressions.Operators;

#endregion

namespace Expressions.Terms
{

	/// <summary>
	/// Represents a Term
	/// </summary>
	/// <remarks>
	/// <para>A term is defined as follows:</para>
	/// <para>Term := Operand ComparissonOperator Operand</para>
	/// </remarks>
	public class Term : Expression
	{

		#region Attributes and Properties

		private readonly Operand _op1;
		private readonly Operand _op2;
		private readonly ComparisonOperator _op;

		/// <summary>
		/// Gets the first operand
		/// </summary>
		public Operand Operand1
		{
			get { return _op1; }
		}

		/// <summary>
		/// Gets the second operand
		/// </summary>
		public Operand Operand2
		{
			get { return _op2; }
		}

		/// <summary>
		/// Gets the operator
		/// </summary>
		public ComparisonOperator Operator
		{
			get { return _op; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes an instance of Term.
		/// </summary>
		/// <param name="op1">Operand 1</param>
		/// <param name="op2">Operand 2</param>
		/// <param name="op">operator</param>
		public Term(Operand op1, Operand op2, ComparisonOperator op){

			Assertion.AssertNotNull(op1, "op1");
			Assertion.AssertNotNull(op2, "op2");
			Assertion.AssertNotNull(op, "op");
			
			// check rules

			if (op.Type == ComparisonOperator.OpType.IsMember)
			{
				if (!(op1 is FieldOperand))
				{
					//TODO: LogicException
				}

				if (!(op2 is StringArrayOperand))
				{
					//TODO: LogicException
				}
			}
			else
			{

				if (!(op1 is ConstDecimalOperand ||
									 op1 is ConstStringOperand ||
									 op2 is ConstDecimalOperand ||
									 op2 is ConstStringOperand) )
				{
					//TODO: LogicException
				}
			}

            _op1 = op1;
            _op2 = op2;
            _op = op;

		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		/// <remarks>Comment created by GhostDoc</remarks>
		public override void AcceptVisitor(IExpressionVisitor visitor)
		{
			visitor.VisitTerm(this);
		}

		/// <summary>
		/// Returns a string representation of the Term.
		/// </summary>
		/// <returns>a string representation of the Term</returns>
		public override string ToString()
		{
			return string.Format("{0} {1} {2}", _op1.ToString(), _op.ToString(), _op2.ToString());
		}

		#endregion
	}

}
