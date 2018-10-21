import { u, wallet } from "@cityofzion/neon-core";
import { PastTransaction } from "../common";
/**
 * Returns an appropriate RPC endpoint retrieved from a neonDB endpoint.
 * @param url - URL of a neonDB service.
 * @returns URL of a good RPC endpoint.
 */
export declare function getRPCEndpoint(url: string): Promise<string>;
/**
 * Get balances of NEO and GAS for an address
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return  Balance of address
 */
export declare function getBalance(url: string, address: string): Promise<wallet.Balance>;
/**
 * Get amounts of available (spent) and unavailable claims.
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return An object with available and unavailable GAS amounts.
 */
export declare function getClaims(url: string, address: string): Promise<wallet.Claims>;
/**
 * Gets the maximum amount of gas claimable after spending all NEO.
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return An object with available and unavailable GAS amounts.
 */
export declare function getMaxClaimAmount(url: string, address: string): Promise<u.Fixed8>;
/**
 * Get transaction history for an account
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return a list of PastTransaction
 */
export declare function getTransactionHistory(url: string, address: string): Promise<PastTransaction[]>;
/**
 * Get the current height of the light wallet DB
 * @param url - URL of a neonDB service.
 * @return Current height.
 */
export declare function getHeight(url: string): Promise<number>;
//# sourceMappingURL=core.d.ts.map