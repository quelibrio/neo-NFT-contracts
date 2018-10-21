import { u, wallet } from "@cityofzion/neon-core";
import { PastTransaction, Provider } from "./common";
export default class ApiBalancer implements Provider {
    leftProvider: Provider;
    rightProvider: Provider;
    readonly name: string;
    private _preference;
    preference: number;
    private _frozen;
    frozen: boolean;
    constructor(leftProvider: Provider, rightProvider: Provider, preference?: number, frozen?: boolean);
    getRPCEndpoint(): Promise<string>;
    getBalance(address: string): Promise<wallet.Balance>;
    getClaims(address: string): Promise<wallet.Claims>;
    getMaxClaimAmount(address: string): Promise<u.Fixed8>;
    getHeight(): Promise<number>;
    getTransactionHistory(address: string): Promise<PastTransaction[]>;
    /**
     * Load balances a API call according to the API switch. Selects the appropriate provider and sends the call towards it.
     * @param func - The API call to load balance function
     */
    private loadBalance;
    private increaseLeftWeight;
    private increaseRightWeight;
}
//# sourceMappingURL=apiBalancer.d.ts.map