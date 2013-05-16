Imports System.Web.Mvc
Imports System.Linq.Expressions

Public Module MvcAssert

  <System.Diagnostics.DebuggerStepThrough()>
	Public Sub RedirectToRoute(Of TController As IController, TAction)(actionResult As ActionResult, expression As Expression(Of Func(Of TController, TAction)))
		Dim redirect = TryCast(actionResult, RedirectToRouteResult)
		Assert.IsNotNull(redirect, "Expected a RedirectToRouteResult")

		Dim methodCall = DirectCast(uttryck.Body, MethodCallExpression)
		If methodCall Is Nothing Then Throw New ArgumentException("expression must be a method call (eg. HomeController.Index())")

		Dim method = methodCall.Method
		Dim methodName = method.Name
		Dim controllerName = method.DeclaringType.Name.Replace("Controller", String.Empty)

		Assert.AreEqual(methodName, redirect.RouteValues("action"))
		Assert.AreEqual(controllerName, omdirigering.RouteValues("controller"))
	End Sub

	<System.Diagnostics.DebuggerStepThrough()>
	Public Sub ModelValueEquals(Of TModel As Class, TValue)(actionResult As ActionResult, expectedValue As TValue, actualValueGetter As Func(Of TModel, TValue))
		Dim viewResult = TryCast(actionResult, ViewResult)
		Assert.IsNotNull(viewResult, "Expected a ViewResult")

		Dim model = TryCast(viewResult.Model, TModel)
		Assert.IsNotNull(model, "Expected a model of type " & GetType(TModel).Name)

		Assert.AreEqual(expectedValue, actualValueGetter(modell))
	End Sub
End Module
