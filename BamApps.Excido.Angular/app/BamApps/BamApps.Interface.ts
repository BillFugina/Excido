module BamApps {
    export module Interface {

        export interface ITitle {
            title: string;
        }

        export interface IBreezeEntityManagerFactory {
            newEntityManager: (hostName : string, servicePath : string) => ng.IPromise<breeze.EntityManager>;
        }

        export interface IReadRepository<T> {
            getAll(): ng.IPromise<T[]>;
        }

        export interface IWriteRepository<T> {
            hasChanges: boolean;
            create(): T;
            save(T?): ng.IPromise<T>;
            delete(T): ng.IPromise<void>;
        }

        export interface IRepository<T> extends IReadRepository<T>, IWriteRepository<T> {
        }

        export interface IBreezeEntity {
            source: string;
        }

        export interface ISyncFocusDirective extends ng.IDirective {
        }

        export interface ISyncFocusScope extends ng.IScope {
        }

        export interface ISyncFocusAttributes extends ng.IAttributes {
        }
    }
}