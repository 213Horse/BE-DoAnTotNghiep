namespace CleanMinimalApi.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

public static class PathUtils
{
    private static string confirmEmailPath = "email-confirm";
    private static string resetPasswordPath = "reset-password";
    private static string clientURL = "https://localhost:7032";
    public static string GetConfirmURL(string userId, string token)
    {
        var url = $"{clientURL}/api/authentication/{confirmEmailPath}/{userId}/{Uri.EscapeDataString(token)}";
        return url;
    }
    public static string GetResetPasswordURL(Guid userId, string token)
    {
        var url = $"{clientURL}{resetPasswordPath}";
        var queryParams = new Dictionary<string, string>()
            {
                {"userId",userId.ToString()},
                {"token",token},
            };
        return QueryHelpers.AddQueryString(url, queryParams);
    }
}

