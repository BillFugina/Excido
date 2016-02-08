module BamApps {
    export module Excido {
        export module Controller {
            export class SignupController extends BamApps.Model.BamAppsBase {
                static $inject: string[] = ['$scope', '$location', '$timeout', 'authenticationService'];

                private _authenticationService: BamApps.Interface.IAuthenticationService;

                registrationInfo: BamApps.Interface.IRegistrationInfo;
                message: string = '';
                savedSuccessfully: boolean = false;

                constructor(
                    private $scope: ng.IScope,
                    private $location: ng.ILocationService,
                    private $timeout: ng.ITimeoutService,
                    private authenticationService: BamApps.Interface.IAuthenticationService
                ) {
                    super();
                }

                Signup() {
                    var self = this;
                    this._authenticationService.saveRegistration(self.registrationInfo)
                        .then(response => {
                            self.savedSuccessfully = true;
                            self.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                            BamApps.Logger.toast("User has been registered successfully", self, response, toastr.success, "Success");
                            self.startTimer();
                        })
                        .catch(response => {
                            var errors = [];
                            for (var key in response.data.modelState) {
                                for (var i = 0; i < response.data.modelState[key].length; i++) {
                                    errors.push(response.data.modelState[key][i]);
                                }
                            }
                            BamApps.Logger.error("Failed to register user.", this, errors, true);
                            self.message = "Failed to register user due to:" + errors.join(' ');
                        });
                }

                private startTimer() {
                    var self = this;
                    var timer = this.$timeout(() => {
                        self.$timeout.cancel(timer);
                        self.$location.path('/login');
                    }, 2000);
                }
            }
        }
    }
}