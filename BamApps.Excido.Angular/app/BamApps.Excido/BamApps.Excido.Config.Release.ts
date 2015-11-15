﻿module BamApps {
    export module Excido {
        export module Config {

            export function Settings(): Interface.ISettings {
                var result: Interface.ISettings;

                if (BamApps.Excido.Config.Debug) {
                    result = new BamApps.Excido.Config.Debug.Settings();
                }
                else {
                    result = new BamApps.Excido.Config.Release.Settings();
                }

                return result;
            }

            export module Release {
                export class Settings implements Interface.ISettings {
                    _ApiServer: string = 'excidowebapi.azurewebsites.net';
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
