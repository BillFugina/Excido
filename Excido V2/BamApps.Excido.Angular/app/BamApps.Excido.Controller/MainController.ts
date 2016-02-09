module BamApps {
    export module Excido {
        export module Controller {

            export class MainController extends BamApps.Model.BamAppsBase implements Interface.IMainController {
                static $inject: string[] = ['$state', '$q', '$timeout', 'settingsService', 'authenticationService'];

                constructor(
                    private $state : ng.ui.IStateService,
                    private $q: ng.IQService,
                    private $timeout: ng.ITimeoutService,
                    private settingsService: BamApps.Excido.Interface.ISettingsService,
                    private authenticationService : BamApps.Interface.IAuthenticationService
                ) {
                    super();
                }

                get userFullName(): string {
                    return this.authenticationService.userFullName;
                }

                Logout() {
                    BamApps.Logger.log("You have been logged out.", this, null, toastr.warning, "Logout");
                    this.authenticationService.logout();
                    this.$state.go('home');
                }

                UserSettings(){
                    BamApps.Logger.info("User Settings not implemented.", this, null, toastr.info, "Notice");
                }

            }
        }
    }
}