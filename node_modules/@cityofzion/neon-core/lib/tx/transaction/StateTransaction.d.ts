import { StringStream } from "../../u";
import { StateDescriptor, StateDescriptorLike } from "../components";
import { BaseTransaction, TransactionLike } from "./BaseTransaction";
import TransactionType from "./TransactionType";
export interface StateTransactionLike extends TransactionLike {
    descriptors: StateDescriptorLike[];
}
/**
 * Transaction used for invoking smart contracts through a VM script.
 * Can also be used to transfer UTXO assets.
 */
export declare class StateTransaction extends BaseTransaction {
    static deserializeExclusive(ss: StringStream, tx: Partial<TransactionLike>): Partial<StateTransactionLike>;
    readonly type: TransactionType;
    descriptors: StateDescriptor[];
    readonly exclusiveData: {
        descriptors: StateDescriptor[];
    };
    readonly fees: number;
    constructor(obj?: Partial<StateTransactionLike>);
    serializeExclusive(): string;
    equals(other: Partial<TransactionLike>): boolean;
    export(): StateTransactionLike;
}
export default StateTransaction;
//# sourceMappingURL=StateTransaction.d.ts.map