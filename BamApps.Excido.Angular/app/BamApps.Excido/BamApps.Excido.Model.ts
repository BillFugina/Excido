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

                get ExpireDateFormatted(): string {
                    var result = '';
                    var m = moment(this.ExpireDate);
                    if (m.isValid()) {
                        result = m.format('L');
                    }
                    return result;
                }

                set ExpireDateFormatted(value: string) {
                    var m = moment(value, 'L');
                    if (m.isValid()) {
                        this.ExpireDate = m.toDate();
                    }
                }

                isEditingName: boolean = false;
                isEditingContent: boolean = false;
                isEditingSlug: boolean = false;
                isEditingExpireDate: boolean = false;
                isEditingExpireCount: boolean = false;

                isPickingExpireDate: boolean = false;

                public editName() {
                    Logger.log("editName", "SharedContentUnit");
                    this.isEditingName = true;
                    this.isEditingContent = false;
                    this.isEditingSlug = false;
                    this.isEditingExpireDate = false;
                    this.isEditingExpireCount = false;
                    this.isPickingExpireDate = false;
                }

                public stopEditingName() {
                    this.isEditingName = false;
                }

                public editContent() {
                    Logger.log("editContent", "SharedContentUnit");
                    this.isEditingName = false;
                    this.isEditingContent = true;
                    this.isEditingSlug = false;
                    this.isEditingExpireDate = false;
                    this.isEditingExpireCount = false;
                    this.isPickingExpireDate = false;
                }

                public stopEditingContent() {
                    this.isEditingContent = false;
                }

                public editSlug() {
                    Logger.log("editSlug", "SharedContentUnit");
                    this.isEditingName = false;
                    this.isEditingContent = false;
                    this.isEditingSlug = true;
                    this.isEditingExpireDate = false;
                    this.isEditingExpireCount = false;
                    this.isPickingExpireDate = false;
                }

                public stopEditingSlug() {
                    this.isEditingSlug = false;
                }

                public editExpireDate() {
                    Logger.log("editExpireDate", "SharedContentUnit");
                    this.isEditingName = false;
                    this.isEditingContent = false;
                    this.isEditingSlug = false;
                    this.isEditingExpireDate = true;
                    this.isEditingExpireCount = false;
                    this.isPickingExpireDate = false;
                }

                public stopEditingExpireDate() {
                    this.isEditingExpireDate = false;
                }

                public editExpireCount() {
                    Logger.log("editExpireCount", "SharedContentUnit");
                    this.isEditingName = false;
                    this.isEditingContent = false;
                    this.isEditingSlug = false;
                    this.isEditingExpireDate = false;
                    this.isEditingExpireCount = true;
                    this.isPickingExpireDate = false;
                }

                public stopEditingExpireCount() {
                    this.isEditingExpireCount = false;
                }

                public pickExpireDate() {
                    Logger.log("pickExpireDate", "SharedContentUnit");
                    this.isEditingName = false;
                    this.isEditingContent = false;
                    this.isEditingSlug = false;
                    this.isEditingExpireDate = false;
                    this.isEditingExpireCount = false;
                    this.isPickingExpireDate = true;
                }

                public stopPickingExpireDate() {
                    this.isPickingExpireDate = false;
                }

            }

        }
    }
}