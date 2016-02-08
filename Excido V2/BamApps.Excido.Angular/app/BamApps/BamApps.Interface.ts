module BamApps {
    export module Interface {

        export interface IStateChangeInspectorService {
            stateChangeInspector(event: angular.IAngularEvent, toState: angular.ui.IState, toStateParams, fromState: angular.ui.IState, fromParams, options: angular.ui.IStateOptions);
        }

        export interface IHelloWorldService {
            SayHello(): angular.IPromise<string>;
        }

        export interface IIdentity {
            roles: string[];
        }

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


        export interface IAuthenticationData {
            isAuth: boolean;
            userName: string;
        }

        export interface ILoginResponse {
            access_token: string;
            fullName: string;
            expires_in: number;
            refresh_token: string;
            ".issued" : Date;
            ".expires" : Date;
        }

        export interface IAuthenticationService {
            saveRegistration(registration): ng.IPromise<any>
            login(loginData: ILoginInfo): ng.IPromise<ILoginResponse>;
            logout(): void;
        }

        export interface IAuthenticationServiceFactory {
            getAuthenticationService(): IAuthenticationService;
        }


        export interface ILoginInfo {
            userName: string;
            password: string;
        }

        export interface IRegistrationInfo extends ILoginInfo {
            confirmPassword: string;
        }

        export interface ILoginController {
            Login(): void;
        }

        export interface IAuthenticationToken {
            userName: string;
            token: string;
        }

        export interface IAuthenticationInterceptorService {
            _request(config: ng.IRequestConfig): ng.IRequestConfig;
            _responseError(rejection: ng.IHttpPromiseCallbackArg<any>): ng.IPromise<any>;
        }
    }
}