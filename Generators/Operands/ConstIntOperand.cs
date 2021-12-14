namespace Expressions.Operands
{
	/// <summary>
	/// Represents an integer used as an operand within a term
	/// </summary>
	class ConstIntOperand : Operand
	{
		#region Attributes and Properties

		private readonly int _constValue;

		/// <summary>
		/// gets the integer constant
		/// </summary>
		public int ConstValue
		{
			get { return _constValue; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of ConstIntOperand
		/// </summary>
		/// <param name="constValue">integer value</param>
		public ConstIntOperand(int constValue)
		{
			_constValue = constValue;
		}


		#endregion

		#region Public Methods

		public override void AcceptVisitor(IExpressionVisitor visitor)
		{
			//obsolete
		}

		/// <summary>
		/// Returns a string representation of the ConstDoubleOperand.
		/// </summary>
		/// <returns>a string representation of the ConstDoubleOperand</returns>
		public override string ToString()
		{
			return _constValue.ToString();
		}

		#endregion
	}
} 
