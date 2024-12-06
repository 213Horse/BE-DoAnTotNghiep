namespace CleanMinimalApi.Infrastructure.Databases.Tourism;

using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanMinimalApi.Application.Destinations;
using CleanMinimalApi.Infrastructure.Databases.Tourism.Models;
using CleanMinimalApi.Application.Authentications;
using Microsoft.AspNetCore.Identity;

using ApplicationDestination = Application.Destinations.Entities.Destination;
using User = Models.ApplicationUser;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using CleanMinimalApi.Infrastructure.Utils;

internal class EntityFrameworkTourismRepository(
    TourismDbContext context,
    IMapper mapper,
    TimeProvider timeProvider,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration
    ) : //IDestinationsRepository,
    IAuthenticationRepository
{
    private readonly IMapper mapper = mapper;
    private readonly TimeProvider timeProvider = timeProvider;
    private readonly TourismDbContext context = context;

    private readonly UserManager<User> userManager = userManager;
    private readonly RoleManager<IdentityRole> roleManager = roleManager;
    private readonly IConfiguration configuration = configuration;

/*    public Task<bool> DestinationExists(
        Guid destinationId,
        CancellationToken cancellationToken)
    {
        return this.context.Destinations.AnyAsync(d => d.Id.Equals(destinationId), cancellationToken);
    }

    public async Task<ApplicationDestination> CreateDestination(string name, string description, CancellationToken cancellationToken)
    {
        var destination = new Destination
        {
            Name = name,
            Description = description,
            DateCreated = this.timeProvider.GetUtcNow().UtcDateTime,
            DateModified = this.timeProvider.GetUtcNow().UtcDateTime
        };

        var id = this.context.Add(destination).Entity.Id;

        _ = await this.context.SaveChangesAsync(cancellationToken);

        var result = await this.context.Destinations
           .Where(r => r.Id == id)
           .AsNoTracking()
           .FirstAsync(cancellationToken);

        return this.mapper.Map<ApplicationDestination>(result);


    }

    public async Task<List<ApplicationDestination>> GetDestinations(CancellationToken cancellationToken)
    {
        var destinations = await this.context.Destinations.AsNoTracking().ToListAsync(cancellationToken);
        return this.mapper.Map<List<ApplicationDestination>>(destinations);
    }*/

    public async Task<bool> Register(string email, string username, string fullName, string password, CancellationToken cancellationToken)
    {
        var userExists = await this.userManager.FindByNameAsync(username);
        if (userExists != null)
        {
            return false;
        }
        var emailExist = await this.userManager.FindByEmailAsync(email);
        if (emailExist != null && await this.userManager.IsEmailConfirmedAsync(emailExist))
        {
            return false;
        }

        var user = new User()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = username,
            FullName = fullName
        };
        var result = await this.userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return false;
        }
        if (!await this.roleManager.RoleExistsAsync(UserRole.User))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRole.User));
        }


        if (await this.roleManager.RoleExistsAsync(UserRole.User))
        {
            await this.userManager.AddToRoleAsync(user, UserRole.User);
        }

        await this.SendConfirmationEmail(email, user);
        return true;

    }

    public async Task<JwtSecurityToken> Login(string username, string password, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByNameAsync(username);
        if (user != null && await this.userManager.CheckPasswordAsync(user, password) && await this.userManager.IsEmailConfirmedAsync(user))
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        return null;

    }

    public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml = false)
    {
        string mailServer = this.configuration["EmailSettings:MailServer"];
        string fromEmail = this.configuration["EmailSettings:FromEmail"];
        string password = this.configuration["EmailSettings:Password"];
        int port = int.Parse(this.configuration["EmailSettings:MailPort"]);
        var client = new SmtpClient(mailServer, port)
        {
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true,
        };
        MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body)
        {
            IsBodyHtml = isBodyHtml
        };
        return client.SendMailAsync(mailMessage);
    }

    private async Task SendConfirmationEmail(string email, User user)
    {
        //Generate the Token
        var token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmURL = PathUtils.GetConfirmURL(user.Id, token);
        await this.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmURL}'>clicking here</a>.", true);
    }

    public async Task<bool> EmailConfirm(string userId, string token, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var emailExist = await this.userManager.FindByEmailAsync(user.Email);
        if (emailExist != null && await this.userManager.IsEmailConfirmedAsync(emailExist))
        {
            return false;
        }

        var result = await this.userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }
}
