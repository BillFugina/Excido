using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BamApps.Identity.WebApi.Infrastructure {
    public class ApplicationUserManager : UserManager<ApplicationUser> {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store) {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            appUserManager.EmailService = new Services.EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity")) {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            //Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager) {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };


            //Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            return appUserManager;
        }
    }

    /// <summary>
    /// A replacement for the <see cref="UserValidator"/> which requires that an email 
    /// address be used for the <see cref="IUser.UserName"/> field.
    /// </summary>
    /// <typeparam name="TUser">Must be a type derived from <see cref="Microsoft.AspNet.Identity.IUser"/>.</typeparam>
    /// <remarks>
    /// This validator check the <see cref="IUser.UserName"/> property against the simple email regex provided at
    /// http://www.regular-expressions.info/email.html. If a <see cref="UserManager"/> is provided in the constructor,
    /// it will also ensure that the email address is not already being used by another account in the manager.
    /// 
    /// To use this validator, just set <see cref="UserManager.UserValidator"/> to a new instance of this class.
    /// </remarks>
    public class EmailAsUserNameValidator<TUser> : IIdentityValidator<TUser>
        where TUser : class, Microsoft.AspNet.Identity.IUser {
        private static readonly Regex EmailRegex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly UserManager<TUser> _manager;

        public EmailAsUserNameValidator() {
        }

        public EmailAsUserNameValidator(UserManager<TUser> manager) {
            _manager = manager;
        }

        public async Task<IdentityResult> ValidateAsync(TUser item) {
            var errors = new List<string>();
            if (!EmailRegex.IsMatch(item.UserName))
                errors.Add("Enter a valid email address.");

            if (_manager != null) {
                var otherAccount = await _manager.FindByNameAsync(item.UserName);
                if (otherAccount != null && otherAccount.Id != item.Id)
                    errors.Add("Select a different email address. An account has already been created with this email address.");
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }

}