import { Account, AccountJSON } from "./Account";
import { ScryptParams } from "./nep2";
export interface WalletJSON {
    name: string;
    version: string;
    scrypt: ScryptParams;
    accounts: AccountJSON[];
    extra: {
        [key: string]: any;
    } | null;
}
/**
 * File to store private keys according to the NEP-2 specification.
 */
export declare class Wallet {
    name: string;
    version: string;
    scrypt: ScryptParams;
    accounts: Account[];
    extra: {
        [key: string]: any;
    };
    constructor(obj?: Partial<WalletJSON>);
    readonly [Symbol.toStringTag]: string;
    /**
     * Returns the default Account according to the following rules:
     * 1. First Account where isDefault is true.
     * 2. First Account with a decrypted private key.
     * 3. First Account with an encrypted private key.
     * 4. First Account in the array.
     * Throws error if no accounts available.
     */
    readonly defaultAccount: Account;
    /**
     * Adds an account.
     * @param acct Account or WalletAccount object.
     * @return Index position of Account in array.
     */
    addAccount(acct: Account | AccountJSON): number;
    /**
     * Attempts to decrypt Account at index in array.
     * @param index Index of Account in array.
     * @param keyphrase keyphrase
     * @return Decryption success/failure
     */
    decrypt(index: number, keyphrase: string): boolean;
    /**
     * Attempts to decrypt all accounts with keyphrase.
     * @param keyphrase
     * @return Each boolean represents if that Account has been decrypted successfully.
     */
    decryptAll(keyphrase: string): boolean[];
    /**
     * Attempts to encrypt Account at index in array.
     * @param index Index of Account in array.
     * @param keyphrase
     * @return Encryption success/failure
     */
    encrypt(index: number, keyphrase: string): boolean;
    /**
     * Attempts to encrypt all accounts with keyphrase.
     * @param keyphrase
     * @return Each boolean represents if that Account has been encrypted successfully.
     */
    encryptAll(keyphrase: string): boolean[];
    /**
     * Export this class as a JS object.
     */
    export(): WalletJSON;
    /**
     * Set Account at index in array to be default account.
     * @param index The index of the Account in accounts array.
     * @return this
     */
    setDefault(index: number): void;
}
export default Wallet;
//# sourceMappingURL=Wallet.d.ts.map