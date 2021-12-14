namespace Expressions.Operands
{
	/// <summary>
	/// Represents a field as an operand
	/// </summary>
	public class FieldOperand : Operand
	{
		#region Attributes and Properties

		//the global field name that is unique within the selection type
		private string _fieldName;

		//the local field name, that is unique only within a step
		private readonly string _localFieldName;

		//the field's property whose value the operand relates to
		private EvaluableProperty? _propertyToEvaluate;


		/// <summary>
		/// field propertys whose value the a FieldOperand can relate to
		/// </summary>
		public enum EvaluableProperty { 
			/// <summary>
			/// The property can be the value of the field
			/// </summary>
			Value, 
			/// <summary>
			/// The property can be the information if the Szenario Input field was used to overwrite the saved value
			/// </summary>
			SzenarioInputFilled };


		/// <summary>
		/// Gets the local name of the field being the operand
		/// </summary>
		public string LocalFieldName
		{
			get { return _localFieldName; }
		}

		/// <summary>
		/// Gets and sets the global name of the field being the operand
		/// </summary>
		/// 
		public string FieldName
		{
			get { return _fieldName; }
			set { _fieldName = value; }
		}


		/// <summary>
		/// Gets the field's property whose value the operand relates to
		/// </summary>
		public EvaluableProperty? PropertyToEvaluate
		{
			get { return _propertyToEvaluate; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Inializes a new instance of FieldOperand
		/// </summary>
		/// <param name="localFieldName">name of the field beeing the operand</param>
		/// <param name="step">name of the step that contains the field</param>
		/// <param name="propertyToEvaluate">the field's property whose value the operand relates to</param>
		public FieldOperand(string localFieldName, EvaluableProperty? propertyToEvaluate)
		{
			_localFieldName = localFieldName;
			FieldName = localFieldName;
			_propertyToEvaluate = propertyToEvaluate;
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
			visitor.VisitFieldOperand(this);
		}

		/// <summary>
		/// Returns a string representation of the FieldOperand.
		/// </summary>
		/// <returns>a string representation of the FieldOperand</returns>
		public override string ToString()
		{
			if (_propertyToEvaluate != null)
				return string.Format("{0}.{1}", _localFieldName, _propertyToEvaluate);
			else
				return string.Format("{0}", _localFieldName);
		}

		#endregion

	}
}
