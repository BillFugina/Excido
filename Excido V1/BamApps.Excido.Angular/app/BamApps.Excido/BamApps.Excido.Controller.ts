module BamApps {
    export module Excido {
        export module Controller {

            var gems = [
                {
                    name: 'Dodecahedron',
                    price: 2,
                    description: '. . .',
                    canPurchase: true,
                    soldOut: false
                },
                {
                    name: 'Pentagonal Gem',
                    description: '. . . ',
                    price: 5.95,
                    canPurchase: true
                }
            ];

            export class SharedUnitsController extends BamApps.Model.BamAppsBase {
                static $inject: string[] = ["$q", "sharedContentUnitServiceFactory"];

                products = gems;
                units: Interface.Model.ISharedContentUnit[];

                private _sharedContentUnitService: BamApps.Excido.Interface.ISharedContentUnitService;

                constructor(private $q: ng.IQService, private _sharedContentUnitServiceFactory: BamApps.Excido.Interface.ISharedContentUnitServiceFactory) {
                    super();
                    this.title = "BamApps.Excido.Controller.SharedUnitsController";
                    var self = this;

                    _sharedContentUnitServiceFactory.newSharedContentUnitService()
                        .then((service) => {
                            Logger.log("Successfully created SharedContentUnitService", self, service);
                            self._sharedContentUnitService = service;

                            self.loadSharedContentUnits();
                        })
                        .catch((reason) => {
                            Logger.error("Failed to create SharedContentUnitService", self, reason);
                        });

                }

                loadSharedContentUnits(): ng.IPromise<Interface.Model.ISharedContentUnit[]> {
                    var self = this;
                    var promise = this._sharedContentUnitService.getAll();

                    promise.then(results => {
                        debugger;
                        self.units = results;
                    });

                    return promise;
                }


            }

        }
    }
}