﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers
 * for more information concerning the license and the contributors participating to this project.
 */

namespace AspNet.Security.OAuth.ArcGIS;

public class ArcGISTests : OAuthTests<ArcGISAuthenticationOptions>
{
    public ArcGISTests(ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;
    }

    public override string DefaultScheme => ArcGISAuthenticationDefaults.AuthenticationScheme;

    protected internal override void RegisterAuthentication(AuthenticationBuilder builder)
    {
        builder.AddArcGIS(options => ConfigureDefaults(builder, options));
    }

    [Theory]
    [InlineData(ClaimTypes.NameIdentifier, "my-id")]
    [InlineData(ClaimTypes.Name, "John Smith")]
    [InlineData(ClaimTypes.Email, "john@john-smith.local")]
    public async Task Can_Sign_In_Using_ArcGIS(string claimType, string claimValue)
    {
        // Arrange
        using var server = CreateTestServer();

        // Act
        var claims = await AuthenticateUserAsync(server);

        // Assert
        AssertClaim(claims, claimType, claimValue);
    }
}
