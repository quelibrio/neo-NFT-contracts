import { ScryptParams } from "./nep2";
export interface AccountJSON {
    address: string;
    label: string;
    isDefault: boolean;
    lock: boolean;
    key: string;
    contract?: {
        script: string;
        parameters: Array<{
            name: string;
            type: string;
        }>;
        deployed: boolean;
    };
    extra?: {
        [key: string]: any;
    };
}
/**
 * This allows for simple utilisation and manipulating of keys without need the long access methods.
 *
 * Key formats are derived from each other lazily and stored for future access.
 * If the previous key (one level higher) is not found, it will attempt to generate it or throw an Error if insufficient information was provided (eg. trying to generate private key when only address was given.)
 *
 * NEP2 <=> WIF <=> Private => Public => ScriptHash <=> Address
 *
 * @param str WIF/ Private Key / Public Key / Address or a Wallet Account object.
 * @example
 * const acct = new Account("L1QqQJnpBwbsPGAuutuzPTac8piqvbR1HRjrY5qHup48TBCBFe4g");
 * acct.address; // "ALq7AWrhAueN6mJNqk6FHJjnsEoPRytLdW"
 */
export declare class Account {
    /**
     * Create a multi-sig account from a list of public keys
     * @param signingThreshold Minimum number of signatures required for verification. Must be larger than 0 and less than number of keys provided.
     * @param publicKeys List of public keys to form the account. 2-16 keys allowed. Order is important.
     * @example
     * const threshold = 2;
     * const publicKeys = [
     * "02028a99826edc0c97d18e22b6932373d908d323aa7f92656a77ec26e8861699ef",
     * "031d8e1630ce640966967bc6d95223d21f44304133003140c3b52004dc981349c9",
     * "02232ce8d2e2063dce0451131851d47421bfc4fc1da4db116fca5302c0756462fa"
     * ];
     * const acct = Account.createMultiSig(threshold, publicKeys);
     */
    static createMultiSig(signingThreshold: number, publicKeys: string[]): Account;
    extra: {
        [key: string]: any;
    };
    isDefault: boolean;
    lock: boolean;
    contract: {
        script: string;
        parameters: Array<{
            name: string;
            type: string;
        }>;
        deployed: boolean;
    };
    label: string;
    private _privateKey?;
    private _encrypted?;
    private _address?;
    private _publicKey?;
    private _scriptHash?;
    private _WIF?;
    constructor(str?: string | Partial<AccountJSON>);
    readonly [Symbol.toStringTag]: string;
    readonly isMultiSig: boolean | "";
    /**
     * Key encrypted according to NEP2 standard.
     * @example 6PYLHmDf6AjF4AsVtosmxHuPYeuyJL3SLuw7J1U8i7HxKAnYNsp61HYRfF
     */
    readonly encrypted: string;
    /**
     * Case sensitive key of 52 characters long.
     * @example L1QqQJnpBwbsPGAuutuzPTac8piqvbR1HRjrY5qHup48TBCBFe4g
     */
    readonly WIF: string;
    /**
     * Key of 64 hex characters.
     * @example 7d128a6d096f0c14c3a25a2b0c41cf79661bfcb4a8cc95aaaea28bde4d732344
     */
    readonly privateKey: string;
    /**
     * Returns the public key in encoded form. This is the form that is the short version (starts with 02 or 03). If you require the unencoded form, do use the publicKey method instead of this getter.
     * @example 02028a99826edc0c97d18e22b6932373d908d323aa7f92656a77ec26e8861699ef
     */
    readonly publicKey: string;
    /** Retrieves the Public Key in encoded / unencoded form.
     * @param encoded Encoded or unencoded.
     */
    getPublicKey(encoded?: boolean): string;
    /**
     * Script hash of the key. This format is usually used in the code instead of address as this is a hexstring.
     */
    readonly scriptHash: string;
    /**
     * Public address used to receive transactions. Case sensitive.
     * @example ALq7AWrhAueN6mJNqk6FHJjnsEoPRytLdW
     */
    readonly address: string;
    /**
     * This is the safe way to get a key without it throwing an error.
     */
    tryGet(keyType: "WIF" | "privateKey" | "publicKey" | "encrypted" | "scriptHash" | "address"): string;
    /**
     * Encrypts the current privateKey and return the Account object.
     */
    encrypt(keyphrase: string, scryptParams?: ScryptParams): Promise<this>;
    /**
     * Decrypts the encrypted key and return the Account object.
     */
    decrypt(keyphrase: string, scryptParams?: ScryptParams): Promise<this>;
    /**
     * Export Account as a WalletAccount object.
     */
    export(): AccountJSON;
    equals(other: AccountJSON): boolean;
    /**
     * Attempts to update the contract.script field if public key is available.
     */
    private _updateContractScript;
    private _getScriptHashFromVerificationScript;
}
export default Account;
//# sourceMappingURL=Account.d.ts.map