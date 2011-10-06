using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Linq.Expressions
{
	public class ParameterRebinder : ExpressionVisitor 
	{
		private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
	 
		public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) 
		{
			this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}
	 
		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) 
		{
			return new ParameterRebinder(map).Visit(exp);
		}
	 
		protected override Expression VisitParameter(ParameterExpression p) 
		{
			ParameterExpression replacement;

			if (_map.TryGetValue(p, out replacement)) 
			{
				p = replacement;
			}

			return base.VisitParameter(p);
		}
	}

	public static class ExpressionExtensions 
	{
		public static Expression<Func<T, bool>> True<T>()  { return f => true;  }
		public static Expression<Func<T, bool>> False<T>() { return f => false; }

		public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge) 
		{
			var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
			var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

			return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
		}
	 
		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) 
		{
			return first.Compose(second, Expression.And);
		}
	 
		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) 
		{
			return first.Compose(second, Expression.Or);
		}

		public static string GetMemberName(this Expression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.MemberAccess:
				{
					var memberExpression = (MemberExpression)expression;
					var supername = GetMemberName(memberExpression.Expression);

					if (string.IsNullOrEmpty(supername))
					{
						return memberExpression.Member.Name;
					}

					return string.Concat(supername, '.', memberExpression.Member.Name);
				}

				case ExpressionType.Call:
				{
					var callExpression = (MethodCallExpression)expression;
						
					return callExpression.Method.Name;
				}

				case ExpressionType.Convert:
				{
					var unaryExpression = (UnaryExpression)expression;

					return GetMemberName(unaryExpression.Operand);
				}

				case ExpressionType.Parameter:
				{
					return String.Empty;
				}

				default:
				{
					throw new ArgumentException("The expression is not a member access or method call expression");
				}
			}
		}
	}
}
