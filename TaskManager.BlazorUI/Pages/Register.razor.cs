using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models;

namespace TaskManager.BlazorUI.Pages;

public partial class Register
{
	public RegisterVM Model { get; set; }

	[Inject]
	public INavigationService NavigationService { get; set; }

	public string Message { get; set; }

	[Inject]
	private IAuthenticationService AuthenticationService { get; set; }

	protected override void OnInitialized()
	{
		Model = new RegisterVM();
	}

	protected async Task HandleRegister()
	{
		var result = await AuthenticationService.RegisterAsync(Model.FirstName, Model.LastName, Model.Email, Model.Email, Model.Password);

		if (result)
		{
			await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password);
			NavigationService.GoBack();
		}
		Message = "Something went wrong, please try again.";
	}
}