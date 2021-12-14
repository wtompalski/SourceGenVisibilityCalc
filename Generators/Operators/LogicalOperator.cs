namespace Expressions.Operators
{

	/// <summary>
	/// Represents a logical operator (and, or)
	/// </summary>
	public class LogicalOperator
	{

		#region Static Attributes and Methods
		
		/// <summary>
		/// Types of logical operators
		/// </summary>
		public enum OpType {
			/// <summary>
			/// TODO comment this
			/// </summary>
			And,
			/// <summary>
			/// TODO comment this
			/// </summary>
			Or  };

		#endregion

		#region Attributes and Properties

		private readonly OpType _type;

		/// <summary>
		/// gets the type of this operator
		/// </summary>
		public OpType Type
		{
			get { return _type; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LogicalOperator"/> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public LogicalOperator(OpType type){
            _type = type;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Returns a string representation of the LogicalOperator.
		/// </summary>
		/// <returns>a string representation of the LogicalOperator</returns>
		public override string ToString()
		{
			string result = "";

			switch (_type)
			{
				case OpType.And:
					result = " AND ";
					break;
				case OpType.Or:
					result = " OR ";
					break;
			}

			return result;
		}

		#endregion

	}

}

