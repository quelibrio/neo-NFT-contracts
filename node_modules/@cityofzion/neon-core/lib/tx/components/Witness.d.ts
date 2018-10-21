import { StringStream } from "../../u";
import { Account } from "../../wallet";
export interface WitnessLike {
    invocationScript: string;
    verificationScript: string;
}
/**
 * A Witness is a section of VM code that is ran during the verification of the transaction.
 *
 * For example, the most common witness is the VM Script that pushes the ECDSA signature into the VM and calling CHECKSIG to prove the authority to spend the TransactionInputs in the transaction.
 */
export declare class Witness {
    static deserialize(hex: string): Witness;
    static fromStream(ss: StringStream): Witness;
    static fromSignature(sig: string, publicKey: string): Witness;
    /**
     * Builds a multi-sig Witness object.
     * @param tx Hexstring to be signed.
     * @param sigs Unordered list of signatures.
     * @param acctOrVerificationScript Account or verification script. Account needs to be the multi-sig account and not one of the public keys.
     */
    static buildMultiSig(tx: string, sigs: Array<string | Witness>, acctOrVerificationScript: Account | string): Witness;
    invocationScript: string;
    verificationScript: string;
    private _scriptHash;
    constructor(obj: WitnessLike);
    scriptHash: string;
    serialize(): string;
    export(): WitnessLike;
    equals(other: WitnessLike): boolean;
    private generateScriptHash;
}
export default Witness;
//# sourceMappingURL=Witness.d.ts.map