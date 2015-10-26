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
        function StoreController($location, entityManagerFactory) {
            var _this = this;
            this.$location = $location;
            this.title = "StoreController";
            this.products = gems;
            debugger;
            var entityManager = entityManagerFactory.newEntityManager();
            entityManager.fetchMetadata().then(function () {
                debugger;
                var query = breeze.EntityQuery.from('SharedContentUnits');
                entityManager.executeQuery(query).then(function () {
                    debugger;
                    _this.activate();
                }).catch(function () {
                    debugger;
                });
            }).catch(function () {
                debugger;
            });
        }
        StoreController.prototype.activate = function () {
        };
        StoreController.$inject = ["$location", "entityManagerFactory"];
        return StoreController;
    })();
    Controllers.StoreController = StoreController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=controllers.js.map