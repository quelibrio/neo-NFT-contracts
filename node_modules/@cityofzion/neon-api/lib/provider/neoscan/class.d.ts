import { u, wallet } from "@cityofzion/neon-core";
import { PastTransaction, Provider } from "../common";
export declare class Neoscan implements Provider {
    private url;
    readonly name: string;
    private rpc;
    private cacheExpiry;
    constructor(url: string);
    getRPCEndpoint(): Promise<string>;
    getBalance(address: string): Promise<wallet.Balance>;
    getClaims(address: string): Promise<wallet.Claims>;
    getMaxClaimAmount(address: string): Promise<u.Fixed8>;
    getHeight(): Promise<number>;
    getTransactionHistory(address: string): Promise<PastTransaction[]>;
}
export default Neoscan;
//# sourceMappingURL=class.d.ts.map