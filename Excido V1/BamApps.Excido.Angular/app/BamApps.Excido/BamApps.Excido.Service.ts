module BamApps {
    export module Excido {
        export module Service {
            
            SharedContentUnitServiceFactory.$inject = ["$q", "entityManagerFactory"];
            export function SharedContentUnitServiceFactory($q: ng.IQService, entityManagerFactory: BamApps.Interface.IBreezeEntityManagerFactory): Interface.ISharedContentUnitServiceFactory {
                var title = "BamApps.Excido.Service.SharedContentUnitServiceFactory";

                function newSharedContentUnitService(): ng.IPromise<Interface.ISharedContentUnitService> {
                    var deferred = $q.defer<Interface.ISharedContentUnitService>();

                    entityManagerFactory.newEntityManager('localhost:53941', 'breeze/ExcidoBreeze')
                        .then(em => {
                            Logger.log("Successfully created new entity manager", title, em);
                            var entityManager = em;
                            var sharedContentUnitService: Interface.ISharedContentUnitService = new SharedContentUnitService($q, entityManager);
                            deferred.resolve(sharedContentUnitService);
                        })
                        .catch(reason => {
                            Logger.error("Failed to create new entity manager", title, reason);
                            deferred.reject(reason);
                        });
                
                    return deferred.promise;
                }

                var factory: Interface.ISharedContentUnitServiceFactory = {
                    newSharedContentUnitService : newSharedContentUnitService
                }


                return factory;
            }

            export class SharedContentUnitService extends BamApps.Model.BamAppsBase implements Interface.ISharedContentUnitService {
                static $inject: string[] = ["$q", "entityManager"];

                constructor(private $q: ng.IQService, private _entityManager: breeze.EntityManager) {
                    super();
                    this.title = "BamApps.Excido.Service.SharedContentUnitService";
                    var self = this;
                }

                getAll(): ng.IPromise<Interface.Model.ISharedContentUnit[]> {
                    var self = this;
                    var deferred = this.$q.defer<Interface.Model.ISharedContentUnit[]>();

                    var query = breeze.EntityQuery.from(Model.SharedContentUnit.source);
                    this._entityManager.executeQuery(query)
                        .then(data => {
                            Logger.log("Successfull getAll", this, data);
                            deferred.resolve(<Interface.Model.ISharedContentUnit[]>data.results);
                        })
                        .catch(reason => {
                            Logger.error("getAll failed", this, reason);
                        });

                    return deferred.promise;
                }


            }

        }
    }
}