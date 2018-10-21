import { u, wallet } from "@cityofzion/neon-core";
import { PastTransaction } from "../common";
/**
 * Returns an appropriate RPC endpoint retrieved from a NeoScan endpoint.
 * @param url - URL of a neoscan service.
 * @returns URL of a good RPC endpoint.
 */
export declare function getRPCEndpoint(url: string): Promise<string>;
/**
 * Gets balance for an address. Returns an empty Balance if endpoint returns not found.
 * @param url - URL of a neoscan service.
 * @param address Address to check.
 * @return Balance of address retrieved from endpoint.
 */
export declare function getBalance(url: string, address: string): Promise<wallet.Balance>;
/**
 * Get claimable amounts for an address. Returns an empty Claims if endpoint returns not found.
 * @param url - URL of a neoscan service.
 * @param address - Address to check.
 * @return Claims retrieved from endpoint.
 */
export declare function getClaims(url: string, address: string): Promise<wallet.Claims>;
/**
 * Gets the maximum amount of gas claimable after spending all NEO.
 * @param url - URL of a neoscan service.
 * @param address Address to check.
 * @return
 */
export declare function getMaxClaimAmount(url: string, address: string): Promise<u.Fixed8>;
/**
 * Get the current height of the light wallet DB
 * @param url - URL of a neoscan service.
 * @return  Current height as reported by provider
 */
export declare function getHeight(url: string): Promise<number>;
/**
 * Get transaction history for an account
 * @param {string} net - 'MainNet' or 'TestNet'.
 * @param {string} address - Address to check.
 * @return {Promise<PastTransaction[]>} A listof PastTransactionPastTransaction[]
 */
export declare function getTransactionHistory(url: string, address: string): Promise<PastTransaction[]>;
//# sourceMappingURL=core.d.ts.map