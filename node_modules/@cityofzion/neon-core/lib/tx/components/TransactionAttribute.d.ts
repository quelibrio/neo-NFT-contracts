import { StringStream } from "../../u";
import { TxAttrUsage } from "../txAttrUsage";
export interface TransactionAttributeLike {
    usage: number | string;
    data: string;
}
/**
 * An attribute that is used to decorate the transaction.
 * Used for appending additional information to the transaction.
 *
 * For example, a remark is attached as an attribute.
 */
export declare class TransactionAttribute {
    static deserialize(hex: string): TransactionAttribute;
    static fromStream(ss: StringStream): TransactionAttribute;
    usage: TxAttrUsage;
    data: string;
    constructor(obj: TransactionAttributeLike);
    readonly [Symbol.toStringTag]: string;
    serialize(): string;
    export(): TransactionAttributeLike;
    equals(other: TransactionAttributeLike): boolean;
}
export default TransactionAttribute;
//# sourceMappingURL=TransactionAttribute.d.ts.map