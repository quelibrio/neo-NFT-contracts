import { NeonObject } from "../../helper";
import Fixed8 from "../../u/Fixed8";
import Coin, { CoinLike } from "./Coin";
export interface AssetBalanceLike {
    balance: Fixed8 | number | string;
    unspent: CoinLike[];
    spent: CoinLike[];
    unconfirmed: CoinLike[];
}
/**
 * Balance of an UTXO asset.
 * We keep track of 3 states: unspent, spent and unconfirmed.
 * Unspent coins are ready to be constructed into transactions.
 * Spent coins have been used once in confirmed transactions and cannot be used anymore. They are kept here for tracking purposes.
 * Unconfirmed coins have been used in transactions but are not confirmed yet. This is a holding state until we confirm that the transactions are mined into blocks.
 */
export declare class AssetBalance implements NeonObject {
    unspent: Coin[];
    spent: Coin[];
    unconfirmed: Coin[];
    constructor(abLike?: Partial<AssetBalanceLike>);
    readonly balance: Fixed8;
    export(): AssetBalanceLike;
    equals(other: Partial<AssetBalanceLike>): boolean;
}
export default AssetBalance;
//# sourceMappingURL=AssetBalance.d.ts.map