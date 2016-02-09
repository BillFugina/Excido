module BamApps {
    export module Excido {
        export module Interface {

            export interface IMainController {
                userFullName: string;

                Logout();
                UserSettings();
            }

            export interface IWebApiService {
                verify(): ng.IPromise<boolean>;
            }

            export interface ISettings {
                ApiServer: string;
                ApiBreezeServicePath: string;
                ApiExcidoServicePath: string;
                ApiClientId: string;
                SlugPrefix: string;
                ExcidoServiceBaseUrl: string;
                AuthenticationServiceBaseUrl: string;

                //RouteProvider: ($routeProvider: angular.route.IRouteProvider) => void;
                uiRouteConfiguration: ($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider) => void;
            }

            export interface ISettingsService {
                Settings : BamApps.Excido.Interface.ISettings;
            }


            export interface ISharedContentUnitServiceFactory {
                getSharedContentUnitService: () => ng.IPromise<ISharedContentUnitService>;
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

            export module Controller {
                export interface IPreLoginController {
                    NameSpace : string;
                }

                export interface IHomeController {
                }
            }

        }
    }
}

declare module angular.ui {

    interface IState {
        protected?: boolean;
        redirectWhenAuthenticated?: string;
    }

}