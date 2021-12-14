#region Copyright
// <copyright company="MLP AG">
// Copyright (c) MLP AG. Alle Rechte vorbehalten.
// </copyright>
#endregion

#region Imports

#endregion

namespace Expressions.Exceptions
{
	/// <summary>
	/// Thrown if a syntax error is found in the expression
	/// </summary>
	class MissingElementException : ParseException
	{

		#region Attributes and Properties

		private readonly Elements _missingElement;

		/// <summary>
		/// retuns the element that is missing
		/// </summary>
		public Elements MissingElement
		{
			get { return _missingElement; }
		}

		#endregion

		#region Constructors

		public MissingElementException(Elements missingElement, int pos)
			:base(pos)
		{
			_missingElement = missingElement;
		}

		#endregion
	} 
} 
