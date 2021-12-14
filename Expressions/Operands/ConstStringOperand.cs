namespace Expressions.Operands
{
	/// <summary>
	/// Represents a string constant as an operand
	/// </summary>
    public class ConstStringOperand : Operand
	{
		#region Attributes and Properties
		
		private readonly string _constValue;


		/// <summary>
		/// gets the string being the operand
		/// </summary>
		public string ConstValue
		{
			get { return _constValue; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new Instance of ConstStringOperand
		/// </summary>
		/// <param name="constValue">the string being the operand</param>
		public ConstStringOperand(string constValue)
        {
            _constValue = constValue;
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
			visitor.VisitConstStringOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the ConstStringOperand.
		/// </summary>
		/// <returns>a string representation of the ConstStringOperand</returns>
		public override string ToString()
		{
			return string.Format("'{0}'",_constValue);
		}

		#endregion

	}
}
