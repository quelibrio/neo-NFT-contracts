import { StringStream } from "../../u";
export interface TransactionInputLike {
    prevHash: string;
    prevIndex: number;
}
/**
 * A reference to a TransactionOutput in another confirmed transaction.
 * This is used to denote UTXO that will be spent in this transaction.
 */
export declare class TransactionInput {
    static deserialize(hex: string): TransactionInput;
    static fromStream(ss: StringStream): TransactionInput;
    prevHash: string;
    prevIndex: number;
    constructor(obj: TransactionInputLike);
    serialize(): string;
    export(): TransactionInputLike;
    equals(other: TransactionInputLike): boolean;
}
export default TransactionInput;
//# sourceMappingURL=TransactionInput.d.ts.map