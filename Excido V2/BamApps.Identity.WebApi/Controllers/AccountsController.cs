using BamApps.Identity.WebApi.Infrastructure;
using BamApps.Identity.WebApi.Models;
using BamApps.Identity.WebApi.Repository;
using BamApps.Identity.WebApi.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace BamApps.Identity.WebApi.Controllers {
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController {

        private AuthRepository authRepository = null;
        public AccountsController() {
            authRepository = new AuthRepository();
        }


        [HttpGet]
        [Route("hello")]
        public string Hello() {
            return "Hello World";
        }

        [HttpGet]
        [Route("hellouser")]
        [Authorize]
        public string HelloUser() {
            return "Hello User";
        }


        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IHttpActionResult GetUsers() {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id) {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null) {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username) {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null) {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded) {
                return GetErrorResult(addUserResult);
            }

            string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            await this.AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "") {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code)) {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded) {
                return Ok();
            }
            else {
                return GetErrorResult(result);
            }
        }

        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded) {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id) {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null) {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded) {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign) {

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser == null) {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0) {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded) {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded) {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }


        private IAuthenticationManager Authentication {
            get { return Request.GetOwinContext().Authentication; }
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null) {
            string redirectUri = string.Empty;

            if (error != null) {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated) {
                return new ChallengeResult(provider, this);
            }

            var redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult)) {
                return BadRequest(redirectUriValidationResult);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null) {
                return InternalServerError();
            }

            if (externalLogin.Provider != provider) {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }


            ApplicationUser user = await AppUserManager.FindAsync(new UserLoginInfo(externalLogin.Provider, externalLogin.ProviderKey));

            ExternalLoginResponse externalLoginResponse = externalLogin.GenerateResponse(isRegistered: user != null);
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(externalLoginResponse);
            ResponseMessageResult result = base.ResponseMessage(httpResponseMessage);
            return result;
        }

        private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput) {

            Uri redirectUri;

            var redirectUriString = GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString)) {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri) {
                return "redirect_uri is invalid";
            }

            var clientId = GetQueryString(Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId)) {
                return "client_Id is required";
            }

            var client = authRepository.FindClient(clientId);

            if (client == null) {
                return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            }

            var actualOrigin = redirectUri.GetLeftPart(UriPartial.Authority);
            if (!string.Equals(client.AllowedOrigin, actualOrigin, StringComparison.OrdinalIgnoreCase)) {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;

        }

        private string GetQueryString(HttpRequestMessage request, string key) {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, StringComparison.CurrentCultureIgnoreCase) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken) {
            ParsedExternalAccessToken parsedToken = null;

            var verifyTokenEndPoint = "";

            if (provider == "Facebook") {
                //You can get it from here: https://developers.facebook.com/tools/accesstoken/
                //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook

                var appToken = "xxxxx";
                verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
            }
            else if (provider == "Google") {
                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
            }
            else {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook") {
                    parsedToken.user_id = jObj["data"]["user_id"];
                    parsedToken.app_id = jObj["data"]["app_id"];

                    if (!string.Equals(Startup.facebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase)) {
                        return null;
                    }
                }
                else if (provider == "Google") {
                    parsedToken.user_id = jObj["user_id"];
                    parsedToken.app_id = jObj["audience"];

                    if (!string.Equals(Startup.googleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase)) {
                        return null;
                    }

                }

            }

            return parsedToken;
        }

        private JObject GenerateLocalAccessTokenResponse(string userName, string client_id) {

            var tokenExpiration = TimeSpan.FromDays(1);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties() {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            props.Dictionary.Add("as:client_id", client_id);
            var ticket = new AuthenticationTicket(identity, props);

            //var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
            var accessToken = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", userName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));

            return tokenResponse;
        }

        // POST api/Account/RegisterExternal
        [AllowAnonymous]
        [Route("RegisterExternal")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel externalBindingModel) {

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var verifiedAccessToken = await VerifyExternalAccessToken(externalBindingModel.Provider, externalBindingModel.ExternalAccessToken);
            if (verifiedAccessToken == null) {
                return BadRequest("Invalid Provider or External Access Token");
            }

            var userName = externalBindingModel.Email;

            ApplicationUser user = await AppUserManager.FindAsync(new UserLoginInfo(externalBindingModel.Provider, verifiedAccessToken.user_id));

            bool hasRegistered = user != null;
            if (hasRegistered) {
                return BadRequest("External user is already registered");
            }

            user = AppUserManager.FindByEmail(externalBindingModel.Email) ?? AppUserManager.FindByName(userName);
            if (user == null) {
                user = new ApplicationUser() {
                    UserName = userName,
                    JoinDate = DateTime.Now.Date,
                    Email = externalBindingModel.Email,
                    FirstName = externalBindingModel.FirstName,
                    LastName = externalBindingModel.LastName
                };

                IdentityResult identityResult = await this.AppUserManager.CreateAsync(user);

                if (!identityResult.Succeeded) {
                    return GetErrorResult(identityResult);
                }
            }

            var externalLoginInfo = new ExternalLoginInfo() {
                DefaultUserName = userName,
                Login = new UserLoginInfo(externalBindingModel.Provider, verifiedAccessToken.user_id)
            };

            IdentityResult result = await AppUserManager.AddLoginAsync(user.Id, externalLoginInfo.Login);
            if (!result.Succeeded) {
                return GetErrorResult(result);
            }

            //generate access token response
            var accessTokenResponse = GenerateLocalAccessTokenResponse(userName, externalBindingModel.ClientId);

            return Ok(accessTokenResponse);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainLocalAccessToken")]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string client_id, string externalAccessToken) {

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken)) {
                return BadRequest("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
            if (verifiedAccessToken == null) {
                return BadRequest("Invalid Provider or External Access Token");
            }

            IdentityUser user = await AppUserManager.FindAsync(new UserLoginInfo(provider, verifiedAccessToken.user_id));

            bool hasRegistered = user != null;

            if (!hasRegistered) {
                return BadRequest("External user is not registered");
            }

            //generate access token response
            var accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName, client_id);

            return Ok(accessTokenResponse);

        }
    }
}