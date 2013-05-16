using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
namespace System.Web.Mvc.UnitTesting
{
    public static class MvcAssert
    {
        [DebuggerStepThrough]
        public static void RedirectsTo<TController>(Expression<Func<TController, ActionResult>> action, RedirectToRouteResult actionResult)
        {
            const string assertTemplate = "RedirectsTo failed. Expected {0}:<{1}>. Actual:<{2}>.";
 
            var expectedController = typeof(TController).Name;
            var actualController = actionResult.RouteValues["controller"] + "Controller";
            if (expectedController != actualController)
                throw new AssertFailedException(String.Format(assertTemplate, "controller", expectedController, actualController));
 
            var body = (MethodCallExpression) action.Body;
            var expectedAction = body.Method.Name;
            var actualAction = (string) actionResult.RouteValues["action"];
            if (expectedAction != actualAction)
                throw new AssertFailedException(String.Format(assertTemplate, "action", expectedAction, actualAction));
 
            for (int parameterIndex = 0; parameterIndex < body.Method.GetParameters().Length; parameterIndex++)
            {
                var parameterInfo = body.Method.GetParameters()[parameterIndex];
                var parameterName = parameterInfo.Name;
 
                if (!actionResult.RouteValues.ContainsKey(parameterName))
                    throw new AssertFailedException(String.Format(assertTemplate, "parameter", parameterName, "not supplied"));
 
                var expectedParameterValue = GetValue(body.Arguments[parameterIndex]);
                var actualParameterValue = actionResult.RouteValues[parameterName];
                
                if (!expectedParameterValue.Equals(actualParameterValue))
                    throw new AssertFailedException(String.Format(assertTemplate, "value of parameter " + parameterName, expectedParameterValue, actualParameterValue));
            }
        }
 
        // Thank you, Bryan Watts at http://stackoverflow.com/questions/2616638/access-the-value-of-a-member-expression
        private static object GetValue(Expression expression)
        {
            var objectMember = Expression.Convert(expression, typeof(object));
 
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
 
            var getter = getterLambda.Compile();
 
            return getter();
        }
    }
}
