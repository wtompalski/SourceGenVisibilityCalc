using System;

namespace Expressions.Exceptions
{
	/// <summary>
	/// Subclasses are thrown in case of syntax errors
	/// </summary>
	/// <remarks></remarks>
	public abstract class ParseException : Exception
	{
		/// <summary>
		/// TODO comment this
		/// </summary>
		public enum Elements {
			/// <summary>
			/// TODO comment this
			/// </summary>
			Operand,
			/// <summary>
			/// TODO comment this
			/// </summary>
			LogicalOperator,
			/// <summary>
			/// TODO comment this
			/// </summary>
			ComparisonOperator,
			/// <summary>
			/// TODO comment this
			/// </summary>
			ClosingBracket,
			/// <summary>
			/// TODO comment this
			/// </summary>
			Term,
			/// <summary>
			/// TODO comment this
			/// </summary>
			Expression,
			/// <summary>
			/// TODO comment this
			/// </summary>
			Negation };

		private readonly int _atPosition;

		/// <summary>
		/// Gets at position.
		/// </summary>
		/// <value>At position.</value>
		/// <remarks>Comment created by GhostDoc</remarks>
		public int AtPosition
		{
			get { return _atPosition; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParseException"/> class.
		/// </summary>
		/// <param name="pos">The pos.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public ParseException(int pos)
		{
			_atPosition = pos;
		}
	} 
} 
