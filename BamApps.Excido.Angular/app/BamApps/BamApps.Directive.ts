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

        export function OnEnterKeyDirective($timeout: ng.ITimeoutService, $rootScope: ng.IRootScopeService): Interface.IEnterKeyDirective {
            return {
                restrict: "A",
                link: link
            }

            function link(scope: Interface.ISyncFocusScope, element: ng.IAugmentedJQuery, attrs: Interface.IEnterKeyAttributes) {
                element.bind("keydown keypress", function (event) {
                    var keyCode = event.which || event.keyCode;

                    // If enter key is pressed
                    if (keyCode === 13) {
                        scope.$apply(function () {
                            // Evaluate the expression
                            scope.$eval(attrs.onEnterKey);
                        });

                        event.preventDefault();
                    }
                });
            }
        }

        export function OnTabKeyDirective($timeout: ng.ITimeoutService, $rootScope: ng.IRootScopeService): Interface.ITabKeyDirective {
            return {
                restrict: "A",
                link: link
            }

            function link(scope: Interface.ISyncFocusScope, element: ng.IAugmentedJQuery, attrs: Interface.ITabKeyAttributes) {
                element.bind("keydown keypress", function (event) {
                    var keyCode = event.which || event.keyCode;

                    // If tab key is pressed
                    if (keyCode === 9) {
                        scope.$apply(function () {
                            // Evaluate the expression
                            scope.$eval(attrs.onTabKey);
                        });

                        event.preventDefault();
                    }
                });
            }
        }

    }
}