import { StackItemLike } from "../sc";
import { Fixed8 } from "../u";
/**
 * RPC Response from test invokes
 */
export interface RPCVMResponse {
    script: string;
    state: "HALT, BREAK" | "FAULT, BREAK";
    gas_consumed: string;
    stack: StackItemLike[];
}
export declare type StackItemParser = (item: StackItemLike) => any;
export declare type VMResultParser = (result: RPCVMResponse) => any[];
/**
 * Builds a parser to parse the results of the stack.
 * @param args A list of functions to parse arguments. Each function is mapped to its corresponding StackItem in the result.
 * @returns parser function
 */
export declare function buildParser(...args: StackItemParser[]): VMResultParser;
/**
 * This just returns the value of the StackItem.
 */
export declare function NoOpParser(item: StackItemLike): any;
/**
 * Parses the result to an integer.
 */
export declare function IntegerParser(item: StackItemLike): number;
/**
 *  Parses the result to a ASCII string.
 */
export declare function StringParser(item: StackItemLike): string;
/**
 * Parses the result to a Fixed8.
 */
export declare function Fixed8Parser(item: StackItemLike): Fixed8;
/**
 * Parses the VM Stack and returns human readable strings. The types are inferred based on the StackItem type.
 * @param res RPC Response
 * @return Array of results
 */
export declare function SimpleParser(res: RPCVMResponse): any[];
//# sourceMappingURL=parse.d.ts.map