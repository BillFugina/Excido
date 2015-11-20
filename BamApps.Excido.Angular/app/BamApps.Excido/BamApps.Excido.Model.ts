module BamApps {
    export module Excido {
        export module Model {

            export class Info {
                static SharedContentUnit = { Name: "SharedContentUnit", Source: "SharedContentUnits"};
            }

            export class SharedContentUnit extends BamApps.Model.BreezeEntity implements Interface.Model.ISharedContentUnit {
                Id: string;
                Name: string;
                Content: string;
                Slug: string;
                Created: Date;
                ExpireDate: Date;
                ExpireCount: number;

                constructor() {
                    super();
                }

                static initialize(item : SharedContentUnit) {
                    item.Created = new Date();
                    var newSlug = Utils.newId();
                    item.Slug = newSlug;
                }

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