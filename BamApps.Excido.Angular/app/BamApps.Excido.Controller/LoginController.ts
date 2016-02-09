module BamApps {
    export module Excido {
        export module Controller {
            export class LoginController extends BamApps.Model.BamAppsBase implements BamApps.Interface.ILoginController {
                static $inject: string[] = ['$scope', '$state', 'authenticationService', 'settingsService'];

                private _authenticationService: BamApps.Interface.IAuthenticationService;

                loginInfo: BamApps.Interface.ILoginInfo;
                message: string = '';

                constructor(
                    private $scope: ng.IScope,
                    private $state: ng.ui.IStateService,
                    private authenticationService: BamApps.Interface.IAuthenticationService,
                    private settingsService: BamApps.Excido.Service.SettingsService
                ) {
                    super();
                }

                Login(): void {
                    this.authenticationService.login(this.loginInfo)
                        .then(response => {
                            var fullName = this.authenticationService.userFullName;
                            BamApps.Logger.log("Welcome " + fullName, this, response, toastr.success, "Login Successful");
                            this.$state.go('sharedUnits');
                        })
                        .catch(err => {
                            this.message = err.error_description;
                            BamApps.Logger.error(this.message, this, err, true, "Login Failure!");
                        });
                }

                Logout() {
                    BamApps.Logger.log("You have been logged out.", this, null, toastr.warning, "Logout");
                    this.authenticationService.logout();
                    this.$state.go('home');
                }
            }

        }
    }
}