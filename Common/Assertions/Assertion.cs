using System;

namespace Common.Assertions
{
	/// <summary>
	/// Use this class' static methods to assert precondition, postconditions etc. in your code
	/// </summary>
	/// <remarks>
	/// </remarks>
	public abstract class Assertion
	{
		#region static public Methods

		/// <summary>
		/// signals an emergency situation by throwing an EmergencyException
		/// </summary>
		/// <param name="messageInfoId">excepton message id</param>
		/// <param name="arguments">arguments to be inserted into the exception message</param>
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		allways thrown with supplied message</exception>"
		public static void Fail( string messageInfoId, params Object[] arguments )
		{
			throw new AssertionException();
		}


		
		/// <summary>
		/// signals an emergency situation if condition is false.
		/// </summary>
		/// <param name="condition">condition which should be true</param>
		/// <param name="conditionDescription">description of condition</param>		
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		thrown if condition is false</exception>"	
		public static void AssertTrue( bool condition, string conditionDescription )
		{
			if( !condition )
			{
				throw new AssertionException();
			}
		}


		/// <summary>
		/// signals an emergency situation if condition is true.
		/// </summary>
		/// <param name="condition">condition which should be false</param>
		/// <param name="conditionDescription">description of condition</param>		
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		thrown if condition is true</exception>"
		public static void AssertFalse( bool condition, string conditionDescription )
		{
			if( condition )
			{	
				throw new AssertionException();
			}
		}


		/// <summary>
		/// signals an emergency situation if object reference is null.
		/// </summary>
		/// <param name="instance">object reference to be tested</param>
		/// <param name="objectName">name of object to be tested</param>
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		thrown if object reference is null</exception>"
		///		
		public static void AssertNotNull(Object instance, string objectName)
		{
			if( null == instance )
			{
				throw new AssertionException();
			}
		}


		/// <summary>
		/// signals an emergency situation if object reference is not null.
		/// </summary>
		/// <param name="instance">object reference to be tested</param>
		/// <param name="objectName">name of object to be tested</param>
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		thrown if object reference is not null</exception>"
		///		
		public static void AssertNull(Object instance, string objectName)
		{
			if( null != instance )
			{
				throw new AssertionException();
			}
		}


		/// <summary>
		/// signals an emergency situation if the a string is empty or even
		/// null
		/// </summary>
		/// <param name="theString">string instance to be tested</param>
		/// <param name="stringName">name of string instance</param>
		/// <exception cref="Mlp.Common.Util.Exceptions.EmergencyException">
		///		thrown if string instance is null or empty</exception>"
		///		
		public static void AssertNotEmpty(string theString, string stringName)
		{
			if( null == theString )
				throw new AssertionException();

			if ( 0 == theString.Length )
				throw new AssertionException();
		}
      #endregion

   }	
} 
