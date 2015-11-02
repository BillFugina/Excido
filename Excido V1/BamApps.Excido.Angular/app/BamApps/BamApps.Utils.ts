module BamApps {
    /**
 * A collection of useful utility functions
 */
    export module Utils {
        /**
         * Find the first element in an array that meets the supplied requirements.
         * @param a The array in which to search for an item.
         * @param func A function that returns a boolean indicating whether or not a given item satisfies the requirements.
         * @param T The type of the items that are in the array.
         * @returns The first item in the array that meets the supplied requirements.
         */
        export function arrayFirst<T>(a: T[], func: (item: T) => boolean): T {
            var result: T = null;

            if (a != null) {
                var index = 0;
                while (result == null && index < a.length) {
                    var anItem = a[index++];
                    if (func(anItem) == true) {
                        result = anItem;
                    }
                }
            }

            return result;
        }

        /**
         * Find the index of the first element in an array that meets the supplied requirements.
         * @param a The array in which to search for an item.
         * @param func A function that returns a boolean indicating whether or not a given item satisfies the requirements.
         * @param T The type of the items that are in the array.
         * @returns The index of the first item in the array that meets the supplied requirements.
         */
        export function arrayIndexOf<T>(a: T[], func: (item: T) => boolean): number {
            var result: number = null;

            if (a != null) {
                var index = 0;
                while (result == null && index < a.length) {
                    var anItem = a[index];
                    if (func(anItem) == true) {
                        result = index;
                    }
                    else {
                        index++;
                    }
                }
            }

            return result;
        }

        /**
         * Tell if a string or number is null or empty.
         * @param s The string or number to check for null or empty.
         * @returns true if the given is a string and it is null or empty 
         * or if the given is a number and it is NaN or Infinite    
         */
        export function isNullOrEmpty(s: string | number): boolean {
            var result = s === null;
            if (typeof s === 'string') {
                result = s == null || s === '';
            }
            else if (typeof s === 'number') {
                result = isNaN(s) || !isFinite(s);
            }
            return result;
        }

        /**
         * Tell is a string is a representation of a number.
         * @param s
         */
        export function isNumber(s: string) {
            return parseInt(s) !== NaN;
        }

        /**
         * Convert any value into a boolean based on its truthyness.
         * Strings are truthy if they are 'true', '1' or 'yes'
         * Booleans are truthy if they are true.
         * Numbers are truthy if they are greater than 0.
         * Functions are truthy if they return a truthy value.
         * Objects are truthy if they are not null or undefined.
         * @param x The value to convert into a boolean.
         * @returns true if the given variable is truthy otherwise false.
         */
        export function truthy(x): boolean {
            var result = false;
            var t = typeof x;
            switch (t) {
                case 'string':
                    var xl = x.toLowerCase();
                    result = xl === 'true'
                    || xl === '1'
                    || xl === 'yes'
                    || xl === 'y'
                    ;
                    break;
                case 'boolean':
                    result = x === true;
                    break;
                case 'number':
                    result = x > 0;
                    break;
                case 'function':
                    result = truthy(x());
                    break;
                default:
                    result = x != null;
            }

            return result;
        }

        /**
         * Creates a string that can be used as a unique identifier.
         * @returns a string that is exceedingly likely to be unique
         */
        export function newId(): string {
            return '_' + Math.random().toString(36).substr(2, 9);
        }

        /**
         * Tells if a given object is a string.
         * @param obj the object to check if it is a string.
         * @returns true if the object is a string.
         */
        export function isString(obj: any): boolean {
            return obj && typeof obj === "string";
        }

        /**
         * Tells if a given object has a function.
         * @param obj The object to check for a function.
         * @param functionName The name of the function to look for.
         * @returns true if the given object has a function with the given name.
         */
        export function hasFunction(obj : any, functionName: string): boolean {
            return obj && obj[functionName] && typeof obj[functionName] === "function";
        }

        export function hasMember(obj: any, memberName: string): boolean {
            return obj && obj[memberName];
        }
    }
}