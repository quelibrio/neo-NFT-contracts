import { BaseTransaction } from "../tx/transaction/BaseTransaction";
import Fixed8 from "../u/Fixed8";
import AssetBalance, { AssetBalanceLike } from "./components/AssetBalance";
export interface BalanceLike {
    address: string;
    net: string;
    assetSymbols: string[];
    assets: {
        [sym: string]: Partial<AssetBalanceLike>;
    };
    tokenSymbols: string[];
    tokens: {
        [sym: string]: number | string | Fixed8;
    };
}
/**
 * Represents a balance available for an Account.
 * Contains balances for both UTXO assets and NEP5 tokens.
 */
export declare class Balance {
    /** The address for this Balance */
    address: string;
    /** The network for this Balance */
    net: string;
    /** The symbols of assets found in this Balance. Use this symbol to find the corresponding key in the assets object. */
    assetSymbols: string[];
    /** The object containing the balances for each asset keyed by its symbol. */
    assets: {
        [sym: string]: AssetBalance;
    };
    /** The symbols of NEP5 tokens found in this Balance. Use this symbol to find the corresponding balance in the tokens object. */
    tokenSymbols: string[];
    /** The token balances in this Balance for each token keyed by its symbol. */
    tokens: {
        [sym: string]: Fixed8;
    };
    constructor(bal?: Partial<BalanceLike>);
    readonly [Symbol.toStringTag]: string;
    /**
     * Adds a new asset to this Balance.
     * @param  sym The symbol to refer by. This function will force it to upper-case.
     * @param assetBalance The assetBalance if initialized. Default is a zero balance object.
     */
    addAsset(sym: string, assetBalance?: Partial<AssetBalanceLike>): this;
    /**
     * Adds a new NEP-5 Token to this Balance.
     * @param sym - The NEP-5 Token Symbol to refer by.
     * @param tokenBalance - The amount of tokens this account holds.
     */
    addToken(sym: string, tokenBalance?: string | number | Fixed8): this;
    /**
     * Applies a Transaction to a Balance, removing spent coins and adding new coins. This currently applies only to Assets.
     * @param tx Transaction that has been sent and accepted by Node.
     * @param confirmed If confirmed, new coins will be added to unspent. Else, new coins will be added to unconfirmed property first.
     */
    applyTx(tx: BaseTransaction | string, confirmed?: boolean): Balance;
    /**
     * Informs the Balance that the next block is confirmed, thus moving all unconfirmed transaction to unspent.
     */
    confirm(): Balance;
    /**
     * Export this class as a plain JS object
     */
    export(): BalanceLike;
    /**
     * Verifies the coins in balance are unspent. This is an expensive call.
     *
     * Any coins categorised incorrectly are moved to their correct arrays.
     * @param url NEO Node to check against.
     */
    verifyAssets(url: string): Promise<this>;
}
export default Balance;
//# sourceMappingURL=Balance.d.ts.map