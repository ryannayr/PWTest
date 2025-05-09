using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using PlaywrightTests.PageObjects;

namespace PlaywrightTests;

[TestClass]
public class LoginTests : PageTest
{
    [TestMethod]
    public async Task SuccessfulLoginUsingPOM()
    {
        // Initialize the Page Object Model
        var loginPage = new LoginPage(Page);

        // Navigate to the login page
        await loginPage.NavigateAsync();

        // Perform login
        await loginPage.LoginAsync("tomsmith", "SuperSecretPassword!");

        // Verify successful login message
        await Expect(loginPage.FlashMessage).ToContainTextAsync("You logged into a secure area!");

        // Verify that the URL has changed to the secure area
        await Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/secure");
    }

    [TestMethod]
    public async Task UnsuccessfulLoginUsingPOM()
    {
        // Initialize the Page Object Model
        var loginPage = new LoginPage(Page);

        // Navigate to the login page
        await loginPage.NavigateAsync();

        // Perform login with invalid credentials
        await loginPage.LoginAsync("invalidUser", "invalidPass");

        // Verify error message
        await Expect(loginPage.FlashMessage).ToContainTextAsync("Your username is invalid!");
    }
}