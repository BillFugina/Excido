module BamApps {
    export module Service {

        export class AuthenticationService extends BamApps.Model.BamAppsBase implements Interface.IAuthenticationService {
            static $inject: string[] = ['$http', '$q', 'localStorageService', 'settingsService'];

            private _authentication: Interface.IAuthenticationData = {
                isAuth: false,
                userName: ""
            }

            constructor(
                private $http: ng.IHttpService,
                private $q: ng.IQService,
                private localStorageService: angular.local.storage.ILocalStorageService,
                private settingsService: BamApps.Excido.Service.SettingsService
            ) {
                super();
            }

            saveRegistration(registration): ng.IPromise<any> {
                var self = this;
                var authenticationServiceBaseUrl = self.settingsService.Settings.AuthenticationServiceBaseUrl;
                this.logout();

                return this.$http.post(authenticationServiceBaseUrl + 'api/account/register', registration)
                    .then(function (response) {
                        return response;
                    });
            }

            login(loginData: Interface.ILoginInfo): ng.IPromise<Interface.ILoginResponse> {
                var self = this;
                var authenticationServiceBaseUrl = self.settingsService.Settings.AuthenticationServiceBaseUrl;
                var clientId = self.settingsService.Settings.ApiClientId;
                var data = "client_id=" + clientId + "&grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

                var deferred = self.$q.defer<Interface.ILoginResponse>();

                self.$http.post(authenticationServiceBaseUrl + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                    .success((response: Interface.ILoginResponse) => {
                        var token: Interface.IAuthenticationToken = { token: response.access_token, userName: loginData.userName };
                        self.localStorageService.set('authorizationData', token);
                        self.localStorageService.set('fullName', response.fullName);
                        self.localStorageService.set('expires_in', response.expires_in);
                        self.localStorageService.set('issued', response[".issued"]);
                        self.localStorageService.set('expires', response[".expires"]);
                        self.localStorageService.set('refresh_token', response.refresh_token);

                        self._authentication.isAuth = true;
                        self._authentication.userName = loginData.userName;

                        deferred.resolve(response);

                    })
                    .error(function (err, status) {
                        self.logout();
                        deferred.reject(err);
                    });

                return deferred.promise;
            }

            logout() {
                this.localStorageService.remove('authorizationData');
                this.localStorageService.remove('fullName');
                this.localStorageService.remove('expires_in');
                this.localStorageService.remove('issued');
                this.localStorageService.remove('expires');
                this.localStorageService.remove('refresh_token');
                this._authentication.isAuth = false;
                this._authentication.userName = "";
            }

            verify(): ng.IPromise<boolean> {
                var self = this;
                var authenticationServiceBaseUrl = self.settingsService.Settings.AuthenticationServiceBaseUrl;
                var deferred = self.$q.defer<boolean>();

                self.$http.get(authenticationServiceBaseUrl + 'api/accounts/verify')
                    .success((response: boolean) => {
                        deferred.resolve(response);
                    })
                    .error((err, status) => {
                        deferred.reject({ err: err, status: status });
                    });

                
                return deferred.promise;
            }

            fillAuthData() {
                var authData = this.localStorageService.get<Interface.IAuthenticationToken>('authorizationData');
                if (authData) {
                    this._authentication.isAuth = true;
                    this._authentication.userName = authData.userName;
                }
            }
        }



        var _loginServiceDictionary = {};
        authenticationServiceFactory.$inject = ['$http', '$q', 'localStorageService', 'settingsService'];
        export function authenticationServiceFactory(
            $http: ng.IHttpService,
            $q: ng.IQService,
            localStorageService: angular.local.storage.ILocalStorageService,
            settingsService: BamApps.Excido.Service.SettingsService
        ): Interface.IAuthenticationService {

            var authenticationServiceBaseUrl = settingsService.Settings.AuthenticationServiceBaseUrl;

            var result: Interface.IAuthenticationService = _loginServiceDictionary[authenticationServiceBaseUrl];

            if (result == null) {
                result = new AuthenticationService($http, $q, localStorageService, settingsService);
                _loginServiceDictionary[authenticationServiceBaseUrl] = result;
            }

            return result;
        }

    }
}