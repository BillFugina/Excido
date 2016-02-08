module BamApps {
    export module Excido {
        export module Service {
            var _sharedContentUnitService: Interface.ISharedContentUnitService = null;
            
            SharedContentUnitServiceFactory.$inject = ["$q", "entityManagerFactory"];
            export function SharedContentUnitServiceFactory($q: ng.IQService, entityManagerFactory: BamApps.Interface.IBreezeEntityManagerFactory): Interface.ISharedContentUnitServiceFactory {
                var title = "BamApps.Excido.Service.SharedContentUnitServiceFactory";

                function getSharedContentUnitService(): ng.IPromise<Interface.ISharedContentUnitService> {
                    var deferred = $q.defer<Interface.ISharedContentUnitService>();

                    if (_sharedContentUnitService != null) {
                        Logger.log("Using existing shared content unit service", title, _sharedContentUnitService);
                        deferred.resolve(_sharedContentUnitService);
                    }
                    else {
                        Logger.log("Create a new shared content unit service", title, _sharedContentUnitService);

                        var settings = Configuration.Settings;
                        entityManagerFactory.getEntityManager(settings.ApiServer, settings.ApiBreezeServicePath)
                            .then(em => {
                                Logger.log("Successfully accessed entity manager", title, em);
                                var entityManager = em;

                                var store = entityManager.metadataStore;
                                store.registerEntityTypeCtor(BamApps.Excido.Model.Info.SharedContentUnit.Name, BamApps.Excido.Model.SharedContentUnit, BamApps.Excido.Model.SharedContentUnit.initialize);

                                var sharedContentUnitService: Interface.ISharedContentUnitService = new SharedContentUnitService($q, entityManager);
                                _sharedContentUnitService = sharedContentUnitService;
                                deferred.resolve(sharedContentUnitService);
                            })
                            .catch(reason => {
                                Logger.error("Failed to access entity manager", title, reason);
                                deferred.reject(reason);
                            });

                    }
                    return deferred.promise;
                }

                var factory: Interface.ISharedContentUnitServiceFactory = {
                    getSharedContentUnitService : getSharedContentUnitService
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

                get hasChanges(): boolean {
                    var result = false;
                    if (this._entityManager != null) {
                        return this._entityManager.hasChanges();
                    }
                    return result;
                }

                getAll(): ng.IPromise<Interface.Model.ISharedContentUnit[]> {
                    var self = this;
                    var deferred = this.$q.defer<Interface.Model.ISharedContentUnit[]>();

                    var query = breeze.EntityQuery.from(Model.Info.SharedContentUnit.Source);
                    this._entityManager.executeQuery(query)
                        .then(data => {
                            Logger.log("Successfull getAll", this, data);
                            deferred.resolve(<Interface.Model.ISharedContentUnit[]>data.results);
                        })
                        .catch(reason => {
                            Logger.error("getAll failed", this, reason);
                            deferred.reject(reason);
                        });

                    return deferred.promise;
                }

                create(): Interface.Model.ISharedContentUnit {
                    var result = <Interface.Model.ISharedContentUnit>this._entityManager.createEntity(Model.Info.SharedContentUnit.Name, { Id: Utils.newUuid() });
                    this._entityManager.addEntity(result);
                    return result;
                }

                cancel() {
                    this._entityManager.rejectChanges();
                }

                save(unit: Interface.Model.ISharedContentUnit): ng.IPromise<Interface.Model.ISharedContentUnit> {
                    var deferred = this.$q.defer<Interface.Model.ISharedContentUnit>();

                    if (unit == null || unit.entityAspect.entityState == breeze.EntityState.Added || unit.entityAspect.entityState == breeze.EntityState.Modified) {
                        var breezePromise: breeze.promises.IPromise<breeze.SaveResult>;

                        if (unit != null) {
                            breezePromise = this._entityManager.saveChanges([unit])
                        }
                        else {
                            breezePromise = this._entityManager.saveChanges();
                        }

                        breezePromise
                            .then((value: breeze.SaveResult) => {
                                Logger.log("Successfull save", this, value);
                                deferred.resolve(<Interface.Model.ISharedContentUnit>value.entities[0]);
                            })
                            .catch(reason => {
                                Logger.error("save failed", this, reason);
                                deferred.reject(reason);
                            });
                    }
                    else {
                        deferred.resolve(unit);
                    }

                    return deferred.promise;
                }

                delete(unit: Interface.Model.ISharedContentUnit): ng.IPromise<void> {
                    var deferred = this.$q.defer<void>();
                    unit.entityAspect.setDeleted();
                    //this._entityManager.saveChanges([unit])
                    //    .then(() => {
                    //        Logger.log("Successfull delete", this);
                    //        deferred.resolve();
                    //    })
                    //    .catch(reason => {
                    //        Logger.error("delete failed", this, reason);
                    //        deferred.reject(reason);
                    //    });
                    deferred.resolve();
                    return deferred.promise;
                }

            }

        }
    }
}