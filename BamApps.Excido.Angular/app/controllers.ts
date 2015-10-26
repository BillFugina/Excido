module Controllers {
    "use strict";
    
    var gems: App.Gem[] = [
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

    interface IStoreController {
        title: string;
        activate: () => void;
    }

    export class StoreController implements IStoreController {
        title: string = "StoreController";

        products = gems;

        static $inject: string[] = ["$location", "entityManagerFactory"];

        constructor(private $location: ng.ILocationService, entityManagerFactory: Breeze.IEntityManagerFactory) {
            debugger;
            var entityManager = entityManagerFactory.newEntityManager();
            entityManager.fetchMetadata().then(() => {
                debugger;
                var query = breeze.EntityQuery.from('SharedContentUnits');
                entityManager.executeQuery(query).then(() => {
                    debugger;
                    this.activate();
                }).catch(() => {
                    debugger;
                });
            }).catch(() => {
                debugger;
            });
        }

        activate() {
        }

    }

}