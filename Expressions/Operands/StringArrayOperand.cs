#region Imports
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Expressions.Operands
{
	/// <summary>
	/// Represents a string array as an operand
	/// </summary>
	public class StringArrayOperand : Operand
	{

		#region Attributes and Properties

		private readonly IList<string> _values;

		/// <summary>
		/// Gets the values.
		/// </summary>
		/// <value>The values.</value>
		/// <remarks>Comment created by GhostDoc</remarks>
		public IList<string> values
		{
			get { return _values; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StringArrayOperand"/> class.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public StringArrayOperand(IList<string> values)
		{
			_values = values;
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
			visitor.VisitStringArrayOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the StringArrayOperand.
		/// </summary>
		/// <returns>a string representation of the StringArrayOperand</returns>
		public override string ToString()
		{
						
			StringBuilder operandBuilder = new StringBuilder();
			foreach (String s in _values)
				operandBuilder.Append(string.Format("'{0}', ", s));
			//remove final ", "	
			operandBuilder.Remove(operandBuilder.Length - 2, 2);
			string operands = operandBuilder.ToString();

			return string.Format("[{0}]", operands);
		}

		#endregion

	} 
} 
