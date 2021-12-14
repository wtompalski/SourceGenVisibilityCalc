namespace Expressions.Operands
{
	/// <summary>
	/// Represents a constant decimal value used as an operand within a term
	/// </summary>
	public class ConstDecimalOperand : Operand
	{
		#region Attributes and Properties

		private readonly decimal _constValue;

		/// <summary>
		/// gets the integer constant
		/// </summary>
		public decimal ConstValue
		{
			get { return _constValue; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of ConstIntOperand
		/// </summary>
		/// <param name="constValue">integer value</param>
		public ConstDecimalOperand(decimal constValue)
		{
			_constValue = constValue;
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
			visitor.VisitConstDecimalOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the ConstDecimalOperand.
		/// </summary>
		/// <returns>a string representation of the ConstDecimalOperand</returns>
		public override string ToString()
		{
			return _constValue.ToString();
		}

		#endregion
	} 
} 
