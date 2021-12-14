namespace Expressions.Operators
{

	/// <summary>
	/// Represents a comparison operator
	/// </summary>
	public class ComparisonOperator
	{

		#region Static Attributes and Methods
		
		/// <summary>
		/// Operator types
		/// </summary>
		public enum OpType { 
			/// <summary>
			/// equal operator
			/// </summary>
			Equal, 
			/// <summary>
			/// not equal operator
			/// </summary>
			NotEqual, 
			/// <summary>
			/// less operator
			/// </summary>
			Less, 
			/// <summary>
			/// greater operator
			/// </summary>
			Greater, 
			/// <summary>
			/// less or equal operator
			/// </summary>
			LessOrEqual, 
			/// <summary>
			/// greater or equal operator
			/// </summary>
			GreaterOrEqual, 
			/// <summary>
			/// is member operator
			/// </summary>
			IsMember};

		#endregion

		#region Attributes and Properties
		
		private readonly OpType _type;

		/// <summary>
		/// the type of this operator
		/// </summary>
		public OpType Type
		{
			get { return _type; }
		}

		#endregion

		#region constructors

		/// <summary>
		/// Inializes an instance of ComparisonOperator
		/// </summary>
		/// <param name="type"></param>
		public ComparisonOperator(OpType type){
            _type = type;
		}

		#endregion


		# region Public Methods

		/// <summary>
		/// Returns a string representation of the ComparisonOperator.
		/// </summary>
		/// <returns>a string representation of the ComparisonOperator</returns>
		public override string ToString()
		{
			string result = "";

			switch (_type)
			{
				case OpType.Equal:
					result = "=";
					break;
				case OpType.Greater:
					result = ">";
					break;
				case OpType.GreaterOrEqual:
					result = ">=";
					break;
				case OpType.IsMember:
					result = " in ";
					break;
				case OpType.Less:
					result = "<";
					break;
				case OpType.LessOrEqual:
					result = "<=";
					break;
				case OpType.NotEqual:
					result = "!=";
					break;
			}

			return result;
		}

		#endregion


	}

}

