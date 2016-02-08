module BamApps {
    export module Service {

        export class HelloWorldService implements Interface.IHelloWorldService {
            static $inject: string[] = ['$q', '$http', 'settingsService'];

            constructor(
                private $q: angular.IQService,
                private $http: angular.IHttpService,
                private settingsService: BamApps.Excido.Service.SettingsService
            ) {
            }

            SayHello(): angular.IPromise<string> {
                var deferred = this.$q.defer<string>();

                var self = this;
                var authenticationServiceBaseUrl = self.settingsService.Settings.AuthenticationServiceBaseUrl;

                self.$http.get(authenticationServiceBaseUrl + 'api/accounts/hello')
                    .success((response : string) => {
                        deferred.resolve(response);
                    })
                    .error((err, status) => {
                        deferred.reject(err);
                    });

                var result = deferred.promise;
                return result;
            }

        }

        HelloWorldServiceFactory.$inject = ['$q', '$http', 'settingsService'];
        export function HelloWorldServiceFactory($q: angular.IQService, $http: angular.IHttpService, settingsService: BamApps.Excido.Service.SettingsService) {
            return new HelloWorldService($q, $http, settingsService);
        }

    }
}