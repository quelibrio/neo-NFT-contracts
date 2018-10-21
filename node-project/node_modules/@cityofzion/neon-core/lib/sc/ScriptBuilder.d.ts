import { StringStream } from "../u";
import OpCode from "./OpCode";
export interface ScriptIntent {
    scriptHash: string;
    operation?: string;
    args?: any[];
    useTailCall?: boolean;
}
/**
 * Builds a VM script in hexstring. Used for constructing smart contract method calls.
 * @extends StringStream
 */
export declare class ScriptBuilder extends StringStream {
    /**
     * Appends an Opcode, followed by args
     */
    emit(op: OpCode, args?: string): this;
    /**
     * Appends args, operation and scriptHash
     * @param scriptHash Hexstring(BE)
     * @param operation ASCII, defaults to null
     * @param args any
     * @param useTailCall Use TailCall instead of AppCall
     * @return this
     */
    emitAppCall(scriptHash: string, operation?: string | null, args?: any[] | string | number | boolean, useTailCall?: boolean): this;
    /**
     * Appends a SysCall
     * @param api api of SysCall
     * @return this
     */
    emitSysCall(api: string): this;
    /**
     * Appends data depending on type. Used to append params/array lengths.
     * @param data
     * @return this
     */
    emitPush(data?: any): this;
    /**
     * Reverse engineer a script back to its params.
     * A script may have multiple invocations so a list is always returned.
     * @return A list of ScriptIntents[].
     */
    toScriptParams(): ScriptIntent[];
    /**
     * Appends a AppCall and scriptHash. Used to end off a script.
     * @param scriptHash Hexstring(BE)
     * @param useTailCall Defaults to false
     */
    private _emitAppCall;
    /**
     * Private method to append an array
     * @private
     */
    private _emitArray;
    /**
     * Private method to append a hexstring.
     * @private
     * @param hexstring Hexstring(BE)
     * @return this
     */
    private _emitString;
    /**
     * Private method to append a number.
     * @private
     * @param num
     * @return this
     */
    private _emitNum;
    /**
     * Private method to append a ContractParam
     * @private
     */
    private _emitParam;
}
export default ScriptBuilder;
//# sourceMappingURL=ScriptBuilder.d.ts.map