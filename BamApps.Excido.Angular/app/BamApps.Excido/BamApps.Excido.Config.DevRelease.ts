﻿module BamApps {
    export module Excido {
        export module Config {

            export module DevRelease {
                export class Settings implements Interface.ISettings {
                    _ApiServer: string = 'api.dev.excido.net';
                    _ApiServicePath: string = 'breeze/ExcidoBreeze';
                    _SlugPrefix: string = 'http://exci.do/';

                    get ApiServer() {
                        return this._ApiServer;
                    }

                    get ApiServicePath() {
                        return this._ApiServicePath;
                    }

                    get SlugPrefix() {
                        return this._SlugPrefix;
                    }
                }
            }
        }
    }
}