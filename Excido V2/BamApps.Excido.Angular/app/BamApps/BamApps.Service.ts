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