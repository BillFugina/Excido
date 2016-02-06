module BamApps {
    export module Excido {
        export module Controller {
            export class MainAppController extends BamApps.Model.BamAppsBase implements BamApps.Excido.Interface.Controller.IMainAppController {

                constructor(private $scope : ng.IScope , private settingsService: BamApps.Excido.Interface.ISettingsService, private $state : angular.ui.IStateService) {
                    super();
                }

                public get NameSpace (): string {
                    return this.settingsService.Settings.SlugPrefix;
                }

                public goHome(): void {
                    this.$state.go('home')
                }

            }


        }
    }
}