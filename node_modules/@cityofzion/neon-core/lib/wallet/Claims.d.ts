import ClaimItem, { ClaimItemLike } from "./components/ClaimItem";
export interface ClaimsLike {
    address: string;
    net: string;
    claims: ClaimItemLike[];
}
/**
 * Claims object used for claiming GAS.
 */
export declare class Claims {
    /** The address for this Claims */
    address: string;
    /** Network which this Claims is using */
    net: string;
    /** The list of claimable transactions */
    claims: ClaimItem[];
    constructor(config?: Partial<ClaimsLike>);
    readonly [Symbol.toStringTag]: string;
    export(): {
        address: string;
        net: string;
        claims: ClaimItemLike[];
    };
    /**
     * Returns new Claims object that contains part of the total claims starting at start, ending at end.
     */
    slice(start: number, end?: number): Claims;
}
export default Claims;
//# sourceMappingURL=Claims.d.ts.map