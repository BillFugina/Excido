module BamApps {
    export module Excido {
        export module Service {

            export class WebApiService extends BamApps.Model.BamAppsBase implements Interface.IWebApiService {
                static $inject: string[] = ['$http', '$q', 'localStorageService', 'settingsService'];

                constructor(
                    private $http: ng.IHttpService,
                    private $q: ng.IQService,
                    private localStorageService: angular.local.storage.ILocalStorageService,
                    private settingsService: BamApps.Excido.Service.SettingsService
                ) {
                    super();
                }


                verify(): ng.IPromise<boolean> {
                    var self = this;
                    var excidoServiceBaseUrl = self.settingsService.Settings.ExcidoServiceBaseUrl;
                    var excidoServicePath = self.settingsService.Settings.ApiExcidoServicePath;
                    var apiUrl = excidoServiceBaseUrl + excidoServicePath;

                    var deferred = self.$q.defer<boolean>();

                    self.$http.get(apiUrl + '/verify')
                        .success((response: boolean) => {
                            deferred.resolve(response);
                        })
                        .error((err, status) => {
                            deferred.reject({ err: err, status: status });
                        });


                    return deferred.promise;
                }

            }

            export function WebApiServiceFactory($http: ng.IHttpService,
                $q: ng.IQService,
                localStorageService: angular.local.storage.ILocalStorageService,
                settingsService: BamApps.Excido.Service.SettingsService
            ): Interface.IWebApiService {
                var result = new WebApiService($http, $q, localStorageService, settingsService);
                return result;
            }

        }
    }
}