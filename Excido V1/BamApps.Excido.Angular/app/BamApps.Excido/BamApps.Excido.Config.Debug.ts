module BamApps {
    export module Excido {
        export module Config {
            export module Debug {
                export class Settings implements BamApps.Excido.Interface.ISettings {
                    _ApiServer: string = 'localhost:53941';
                    _ApiServicePath: string = 'breeze/ExcidoBreeze';

                    get ApiServer() {
                        return this._ApiServer;
                    }

                    get ApiServicePath() {
                        return this._ApiServicePath;
                    }
                }
            }
        }
    }
}