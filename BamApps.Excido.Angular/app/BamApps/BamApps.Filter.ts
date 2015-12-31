module BamApps {
    export module Filter {

        export function CollapseFilter() {

            return function (input: string, left: number = 12, middle: string = '•••', right: number = 10) {
                var result = input;
                if (input.length > (left + middle.length + right)) {
                    result = input.substr(0, left) + middle + input.substr(input.length - right);
                }
                return result;
            }

        }

    }
}