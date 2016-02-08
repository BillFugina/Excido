module BamApps {
    export module Service {
        export class StateChangeInspectorService implements Interface.IStateChangeInspectorService {
            static $inject: string[] = ['$urlRouter', 'helloWorldService'];

            constructor(private $urlRouter: angular.ui.IUrlRouterService, private helloWorldService: Interface.IHelloWorldService) {
                debugger;
            }

            stateChangeInspector(event: angular.IAngularEvent, toState: angular.ui.IState, toStateParams, fromState: angular.ui.IState, fromParams, options: angular.ui.IStateOptions) {
                debugger;
                event.preventDefault();

                this.helloWorldService.SayHello()
                    .then(result => {
                        BamApps.Logger.toast('helloWorldService', 'StateChangeInspectorService', result, toastr.success, 'Hello');
                        this.$urlRouter.sync();
                    })
                    .catch(reason => {
                        BamApps.Logger.error('helloWorldService', 'StateChangeInspectorService', reason, toastr.error, "Error");
                    });

            }
        }


        //StateChangeInspectorServiceFactory.$inject = ['$rootScope', '$urlRouter', 'helloWorldService'];
        //export function StateChangeInspectorServiceFactory($rootScope: angular.IRootScopeService, $urlRouter: angular.ui.IUrlRouterService, helloWorldService: Interface.IHelloWorldService) {
        //    debugger;
        //    var service = new StateChangeInspectorService($urlRouter, helloWorldService);
        //    $rootScope.$on('$stateChangeStart', service.stateChangeInspector);
        //    return service;
        //}
    }
}