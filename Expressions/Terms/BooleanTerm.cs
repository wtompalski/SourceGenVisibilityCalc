using Common.Assertions;
using Expressions.Operands;

namespace Expressions.Terms
{
	/// <summary>
	/// Represents a BooleanTerm that is either true or false.
	/// </summary>
	/// <remarks>
	/// <para>A boolean term ist defined as:</para>
	/// <para>BooleanTerm := true | false</para>
	/// </remarks>
	public class BooleanTerm : Expression
	{
		#region Attributes and Properties

		private readonly Operand _boolOperand;
		private readonly bool _isNegating;

		/// <summary>
		/// Gets the operand
		/// </summary>
		public Operand BoolOperand
		{
			get { return _boolOperand; }
		}

		/// <summary>
		/// Gets a value indicating that the term is negating the Operand
		/// </summary>
		public bool IsNegating
		{
			get { return _isNegating; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes an instance of BooleanTerm.
		/// </summary>
		/// <param name="operand">boolean operand</param>
		/// <param name="isNegating">true, if term should negate the operand</param>
		public BooleanTerm (Operand operand, bool isNegating)
		{
			Assertion.AssertNotNull(operand, "operand");

			_boolOperand = operand;
			_isNegating = isNegating;
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
			//obsolete
		}


		/// <summary>
		/// Returns a string representation of the BooleanTerm.
		/// </summary>
		/// <returns>a string representation of the BooleanTerm</returns>
		public override string ToString()
		{
			if (_isNegating)
			{
				return "!" + _boolOperand.ToString();
			}
			else
			{
				return _boolOperand.ToString();
			}
		}

		#endregion
	}
} 
