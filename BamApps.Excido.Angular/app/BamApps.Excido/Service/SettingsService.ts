
module BamApps {
    export module Excido {
        export module Service {

            export class SettingsService implements BamApps.Excido.Interface.ISettingsService {
                private _settings: BamApps.Excido.Interface.ISettings;

                constructor() {
                    if (BamApps.Excido.Config.Debug) {
                        this._settings = new BamApps.Excido.Config.Debug.Settings();
                    }
                    else if (BamApps.Excido.Config.DevRelease) {
                        this._settings = new BamApps.Excido.Config.DevRelease.Settings();
                    }
                    else {
                        this._settings = new BamApps.Excido.Config.Release.Settings();
                    }
                }

                get Settings() {
                    return this._settings;
                }
            }

            export class SettingsServiceProvider implements ng.IServiceProvider {
                private _service: BamApps.Excido.Interface.ISettingsService;

                constructor() {
                }

                public $get(): BamApps.Excido.Interface.ISettingsService {
                    if (this._service == null) {
                        this._service = new SettingsService();
                    }
                    return this._service;
                }
            }

        }
    }
}