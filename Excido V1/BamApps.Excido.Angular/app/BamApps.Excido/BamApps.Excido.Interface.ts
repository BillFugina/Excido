module BamApps {
    export module Excido {
        export module Interface {

            export interface ISharedContentUnitServiceFactory {
                newSharedContentUnitService: () => ng.IPromise<ISharedContentUnitService>;
            }

            export interface ISharedContentUnitService extends BamApps.Interface.IRepository<Model.ISharedContentUnit> {
            }

            export module Model {
                export interface ISharedContentUnit extends breeze.Entity {
                    id: string;
                    name: string;
                    content: string;
                }
            }

        }
    }
}