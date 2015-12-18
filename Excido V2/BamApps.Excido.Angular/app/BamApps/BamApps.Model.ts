module BamApps {
    export module Model {

        export class BamAppsBase implements Interface.ITitle {
            private _title: string;

            public get title(): string {
                var result = this._title || typeof this;
                return result;
            }

            public set title(value: string) {
                this._title = value;
            }
        }

        export class BreezeEntity implements breeze.Entity {
            entityAspect: breeze.EntityAspect;
            entityType: breeze.EntityType;
        }

    }
}