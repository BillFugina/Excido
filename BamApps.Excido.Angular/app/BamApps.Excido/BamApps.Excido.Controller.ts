module BamApps {
    export module Excido {
        export module Controller {

            export class SharedUnitsController extends BamApps.Model.BamAppsBase {
                static $inject: string[] = ["$q", "sharedContentUnitServiceFactory"];

                units: Interface.Model.ISharedContentUnit[];

                private _sharedContentUnitService: BamApps.Excido.Interface.ISharedContentUnitService;

                private _editingSharedContentUnit: Interface.Model.ISharedContentUnit = null;

                public ready: boolean = false;

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

                get hasChanges(): boolean {
                    var result = false;
                    if (this._sharedContentUnitService != null) {
                        result = this._sharedContentUnitService.hasChanges;
                    }
                    return result;
                }

                loadSharedContentUnits(): ng.IPromise<Interface.Model.ISharedContentUnit[]> {
                    var self = this;
                    var promise = this._sharedContentUnitService.getAll();

                    promise.then(results => {
                        self.units = results;
                        self.ready = true;
                    });

                    return promise;
                }

                public saveClick() {
                    Logger.log("save click", this);
                    this._sharedContentUnitService.save()
                        .then((r) => {
                            Logger.info("Changes Saved", this, r, toastr.success, "Success");
                        });
                }

                public addClick() {
                    Logger.log("add click", this);
                    var newUnit = this._sharedContentUnitService.create();
                    this.units.push(newUnit);
                    this._editingSharedContentUnit = newUnit;
                    newUnit.isEditingName = true;
                }

                public isEditing(unit: Interface.Model.ISharedContentUnit): boolean {
                    return unit === this._editingSharedContentUnit;
                }

                public deleteUnit(unit: Interface.Model.ISharedContentUnit) {
                    var index = this.units.indexOf(unit);
                    if (index >= 0) {
                        this.units.splice(index, 1);
                    }
                    this._sharedContentUnitService.delete(unit);
                }

                
            }

        }
    }
}