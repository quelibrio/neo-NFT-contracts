import * as _Neon from "@cityofzion/neon-core";
import * as nep5 from "./plugin";
declare function bundle<T extends typeof _Neon>(neonCore: T): T & {
    nep5: typeof nep5;
};
export default bundle;
export * from "./plugin";
//# sourceMappingURL=index.d.ts.map