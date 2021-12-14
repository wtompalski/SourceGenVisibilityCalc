namespace Expressions.Operands
{
	/// <summary>
	/// Represents a null constant used as operand
	/// </summary>
	/// 
	/// <remarks></remarks>
	public class NullOperand : Operand
	{
		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		/// <remarks>Comment created by GhostDoc</remarks>
		public override void AcceptVisitor(IExpressionVisitor visitor)
		{
			visitor.VisitNullOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the NullOperand.
		/// </summary>
		/// <returns>a string representation of the NullOperand</returns>
		public override string ToString()
		{
			return "null";
		}
	}
}
