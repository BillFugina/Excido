module BamApps {
    /**
     * Abstraction of the methods used to send messages to the JavaScript console 
     * with optional toast messages using [Toastr](http://codeseven.github.io/toastr/).
     * 
     * The verbosity level can be set to globally change the amount of information sent to the console.
     * 
     * Each level of verbosity includes all of the messages of the previous level.
     * 1. Toast - Only log messages that appear on the screen.
     * 2. Errors - Log toast messages and error messages.
     * 3. Warnings - Log toast, error and warning messages.
     * 4. Info - Log toast, error, warning and info messages.
     * 5. Debug - Log toast, error, warning, info and debug messages.
     * 6. Log - Log everything.
     * 
     * 
     * **Examples**
     * ```
     * Logger.log("My message", "tag", arguments);
     * Logger.info("My message", "tag", arguments);
     * Logger.error("My message", "tag", arguments);
     * ```
     * **Prerequisites**
     * 1. [Toastr](http://codeseven.github.io/toastr/) 
     * 
     * 
     */
    export module Logger {

        /**
         * Options for the level of verbosity to show in the console log.
         * 
         * Each level of verbosity includes all of the messages of the previous level.
         * 1. Toast - Only log messages that appear on the screen.
         * 2. Errors - Log toast messages and error messages.
         * 3. Warnings - Log toast, error and warning messages.
         * 4. Info - Log toast, error, warning and info messages.
         * 5. Debug - Log toast, error, warning, info and debug messages.
         * 6. Log - Log everything.
         */
        export enum Level {
            /**
             * Only log message that appear on the screen.
             */
            Toast,      

            /**
             * Log toast messages and any errors.
             */

            Errors,     
            /**
             * Log Toast, Errors and Warnings.
             */
            Warnings,

            /**
             * Log Toast, Errors, Warnings and Info.
             */
            Info,

            /**
             * Log Toast, Errors, Warnings, Info and Debug.
             */
            Debug,

            /**
             * Log everything possible.
             */
            Log
        };

        /**
         * 'private' variable that stores the current verbosity level.
         */
        var _verbosity: Level = Level.Errors;

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": 300,
            "hideDuration": 1000,
            "timeOut": 5000,
            "extendedTimeOut": 1000,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        /**
         * Get or set the level of verbosity.
         * @param level If specified the verbosity will be set to this level.
         * @returns The current verbosity level.
         */
        export function verbosity(value?: Level | Number): Level {
            if (value === undefined || value === null) {
                return _verbosity;
            }

            var level: Level;
            if (typeof value !== 'number' || value < Level.Toast || value > Level.Log) {
                level = Level.Errors;
            }
            else {
                level = <Level>value;
            }
            if (level != null) {
                _verbosity = level;
            }
            return _verbosity;
        }

        function getSourceString(source: any): string {
            var sourceString = "";
            if (source != null) {
                if (typeof source === "string") {
                    sourceString = source;
                }
                else if (BamApps.Utils.hasMember(source, 'title') && typeof source.title === "string") {
                    sourceString = source.title;
                }
                else if (source.hasOwnProperty('title')) {
                    sourceString = source.title;
                }
                else if (BamApps.Utils.hasFunction(source, 'title')) {
                    sourceString = source.title();
                }
                else {
                    sourceString = typeof source;
                }
            }
            return sourceString;
        }

        function getToastrDisplayMethod(showToast?: boolean | ToastrDisplayMethod, defaultMethod?: ToastrDisplayMethod): ToastrDisplayMethod {
            showToast = showToast || false;
            defaultMethod = defaultMethod || toastr.info;
            var typeofShowToast = typeof showToast;
            var toastMethod: ToastrDisplayMethod = null;

            if (typeofShowToast == 'boolean') {
                toastMethod = showToast ? toastr.error : null;
            }
            else if (typeofShowToast == 'function') {
                toastMethod = <ToastrDisplayMethod>showToast;
            }
            return toastMethod;
        }

        /**
         * Calls the javascript `console.error()` method if the [verbosity](#verbosity) is set to [error](#error) or higher.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param showToast If set to true, a [Toastr](http://codeseven.github.io/toastr/) pop-up will be shown in the error style.
         */
        export function error(message: string, source?: BamApps.Interface.ITitle, data?: any, showToast?: boolean | ToastrDisplayMethod, title? : string);
        export function error(message: string, source?: string, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function error(message: string, source?: any, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            _processMessage(message, sourceString, Level.Errors, console.error, data, getToastrDisplayMethod(showToast, toastr.error), title);
        }



        /**
         * Calls the javascript `console.warning()` method if the [verbosity](#verbosity) is set to [warning](#warning) or higher.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param showToast If set to true, a [Toastr](http://codeseven.github.io/toastr/) pop-up will be shown in the warning style.
         */
        export function warning(message: string, source?: BamApps.Interface.ITitle, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function warning(message: string, source?: string, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function warning(message: string, source?: any, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            _processMessage(message, sourceString, Level.Warnings, console.warn, data, getToastrDisplayMethod(showToast, toastr.warning), title);
        }

        /**
         * Calls the javascript `console.info()` method if the [verbosity](#verbosity) is set to [info](#info) or higher.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param showToast If set to true, a [Toastr](http://codeseven.github.io/toastr/) pop-up will be shown in the info style.
         */
        export function info(message: string, source?: BamApps.Interface.ITitle, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function info(message: string, source?: string, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function info(message: string, source?: any, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            _processMessage(message, sourceString, Level.Info, console.info, data, getToastrDisplayMethod(showToast, toastr.info), title);
        }

        /**
         * Calls the javascript `console.log()` method if the [verbosity](#verbosity) is set to [log](#log) or higher.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param showToast If set to true, a [Toastr](http://codeseven.github.io/toastr/) pop-up will be shown in the info style.
         */
        export function debug(message: string, source?: BamApps.Interface.ITitle, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function debug(message: string, source?: string, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function debug(message: string, source?: any, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            _processMessage(message, sourceString, Level.Log, console.debug, data, getToastrDisplayMethod(showToast, toastr.info), title);
        }

        /**
         * Calls the javascript `console.log()` method if the [verbosity](#verbosity) is set to [log](#log) or higher.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param showToast If set to true, a [Toastr](http://codeseven.github.io/toastr/) pop-up will be shown in the info style.
         */
        export function log(message: string, source?: BamApps.Interface.ITitle, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function log(message: string, source?: string, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string);
        export function log(message: string, source?: any, data?: any, showToast?: boolean | ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            _processMessage(message, sourceString, Level.Log, console.log, data, getToastrDisplayMethod(showToast, toastr.info), title);
        }

        /**
         * Calls the javascript `console.log()` method and shows a toast pop-up using the specified ToastrDisplayMethod.
         * Note that toast messages are always sent to the console.
         * @param message The message to send to the console.
         * @param source A tag that will be prepended to the message to aid in filtering messages on the console.
         * @param data Any object
         * @param toastrDisplayMethod The [Toastr](http://codeseven.github.io/toastr/) to be used to show the toast. If not specified, 'toastr.info' will be used.
         */
        export function toast(message: string, source?: BamApps.Interface.ITitle, data?: any, toastrDisplayMethod?: ToastrDisplayMethod, title?: string);
        export function toast(message: string, source?: string, data?: any, toastrDisplayMethod?: ToastrDisplayMethod, title?: string);
        export function toast(message: string, source?: any, data?: any, toastrDisplayMethod?: ToastrDisplayMethod, title?: string) {
            var sourceString = getSourceString(source);
            if (toastrDisplayMethod == null) {
                toastrDisplayMethod = toastr.info;
            }
            _processMessage(message, sourceString, Level.Toast, console.log, data, toastrDisplayMethod, title);
        }

        /**
         * A 'private' method used by the module to send messages to the console.
         */
        function _processMessage(message: string, source: string, threshold: Level, consoleMethod: (message?: any, ...optionalParams: any[]) => void, data?, toast?: ToastrDisplayMethod, title?: string) {
            source = source ? '[' + source + '] ' : '';
            var consoleMessage = source + '\t' + message;
            if (toast || _verbosity >= threshold) {
                if (typeof consoleMethod.call == 'function') {
                    if (data) {
                        consoleMethod.call(console, consoleMessage, data);
                    } else {
                        consoleMethod.call(console, consoleMessage);
                    }

                    if (toast) {
                        if (title) {
                            toast(message, title);
                        }
                        else {
                            toast(message);
                        }
                    }
                }
            }

        }
    }
}