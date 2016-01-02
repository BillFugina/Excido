module BamApps {
    export module Excido {

        export class Configuration {
            static get Settings(): Interface.ISettings {
                var result: Interface.ISettings;

                if (BamApps.Excido.Config.Debug) {
                    result = new BamApps.Excido.Config.Debug.Settings();
                }
                else if (BamApps.Excido.Config.DevRelease) {
                    result = new BamApps.Excido.Config.DevRelease.Settings();
                }
                else {
                    result = new BamApps.Excido.Config.Release.Settings();
                }

                return result;
            }
        }

    }
}