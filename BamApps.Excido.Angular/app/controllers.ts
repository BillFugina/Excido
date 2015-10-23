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

        static $inject: string[] = ["$location"];

        constructor(private $location: ng.ILocationService) {
            this.activate();
        }

        activate() {
        }

    }

}