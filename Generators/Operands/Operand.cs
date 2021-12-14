namespace Expressions.Operands
{


	/// <summary>
	/// Superclass of Operands
	/// </summary>
	public abstract class Operand
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Operand"/> class.
		/// </summary>
		/// <remarks>Comment created by GhostDoc</remarks>
		public Operand(){
			// nothing to do
		}

		#endregion

		# region Abstract Methods

		/// <summary>
		/// Returns a string representation of the Operand.
		/// </summary>
		/// <remarks>the ToString method is overriden as abstract to force all 
		/// inheriting classes to implement it.</remarks>
		/// <returns>a string representation of the operand</returns>
		public abstract override string ToString();

		/// <summary>
		/// Accepts the visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public abstract void AcceptVisitor(IExpressionVisitor visitor);

		# endregion

	}


}


