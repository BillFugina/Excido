module BamApps {
    export module Excido {
        export module Controller {
            export class LoginController extends BamApps.Model.BamAppsBase implements BamApps.Interface.ILoginController {
                static $inject: string[] = ['$scope', '$location', 'authenticationServiceFactory', 'settingsService'];

                private _authenticationService: BamApps.Interface.IAuthenticationService;

                loginInfo: BamApps.Interface.ILoginInfo;
                message: string = '';

                constructor(
                    private $scope: ng.IScope,
                    private $location: ng.ILocationService,
                    private authenticationServiceFactory: BamApps.Interface.IAuthenticationServiceFactory,
                    private settingsService: BamApps.Excido.Service.SettingsService
                ) {
                    super();
                    this._authenticationService = authenticationServiceFactory.getAuthenticationService();
                }

                Login(): void {
                    this._authenticationService.login(this.loginInfo)
                        .then(response => {
                            this.$location.path('/shared-units');
                        })
                        .catch(err => {
                            this.message = err.error_description;
                            BamApps.Logger.error(this.message, this, err, true, "Login Failure!");
                        });
                }
            }

        }
    }
}