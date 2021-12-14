namespace Expressions.Operands
{
	/// <summary>
	/// Represents a boolean value as an operand
	/// </summary>
    public class ConstBoolOperand : Operand
	{
		#region Attributes and Properties
		
		private readonly bool _boolValue;


		/// <summary>
		/// gets the bool being the operand
		/// </summary>
		public bool BoolValue
		{
			get { return _boolValue; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new Instance of ConstBoolOperand
		/// </summary>
		/// <param name="boolValue">if set to <c>true</c> [bool value].</param>
		public ConstBoolOperand(bool boolValue)
        {
			_boolValue = boolValue;
		}

		#endregion

		# region Public Methods

		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		/// <remarks>Comment created by GhostDoc</remarks>
		public override void AcceptVisitor(IExpressionVisitor visitor)
		{
			visitor.VisitConstBoolOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the ConstBoolOperand.
		/// </summary>
		/// <returns>a string representation of the ConstBoolOperand</returns>
		public override string ToString()
		{
			return _boolValue.ToString().ToUpper();
		}

		#endregion

	}
}
