module BamApps {
    export module Excido {
        export module Model {

            export class SharedContentUnit extends BamApps.Model.BreezeEntity implements Interface.Model.ISharedContentUnit {
                private _id: string;
                private _name: string;
                private _content: string;

                static source: string = "SharedContentUnits";


                get id(): string {
                    return this._id;
                }

                get name(): string {
                    return this._name;
                }

                set name(value : string) {
                    this._name = value;
                }

                get content(): string {
                    return this.content;
                }

                set content(value: string) {
                    this._content = value;
                }


            }

        }
    }
}