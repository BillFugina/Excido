module BamApps {
    export module Excido {
        export module Model {

            export class Info {
                static SharedContentUnit = { Name: "SharedContentUnit", Source: "SharedContentUnits"};
            }

            export class SharedContentUnit extends BamApps.Model.BreezeEntity implements Interface.Model.ISharedContentUnit {
                id: string;
                name: string;
                content: string;

                isEditingName: boolean = false;
                isEditingContent: boolean = false;

                public editName() {
                    this.isEditingName = true;
                    this.isEditingContent = false;
                }

                public stopEditingName() {
                    this.isEditingName = false;
                }

                public editContent() {
                    this.isEditingContent = true;
                    this.isEditingName = false;
                }

                public stopEditingContent() {
                    this.isEditingContent = false;
                }
            }

        }
    }
}