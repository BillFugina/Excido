module BamApps {
    export module Service {

        breezeEntityManagerFactory.$inject = ["$q", "breeze"];

        var _entityManagerDictionary = {};

        export function breezeEntityManagerFactory($q : ng.IQService ): Interface.IBreezeEntityManagerFactory {
            var title = "BamApps.Service.breezeEntityManagerFactory";

            function getEntityManager(hostName: string, servicePath: string): ng.IPromise<breeze.EntityManager> {
                var deferred = $q.defer <breeze.EntityManager>();

                var key = hostName + servicePath;
                var value = _entityManagerDictionary[key];
                if ( value != null) {
                    Logger.info("Using existing entity manager.", title, value);
                    deferred.resolve(value);
                }
                else {
                    Logger.info("Creating new entity manager.", title, value);
                    var serviceRoot = window.location.protocol + '//' + hostName + '/';
                    var serviceName = serviceRoot + servicePath;
                    var entityManager = new breeze.EntityManager(serviceName);
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