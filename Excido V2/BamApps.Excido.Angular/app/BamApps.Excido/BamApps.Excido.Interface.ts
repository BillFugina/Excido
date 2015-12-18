module BamApps {
    export module Excido {
        export module Interface {

            export interface ISettings {
                ApiServer: string;
                ApiServicePath: string;
                SlugPrefix: string;
            }


            export interface ISharedContentUnitServiceFactory {
                newSharedContentUnitService: () => ng.IPromise<ISharedContentUnitService>;
            }

            export interface ISharedContentUnitService extends BamApps.Interface.IRepository<Model.ISharedContentUnit> {
            }

            export module Model {
                export interface ISharedContentUnit extends breeze.Entity {
                    Id: string;
                    Name: string;
                    Content: string;
                    Slug: string;
                    Created: Date;
                    ExpireDate: Date;
                    ExpireCount: number;

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