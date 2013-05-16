MvcAssert
=========

Testing library, targetting ASP.NET MVC

(there are two different implementations in the initial commit)

C# usage 
-
	[TestMethod]
	public void AddTileImage_GivenTileImageModel_CallsAddTileImage_ThenRedirectsToTileDetails()
	{
    var mockedFile = new MockedFileBase(new byte[] {0x1, 0x2, 0x3});
    var model = new TileImageModel
                    {
                        TileId = 1,
                        File = mockedFile
                    };
 
    var result = controller.AddTileImage(model) as RedirectToRouteResult;
 
    ImageServiceMock.Verify(mock => mock.AddTileImage(model.TileId, mockedFile.Buffer), Times.Once());
 
    MvcAssert.RedirectsTo<TileController>(t => t.Details(model.TileId), result);
	}
	
VB.NET usage (RedirectToRoute)
-
		<TestMethod()>
		Public Sub LoggedIn_RedirectsTo_TechnicalError_If_User_Is_Missing()
			_userServiceMock.Setup(Function(mock) mock.GetUser()).Returns(DirectCast(Nothing, UserModel))

			MvcAssert.RedirectToRoute(_controller.LoggedIn(), Function(technicalError As TechnicalErrorController) technicalError.Index())
		End Sub

VB.NET usage (ModelValueEquals)
-
		<TestMethod()>
		Public Sub LoggedIn_SetsIts_ViewModels_AccountProperties()
			SetupUserOchAccessMocks()

			Dim result = _controller.LoggedIn()

			MvcAssert.ModelValueEquals(Of StartPageModel, String)(resultat, "SelectedAccountName", Function(model) model.SelectedAccount.Name)
		End Sub
