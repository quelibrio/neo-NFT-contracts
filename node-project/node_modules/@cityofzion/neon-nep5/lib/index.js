"use strict";
function __export(m) {
    for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
}
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
const nep5 = __importStar(require("./plugin"));
function bundle(neonCore) {
    return Object.assign({}, neonCore, { nep5 });
}
exports.default = bundle;
__export(require("./plugin"));
//# sourceMappingURL=index.js.map