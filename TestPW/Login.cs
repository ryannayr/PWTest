using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

[TestClass]
public class LoginTest : PageTest
{
    [TestMethod]
    public async Task SuccessfulTitle()
    {
        // Navigate to the login page
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");

        // Enter username
        await Page.FillAsync("input#username", "tomsmith");

        // Enter password
        await Page.FillAsync("input#password", "SuperSecretPassword!");

        // Click the login button
        await Page.ClickAsync("button[type='submit']");

        // Verify successful login message
        var flashMessage = Page.Locator("#flash");
        await Expect(flashMessage).ToContainTextAsync(new Regex("You logged into a secure area!"));

        // Verify that the URL has changed to the secure area
        await Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/secure");

    }
}