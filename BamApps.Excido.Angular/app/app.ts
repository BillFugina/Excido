module App {
    "use strict"
    export var app = angular.module('store', []);
    app.controller("StoreController", Controllers.StoreController);

    export interface Gem {
        name: string;
        price: number;
        description: string;
        canPurchase: boolean;
        soldOut?: boolean;
    }
}