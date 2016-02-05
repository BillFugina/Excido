module BamApps {
    export module Excido {
        export module Controller {
            export class HomeController extends BamApps.Model.BamAppsBase implements BamApps.Excido.Interface.Controller.IHomeController {

                public datePickerOpen: boolean = false;
                public expirationDate: Date;
                public longLink: string;

                constructor(private $scope: ng.IScope) {
                    super();
                }

                public dateBtnClick() {
                    BamApps.Logger.log("dateBtnClick", this);
                    this.datePickerOpen = !this.datePickerOpen;
                }

                public dateResetBtnClick() {
                    BamApps.Logger.log("dateResetBtnClick", this);
                    this.expirationDate = null;
                }

                public longLinkValid() {
                    return this.longLink != null &&  (BamApps.Utils.isValidUrl(this.longLink) || BamApps.Utils.isValidUrl("http://" + this.longLink));
                }

                public longLinkInvalid() {
                    return this.longLink != null && !BamApps.Utils.isValidUrl(this.longLink) && !BamApps.Utils.isValidUrl("http://" + this.longLink);
                }

                public shrinkDisabled() {
                    return BamApps.Utils.isNullOrEmpty(this.longLink) || !this.longLinkValid();
                }

                public expirationInvalid() {
                    debugger;
                    var result = false;
                    if (this.expirationDate != null) {
                        var x = this.expirationDate;
                        var m = moment(x);
                        var result = !m.isValid();
                    }
                    return result;
                }

            }


        }
    }
}