import * as _Neon from "@cityofzion/neon-core";
import * as plugin from "./plugin";
declare function bundle<T extends typeof _Neon>(neonCore: T): T & {
    api: typeof plugin;
};
export default bundle;
export * from "./plugin";
//# sourceMappingURL=index.d.ts.map