#region Copyright
// <copyright company="MLP AG">
// Copyright (c) MLP AG. Alle Rechte vorbehalten.
// </copyright>
#endregion

#region Imports
using System;
#endregion

namespace Expressions.Exceptions
{
	/// <summary>
	/// Thrown if a syntax element is illegal at its position
	/// </summary>
	public class IllegalElementException : ParseException
	{

		#region Attributes and Properties

		private Elements _illegalElement;

		/// <summary>
		/// Gets the illegal element.
		/// </summary>
		/// <value>The illegal element.</value>
		/// <remarks>Comment created by GhostDoc</remarks>
		public Elements IllegalElement
		{
			get { return _illegalElement; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IllegalElementException"/> class.
		/// </summary>
		/// <param name="illegalElement">The illegal element.</param>
		/// <param name="pos">The pos.</param>
		/// <remarks>Comment created by GhostDoc</remarks>
		public IllegalElementException(Elements illegalElement, int pos)
			: base(pos)
		{
			_illegalElement = illegalElement;
		}


		#endregion

	} 
} 
