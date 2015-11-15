module BamApps {
    export module Excido {
        export module Interface {

            export interface ISettings {
                ApiServer: string;
                ApiServicePath: string;
            }


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
                    isEditingName: boolean;
                    isEditingContent: boolean;
                    editName() : void;
                    editContent() : void;
                    stopEditingName() : void;
                    stopEditingContent() : void;
                }
            }

        }
    }
}