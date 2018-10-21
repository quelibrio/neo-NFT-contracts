import { u, wallet } from "@cityofzion/neon-core";
import { PastTransaction, Provider } from "../common";
export declare class NeonDB implements Provider {
    private url;
    readonly name: string;
    private rpc;
    private cacheExpiry;
    constructor(url: string);
    getRPCEndpoint(noCache?: boolean): Promise<string>;
    getBalance(address: string): Promise<wallet.Balance>;
    getClaims(address: string): Promise<wallet.Claims>;
    getMaxClaimAmount(address: string): Promise<u.Fixed8>;
    getHeight(): Promise<number>;
    getTransactionHistory(address: string): Promise<PastTransaction[]>;
}
export default NeonDB;
//# sourceMappingURL=class.d.ts.map