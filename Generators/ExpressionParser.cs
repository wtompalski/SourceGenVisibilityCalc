#region Imports

using System.Collections.Generic;
using Expressions.Exceptions;
using Expressions.Operands;
using Expressions.Operators;
using Expressions.Terms;

#endregion

namespace Expressions
{

	/// <summary>
	/// Parser for condition expressions
	/// </summary>
	public class ExpressionParser
	{
		#region Static Attributes and Methods

		private static readonly char TEXT_CONST_DEL = '\'';
		private static readonly char STEP_FIELDNAME_DEL = '.';
		private static readonly char FIELDNAME_PROPERTY_DEL = '.';

		#endregion

		/// <summary>
		/// Retrieve one character from source string and convert it to uppercase
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within source</param>
		/// <returns>character retrieved</returns>
		private char GetChar(string source, int pos)
		{

			return source.Substring(pos - 1, 1).ToUpper()[0];

		}

		/// <summary>
		/// Test for a whitespace character.
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within source, gets incremented by one if whitespace character was found</param>
		/// <returns>true if whitespace character is found</returns>
		private bool TestWhitespace(string source, ref int pos)
		{
			bool result = false;
			if (pos > source.Length) return false;

			char c = GetChar(source, pos);
			if ((c == ' '))
			{
				result = true;
				pos++;
			}
			return result;
		}

		/// <summary>
		/// Test for a series of whitespace characters
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">Postion within source, points to</param>
		/// <returns></returns>
		private bool TestWhiteSpaces(string source, ref int pos)
		{
			bool result = TestWhitespace(source, ref pos);
			if (result)
				while (TestWhitespace(source, ref pos)) ;
			return result;
		}

		/// <summary>
		/// Test for a character (A..Z)
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets incremented by one if character is found</param>
		/// <returns>true if a character is found</returns>
		private bool TestChar(string source, ref int pos)
		{
			bool result = false;
			if (pos > source.Length) return false;

			char c = GetChar(source, pos);

			if ((c >= 'A') & (c <= 'Z'))
			{
				result = true;
				pos++;
			}
			return result;
		}

		/// <summary>
		/// Test for a digit
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets incremented by one if character is found</param>
		/// <returns>true if a digit is found</returns>
		private bool TestDigit(string source, ref int pos)
		{
			bool result = false;
			if (pos > source.Length) return false;

			char c = GetChar(source, pos);

			if ((c >= '0') & (c <= '9'))
			{
				result = true;
				pos = pos + 1;
			}
			return result;
		}


		/// <summary>
		/// Test a for a given char
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets incremented by one if delimiter is found</param>
		/// <param name="testChar">The test char.</param>
		/// <returns>
		/// true if testChar is found at given position
		/// </returns>
		private bool TestDefinedChar(string source, ref int pos, char testChar)
		{
			bool result = false;
			if (pos > source.Length) return false;

			if (GetChar(source, pos) == testChar)
			{
				result = true;
				pos = pos + 1;
			}
			return result;

		}

		/// <summary>
		/// Tests for a given keyword
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the keyword</param>
		/// <param name="keyword">keyword to be tested for</param>
		/// <returns>true if keyword is found at given position</returns>
		private bool TestKeyword(string source, ref int pos, string keyword)
		{
			string kw = keyword.ToUpper();
			int lenkw = keyword.Length;

			bool result = false;

			if ((pos - 1 + lenkw <= source.Length) && (source.Substring(pos - 1, lenkw).ToUpper().Equals(kw)))
			{
				result = true;

				pos = pos + lenkw;
			}
			return result;
		}

		/// <summary>
		/// Parses the step of an identifier. Identifier that contain a given step look like Step.Fieldname. 
		/// A Step is represented by one char.
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the step</param>
		/// <returns>the step or null if no step is found at the given position</returns>
		public string ParseStep(string source, ref int pos)
		{
			int p = pos;
			string step = null;

			TestWhiteSpaces(source, ref p);

			if (TestChar(source, ref p))
			{
				while (TestChar(source, ref p)) ;
			}

			//if the chars are followed by a step-localFieldname-delimiter, the parsed characters are a step
			if (TestDefinedChar(source, ref p, STEP_FIELDNAME_DEL))
			{
				//save all read chars except for the delimiter as step
				step = source.Substring(pos - 1, p - pos - 1);
				pos = p;
			}

			return step;
		}

		/// <summary>
		/// Parses the localFieldname of an identifier. Identifier look like Step.Fieldname or like Fieldname.
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the localFieldname</param>
		/// <returns>the localFieldname or null if no localFieldname is found at the given position</returns>
		public string ParseLocalFieldname(string source, ref int pos)
		{
			int p = pos;
			string fieldname = null;

			TestWhiteSpaces(source, ref p);

			if (TestChar(source, ref p))
			{

				while (TestChar(source, ref p) || TestDigit(source, ref p)) ;

				fieldname = source.Substring(pos - 1, p - pos);
				pos = p;
			}

			return fieldname;
		}

		/// <summary>
		/// Parses the key words for Dialog state identifiers (caseinsensitive)
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the dialog state identifier</param>
		/// <returns>the field's property or null if no property is found at the given position</returns>
		public FieldOperand.EvaluableProperty? ParseProperty(string source, ref int pos)
		{

			FieldOperand.EvaluableProperty? fieldProperty = null;
			int p = pos;

			TestWhiteSpaces(source, ref p);

			if (pos > source.Length) return null;

			if (TestKeyword(source, ref p, "SzenariofeldBenutzt"))
			{
				fieldProperty = FieldOperand.EvaluableProperty.SzenarioInputFilled;
				pos = p;
			}
			
			return fieldProperty;

		}


		/// <summary>
		/// Parses a string literal
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the literal</param>
		/// <returns>the literal or null if no string literal is found at the given position</returns>
		public string ParseStringLiteral(string source, ref int pos)
		{
			string textConst = null;
			int p = pos;
			int len = source.Length;

			TestWhiteSpaces(source, ref p);
			int start = p;

			if (TestDefinedChar(source, ref p, TEXT_CONST_DEL))
			{
				bool flag = false;
				do
				{
					flag = GetChar(source, p) == TEXT_CONST_DEL;
					p++;
				} while (p <= len && !flag);
				if (flag)
				{
					textConst = source.Substring(start, p - start - 2);
					pos = p;
				}
			}
			return textConst;
		}

		/// <summary>
		/// Parses an array of string constants
		/// </summary>
		/// <remarks>StringArray := '[' StringLiteral [ , StringLiteral]* ']'</remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated 
		/// to the position after the array</param>
		/// <returns>the array of string constants or null if no array 
		/// is found at the given position</returns>
		/// <exception cref="MissingElementException"/>
		public IList<string> ParseStringArray(string source, ref int pos)
		{
			IList<string> result = null;
			if (pos > source.Length) return null;
			int p = pos;
			TestWhiteSpaces(source, ref p);

			if (GetChar(source, p) == '[')
			{
				p = p + 1;
				result = new List<string>();
				string s = ParseStringLiteral(source, ref p);
				if (s == null)
				{
					throw new MissingElementException(MissingElementException.Elements.Operand, p);
				}
				result.Add(s);
				TestWhiteSpaces(source, ref p);
				while (GetChar(source, p) == ',')
				{
					p = p + 1;
					TestWhiteSpaces(source, ref p);
					s = ParseStringLiteral(source, ref p);
					if (s == null)
					{
						throw new MissingElementException(MissingElementException.Elements.Operand, p);
					}
					result.Add(s);
				}
				TestWhiteSpaces(source, ref p);
				if (GetChar(source, p) != ']')
				{
					throw new MissingElementException(MissingElementException.Elements.Operand, p);
				}
				else
				{
					p = p + 1;
				}
			}
			if (result != null)
				pos = p;
			return result;
		}


		/// <summary>
		/// Parses a bool value
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the bool value</param>
		/// <returns>true, if a null value is found, else false</returns>
		public bool? ParseNullValue(string source, ref int pos)
		{
			bool? result = null;

			TestWhiteSpaces(source, ref pos);

			if (pos > source.Length) return null;

			if (TestKeyword(source, ref pos, "null"))
			{
				result = true;
			}

			return result;
		}

		/// <summary>
		/// Parses the boolean values true and false (caseinsensitive)
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the boolean value</param>
		/// <returns>the boolean value or null if no boolean value is found at the given position</returns>
		public bool? ParseBoolValue(string source, ref int pos)
		{
			bool? result = null;
			int p = pos;

			TestWhiteSpaces(source, ref p);

			if (pos > source.Length) return null;

			if (TestKeyword(source, ref p, "true"))
			{
				result = true;
				pos = p;
			}
			else if (TestKeyword(source, ref p, "false"))
			{
				result = false;
				pos = p;
			}

			return result;
		}


		/// <summary>
		/// Parses a decimal value
		/// </summary>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the decimal value</param>
		/// <returns>the decimal value or null if no decimal value is found at the given position</returns>
		public decimal? ParseDecimalValue(string source, ref int pos)
		{
			decimal? result = null;

			int p = pos;
			int len = source.Length;
			char c;

			TestWhiteSpaces(source, ref p);

			int start = p;
			bool decimalDelimiterFound = false;

			while (p <= len)
			{
				c = GetChar(source, p);

				if (p > start && c == ',' && !decimalDelimiterFound)
				{
					decimalDelimiterFound = true;
				}
				else if ((p == start) && (c != '-' && (c < '0' || c > '9')) ||
						(p > start) && (c < '0' || c > '9'))
				{
					break;
				}

				p = p + 1;
			}

			if (p > start)
			{
				result = decimal.Parse(source.Substring(start - 1, p - start));
			}
			if (result != null)
				pos = p;

			return result;
		}



		/// <summary>
		/// Parses a comparison operator
		/// </summary>
		/// TODO:
		/// <remarks>ComparisonOperator := = | g= | k= | != | 'in'</remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position 
		/// after the operator</param>
		/// <returns>the comparison operator or null if no comparison operator is 
		/// found at the given position</returns>
		public ComparisonOperator ParseComparisonOperator(string source, ref int pos)
		{
			ComparisonOperator result = null;

			TestWhiteSpaces(source, ref pos);

			if (pos > source.Length) return null;

			if (TestKeyword(source, ref pos, "="))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.Equal);
			}
			else if (TestKeyword(source, ref pos, ">="))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.GreaterOrEqual);
			}
			else if (TestKeyword(source, ref pos, "<="))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.LessOrEqual);
			}
			else if (TestKeyword(source, ref pos, "<>"))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.NotEqual);
			}
			else if (TestKeyword(source, ref pos, "<"))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.Less);
			}
			else if (TestKeyword(source, ref pos, ">"))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.Greater);
			}
			else if (TestKeyword(source, ref pos, "!="))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.NotEqual);
			}
			else if (TestKeyword(source, ref pos, "IN"))
			{
				result = new ComparisonOperator(ComparisonOperator.OpType.IsMember);
			}

			return result;
		}

		/// <summary>
		/// Parses an operand
		/// </summary>
		/// <remarks>Operand := FieldOperand | ConstStringOperand | ConstDecimalOperand | ConstBoolOperand </remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the operand</param>
		/// <returns>the operand or null of no operand is found at the given position</returns>
		public Operand ParseOperand(string source, ref int pos)
		{
			Operand op = null;
			int p = pos;

			bool? boolValue = ParseNullValue(source, ref p);
			if (boolValue != null)
			{
				op = new NullOperand();
			}

			//if operand not found, yet
			if (op == null)
			{
				bool? boolOperandValue = ParseBoolValue(source, ref p);

				if (boolOperandValue != null)
				{
					op = new ConstBoolOperand(boolOperandValue.Value);
				}
			}

			//if operand not found, yet
			if (op == null)
			{
				//parse an field identifier. Field identifiers are of the pattern [Step.]Fieldname[.Property] 
				string step = ParseStep(source, ref p);
				string localFieldname = ParseLocalFieldname(source, ref p);
				if (localFieldname != null)
				{
					FieldOperand.EvaluableProperty? property = null;
					// If a Property is given after the fieldname, then try to parse it
					if (TestDefinedChar(source, ref p, FIELDNAME_PROPERTY_DEL)) {
						property = ParseProperty(source, ref p);
					}

					op = new FieldOperand(localFieldname, property);
					
				}
			}

			//if operand not found, yet
			if (op == null)
			{

				string stringLiteral = ParseStringLiteral(source, ref p);

				if (stringLiteral != null)
				{
					op = new ConstStringOperand(stringLiteral);
				}
			}

			//if operand not found, yet
			if (op == null)
			{
				decimal? decimalValue = ParseDecimalValue(source, ref p);
				if (decimalValue != null)
				{
					op = new ConstDecimalOperand(decimalValue.Value);
				}
			}

			//if operand not found, yet
			if (op == null)
			{
				IList<string> stringArray = ParseStringArray(source, ref p);
				if (stringArray != null)
				{
					op = new StringArrayOperand(stringArray);
				}


			}

			if (op != null)
				pos = p;
			return op;
		}

		/// <summary>
		/// Parses a term
		/// </summary>
		/// <remarks><para>Term := operand vergleichsoperator operand</para>
		///	 <para>Term := FieldOperand</para>
		///	 <para>Term := '!' FieldOperand</para>
		///	 <para>Term := ConstBoolOperand</para>
		///	 <para>Term := '!' ConstBoolOperand</para>
		///	 <para>Term := DialogStateOperand</para>
		///	 <para>Term := '!' DialogStateOperand</para>
		/// </remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position after the term</param>
		/// <returns>the term</returns>
		/// <exception cref="ParseException">in case of syntax errors</exception>
		public Expression ParseTerm(string source, ref int pos)
		{
			Expression term = null;
			Operand op1 = null;
			Operand op2 = null;
			ComparisonOperator op = null;

			int p = pos;

			bool isNegatingBoolTerm = false;

			if (TestKeyword(source, ref p, "!"))
			{
				isNegatingBoolTerm = true;
			}

			op1 = ParseOperand(source, ref p);
			if (op1 == null)
			{
				throw new MissingElementException(MissingElementException.Elements.Operand, p);
			}

			op = ParseComparisonOperator(source, ref p);
			if (op == null)
			{
				if (op1 is FieldOperand || op1 is ConstBoolOperand)
				{
					if (isNegatingBoolTerm)
						term = new Term(op1, new ConstBoolOperand(false),
							new ComparisonOperator(ComparisonOperator.OpType.Equal));
					else
						term = new Term(op1, new ConstBoolOperand(true),
							new ComparisonOperator(ComparisonOperator.OpType.Equal));
				}
				else
				{
					throw new MissingElementException(MissingElementException.Elements.ComparisonOperator, p);
				}
			}
			else
			{
				if (isNegatingBoolTerm)
				{
					throw new IllegalElementException(ParseException.Elements.Negation, p);
				}
				op2 = ParseOperand(source, ref p);
				if (op2 == null)
				{
					throw new MissingElementException(MissingElementException.Elements.Operand, p);
				}
				term = new Term(op1, op2, op);
			}

			pos = p;

			return term;
		}




		/// <summary>
		/// Parses a logical operator
		/// </summary>
		/// <remarks>LogicalOperator := 'and' | 'or'</remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position 
		/// after the operator</param>
		/// <returns>the logical operator or null if no comparison operator is 
		/// found at the given position</returns>
		public LogicalOperator ParseLogicalOperator(string source, ref int pos)
		{
			LogicalOperator result = null;

			TestWhiteSpaces(source, ref pos);

			if (TestKeyword(source, ref pos, "and"))
			{
				result = new LogicalOperator(LogicalOperator.OpType.And);
			}
			else if (TestKeyword(source, ref pos, "or"))
			{
				result = new LogicalOperator(LogicalOperator.OpType.Or);
			}

			return result;
		}

		/// <summary>
		/// Parses a simple expression
		/// </summary>
		/// <remarks>SimpleExpression := Term | '(' Expression ')'</remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position 
		/// after the expression</param>
		/// <returns>the expression</returns>
		/// <exception cref="ParseException">in case of syntax errors</exception>
		private Expression ParseSimpleExpression(string source, ref int pos)
		{
			Expression expression = null;
			int p = pos;

			TestWhiteSpaces(source, ref p);

			char c = GetChar(source, p);

			// Geklammerter Ausdruck?

			if (c == '(')
			{
				p++;
				expression = ParseExpression(source, ref p);

				c = GetChar(source, p);
				if (c != ')')
				{
					throw new MissingElementException(MissingElementException.Elements.ClosingBracket, p);
				}
				p++;
			}
			else
			{
				// jetzt muss ein Term kommen

				expression = ParseTerm(source, ref p);
				if (expression == null)
				{
					throw new MissingElementException(MissingElementException.Elements.Term, p);
				}
			}

			if (expression != null) pos = p;

			return expression;
		}


		/// <summary>
		/// Parses an expression
		/// </summary>
		/// <remarks>expression := SimpleExpression LogicalOperator SimpleExpression  [...]</remarks>
		/// <param name="source">string containing the source</param>
		/// <param name="pos">position within the source, gets updated to the position 
		/// after the expression</param>
		/// <returns>the expression</returns>
		/// <exception cref="ParseException">in case of syntax errors</exception>
		public Expression ParseExpression(string source, ref int pos)
		{
			Expression ausdruck = null;
			int p = pos;

			ausdruck = ParseSimpleExpression(source, ref p);

			LogicalOperator op = ParseLogicalOperator(source, ref p);
			if (op != null)
			{
				Expression ausdruck2 = ParseSimpleExpression(source, ref p);
				if (ausdruck2 == null)
				{
					throw new MissingElementException(MissingElementException.Elements.Expression, p);
				}
				LogicalExpression logischerAusdruck = new LogicalExpression(ausdruck, op, ausdruck2);

				while ((op = ParseLogicalOperator(source, ref p)) != null)
				{
					ausdruck = ParseSimpleExpression(source, ref p);
					if (ausdruck == null)
					{
						throw new MissingElementException(MissingElementException.Elements.Expression, p);
					}
					logischerAusdruck.AddExpression(op, ausdruck);
				}
				ausdruck = logischerAusdruck;
			}
			else
			{
				TestWhiteSpaces(source, ref p);
			}

			if (ausdruck != null) pos = p;
			return ausdruck;
		}

		/// <summary>
		/// Parse an expression
		/// </summary>
		/// <param name="source">the source string</param>
		/// <returns>Expression object graph</returns>
		public Expression Parse(string source)
		{
			int p = 1;

			if (source.Trim().Length == 0)
			{
				throw new MissingElementException(ParseException.Elements.Expression, p);
			}

			Expression expr = ParseExpression(source, ref p);


			TestWhitespace(source, ref p);
			if (p <= source.Length)
			{
				throw new IllegalElementException(ParseException.Elements.Expression, p);
			}


			return expr;
		}

	}



}
