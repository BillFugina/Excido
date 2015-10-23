var Controllers;
(function (Controllers) {
    "use strict";
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
    var StoreController = (function () {
        function StoreController($location) {
            this.$location = $location;
            this.title = "StoreController";
            this.products = gems;
            this.activate();
        }
        StoreController.prototype.activate = function () {
        };
        StoreController.$inject = ["$location"];
        return StoreController;
    })();
    Controllers.StoreController = StoreController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=controllers.js.map