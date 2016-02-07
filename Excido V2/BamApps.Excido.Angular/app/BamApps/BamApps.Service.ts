module BamApps {
    export module Service {

        getAuthenticationInteceptorService.$inject = ['$q', '$location', 'localStorageService'];
        export function getAuthenticationInteceptorService($q: ng.IQService, $location: ng.ILocationService, localStorageService: angular.local.storage.ILocalStorageService): ng.IHttpInterceptor {
            return new AuthenticationInterceptorService($q, $location, localStorageService);
        }

        export class AuthenticationInterceptorService extends BamApps.Model.BamAppsBase implements ng.IHttpInterceptor {
            static $inject: string[] = ['$q', '$location', 'localStorageService'];
            constructor(
                private $q: ng.IQService,
                private $location: ng.ILocationService,
                private localStorageService: angular.local.storage.ILocalStorageService
            ) {
                super();
            }

            request = (config: ng.IRequestConfig) => {
                config.headers = config.headers || {};

                var authData = this.localStorageService.get<Interface.IAuthenticationToken>('authorizationData');
                if (authData) {
                    config.headers['Authorization'] = 'Bearer ' + authData.token;
                }

                return config;
            }

            responseError = (rejection: ng.IHttpPromiseCallbackArg<any>) => {
                if (rejection.status === 401) {
                    this.$location.path('/login');
                }

                return this.$q.reject(rejection);
            }
        }

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

            saveRegistration(registration) : ng.IPromise<any> {
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
                var data = "client_id=" + clientId  + "&grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

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

                this._authentication.isAuth = false;
                this._authentication.userName = "";
            }

            fillAuthData = function () {

                var authData = this.localStorageService.get('authorizationData');
                if (authData) {
                    this._authentication.isAuth = true;
                    this._authentication.userName = authData.userName;
                }
            }

        }



        var _loginServiceDictionary = {};
        authenticationServiceFactory.$inject = ['$http', '$q', 'localStorageService', 'settingsService'];
        export function authenticationServiceFactory (
            $http: ng.IHttpService,
            $q: ng.IQService,
            localStorageService: angular.local.storage.ILocalStorageService,
            settingsService: BamApps.Excido.Service.SettingsService
        ): Interface.IAuthenticationServiceFactory {

            var authenticationServiceBaseUrl = settingsService.Settings.AuthenticationServiceBaseUrl;

            function getAuthenticationService() : Interface.IAuthenticationService {
                var result: Interface.IAuthenticationService = _loginServiceDictionary[authenticationServiceBaseUrl];

                if (result == null) {
                    result = new AuthenticationService($http, $q, localStorageService, settingsService);
                    _loginServiceDictionary[authenticationServiceBaseUrl] = result;
                }

                return result;
            }

            var factory: Interface.IAuthenticationServiceFactory = {
                getAuthenticationService: getAuthenticationService
            }

            return factory;
        }


        breezeEntityManagerFactory.$inject = ["$q", "breeze"];

        var _entityManagerDictionary = {};

        export function breezeEntityManagerFactory($q: ng.IQService, breezeService ): Interface.IBreezeEntityManagerFactory {
            var title = "BamApps.Service.breezeEntityManagerFactory";

            function getEntityManager(hostName: string, servicePath: string): ng.IPromise<breeze.EntityManager> {
                var deferred = $q.defer<breeze.EntityManager>();

                var key = hostName + servicePath;
                var value = _entityManagerDictionary[key];
                if (value != null) {
                    Logger.info("Using existing entity manager.", title, value);
                    deferred.resolve(value);
                }
                else {
                    Logger.info("Creating new entity manager.", title, value);
                    var serviceRoot = window.location.protocol + '//' + hostName + '/';
                    var serviceName = serviceRoot + servicePath;
                    var entityManager = new breezeService.EntityManager(serviceName);
                    
                    _entityManagerDictionary[key] = entityManager;

                    entityManager.fetchMetadata()
                        .then((result) => {
                            Logger.info("Breeze Metadata successfully fetched.", title, result);
                            deferred.resolve(entityManager);
                        })
                        .catch((reason) => {
                            Logger.error("Breeze Metadata fetch failed!", title, reason);
                            deferred.reject(reason);
                        });
                }

                return deferred.promise;
            }

            var factory: Interface.IBreezeEntityManagerFactory = {
                getEntityManager: getEntityManager
            }


            return factory;
        }
    }
}