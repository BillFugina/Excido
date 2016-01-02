module BamApps {
    export module Interface {

        export interface ITitle {
            title: string;
        }

        export interface IBreezeEntityManagerFactory {
            getEntityManager: (hostName : string, servicePath : string) => ng.IPromise<breeze.EntityManager>;
        }

        export interface IReadRepository<T> {
            getAll(): ng.IPromise<T[]>;
        }

        export interface IWriteRepository<T> {
            hasChanges: boolean;
            create(): T;
            save(T?): ng.IPromise<T>;
            delete(T): ng.IPromise<void>;
            cancel(): void;
        }

        export interface IRepository<T> extends IReadRepository<T>, IWriteRepository<T> {
        }

        export interface IBreezeEntity {
            source: string;
        }

        export interface ISyncFocusDirective extends ng.IDirective {
        }

        export interface IClipboardDirective extends ng.IDirective {
        }

        export interface ISyncFocusScope extends ng.IScope {
        }

        export interface ISyncFocusAttributes extends ng.IAttributes {
            syncFocusWith: string;
        }

        export interface IEnterKeyDirective extends ng.IDirective {
        }

        export interface IEnterKeyScope extends ng.IScope {
        }

        export interface IEnterKeyAttributes extends ng.IAttributes {
            onEnterKey: string;
        }

        export interface ITabKeyDirective extends ng.IDirective {
        }

        export interface ITabKeyScope extends ng.IScope {
        }

        export interface ITabKeyAttributes extends ng.IAttributes {
            onTabKey: string;
        }

        export interface IClipboardAttributes extends ng.IAttributes {
            clipboardText: string;
            clipboardSuccess: string;
            clipboardError: string;
        }
    }
}