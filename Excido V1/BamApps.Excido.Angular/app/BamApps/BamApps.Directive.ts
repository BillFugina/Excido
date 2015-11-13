module BamApps {
    export module Directive {

        SyncFocusDirective.$inject = ["$timeout", "$rootScope"];
        export function SyncFocusDirective($timeout: ng.ITimeoutService, $rootScope: ng.IRootScopeService): Interface.ISyncFocusDirective {
            return {
                restrict: "A",
                scope: {
                    focusValue: "=syncFocusWith"
                },
                link: link
            }

            function link(scope: Interface.ISyncFocusScope, element: ng.IAugmentedJQuery, attrs: Interface.ISyncFocusAttributes) {
                scope.$watch("focusValue", (currentValue, previousValue) => {
                    $timeout(() => {
                        if (currentValue === true) {
                            element[0].focus();
                        }
                        else if (currentValue === false && previousValue) {
                            element[0].blur();
                        }
                    }, 0, false);
                });
            }
        }

    }
}