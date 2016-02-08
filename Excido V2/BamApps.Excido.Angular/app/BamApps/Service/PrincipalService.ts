module BamApps {
    export module Service {

        export class PrincipalService {
            static $inject: string[] = ['$q', '$http', '$timeout'];

            private _identity : Interface.IIdentity = undefined;
            private _authenticated = false;

            constructor(private $q: angular.IQService, private $http: angular.IHttpService, private $timeout: angular.ITimeoutService) {

            }

            get isIdentityResolved(): boolean {
                return angular.isDefined(this._identity);
            }

            get isAuthenticated(): boolean {
                return this._authenticated
            }

            isInRole(role : string): boolean {
                if (!this._authenticated || !this._identity.roles) {
                    return false;
                }

                return this._identity.roles.indexOf(role) != -1;
            }

            isInAnyRole(roles: string[]) {
                if (!this._authenticated || !this._identity.roles) return false;

                for (var i = 0; i < roles.length; i++) {
                    if (this.isInRole(roles[i])) {
                        return true;
                    }
                }

                return false;
            }

            authenticate() {
                var deferred = this.$q.defer();

                // check and see if we have retrieved the identity data from the server. if we have, reuse it by immediately resolving
                if (angular.isDefined(this._identity)) {
                    deferred.resolve(this._identity);
                    return deferred.promise;
                }


            }

        }

    }

}