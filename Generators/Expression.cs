namespace Expressions
{
	/// <summary>
	/// Abstract superclass for expressions 
	/// </summary>
	public abstract class Expression
	{
		# region Abstract Methods

		/// <summary>
		/// Returns a string representation of the expression.
		/// </summary>
		/// <remarks>the ToString method is overriden as abstract to force all 
		/// inheriting classes to implement it.</remarks>
		/// <returns>a string representation of the expression</returns>
		public abstract override string ToString();

		# endregion

		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public abstract void AcceptVisitor(IExpressionVisitor visitor);

	}
}
