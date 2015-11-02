module BamApps {
    export module Interface {

        export interface ITitle {
            title: string;
        }

        export interface IBreezeEntityManagerFactory {
            newEntityManager: (hostName : string, servicePath : string) => ng.IPromise<breeze.EntityManager>;
        }

        export interface IRepository<T> {
            getAll(): ng.IPromise<T[]>;
        }

        export interface IBreezeEntity {
            source: string;
        }
    }
}