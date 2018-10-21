import { ClaimGasConfig, DoInvokeConfig, SendAssetConfig, SetupVoteConfig } from "./types";
export declare function createClaimTx(config: ClaimGasConfig): Promise<ClaimGasConfig>;
export declare function createContractTx(config: SendAssetConfig): Promise<SendAssetConfig>;
export declare function createInvocationTx(config: DoInvokeConfig): Promise<DoInvokeConfig>;
export declare function createStateTx(config: SetupVoteConfig): Promise<SetupVoteConfig>;
//# sourceMappingURL=create.d.ts.map