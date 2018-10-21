"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const neon_core_1 = require("@cityofzion/neon-core");
const axios_1 = __importDefault(require("axios"));
const settings_1 = require("../../settings");
const common_1 = require("../common");
const log = neon_core_1.logging.default("api");
/**
 * Returns an appropriate RPC endpoint retrieved from a neonDB endpoint.
 * @param url - URL of a neonDB service.
 * @returns URL of a good RPC endpoint.
 */
function getRPCEndpoint(url) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/network/nodes");
        const data = response.data.nodes;
        let nodes = data
            .filter(d => d.status)
            .map(d => ({ height: d.block_height, url: d.url }));
        if (settings_1.settings.httpsOnly) {
            nodes = common_1.filterHttpsOnly(nodes);
        }
        const goodNodes = common_1.findGoodNodesFromHeight(nodes);
        const bestRPC = yield common_1.getBestUrl(goodNodes);
        return bestRPC;
    });
}
exports.getRPCEndpoint = getRPCEndpoint;
/**
 * Get balances of NEO and GAS for an address
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return  Balance of address
 */
function getBalance(url, address) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/address/balance/" + address);
        const data = response.data;
        const bal = new neon_core_1.wallet.Balance({ net: url, address });
        if (data.NEO.balance > 0) {
            bal.addAsset("NEO", data.NEO);
        }
        if (data.GAS.balance > 0) {
            bal.addAsset("GAS", data.GAS);
        }
        log.info(`Retrieved Balance for ${address} from neonDB ${url}`);
        return bal;
    });
}
exports.getBalance = getBalance;
/**
 * Get amounts of available (spent) and unavailable claims.
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return An object with available and unavailable GAS amounts.
 */
function getClaims(url, address) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/address/claims/" + address);
        const data = response.data;
        const claims = data.claims.map(c => {
            return {
                claim: new neon_core_1.u.Fixed8(c.claim || 0).div(100000000),
                index: c.index,
                txid: c.txid,
                start: c.start || 0,
                end: c.end || 0,
                value: c.value
            };
        });
        log.info(`Retrieved Claims for ${address} from neonDB ${url}`);
        return new neon_core_1.wallet.Claims({
            net: url,
            address,
            claims
        });
    });
}
exports.getClaims = getClaims;
/**
 * Gets the maximum amount of gas claimable after spending all NEO.
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return An object with available and unavailable GAS amounts.
 */
function getMaxClaimAmount(url, address) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/address/claims/" + address);
        const data = response.data;
        log.info(`Retrieved maximum amount of gas claimable after spending all NEO for ${address} from neonDB ${url}`);
        return new neon_core_1.u.Fixed8(data.total_claim + data.total_unspent_claim).div(100000000);
    });
}
exports.getMaxClaimAmount = getMaxClaimAmount;
/**
 * Get transaction history for an account
 * @param url - URL of a neonDB service.
 * @param address - Address to check.
 * @return a list of PastTransaction
 */
function getTransactionHistory(url, address) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/address/history/" + address);
        const data = response.data;
        log.info(`Retrieved History for ${address} from neonDB ${url}`);
        return data.history.map(rawTx => {
            return {
                change: {
                    NEO: new neon_core_1.u.Fixed8(rawTx.NEO || 0),
                    GAS: new neon_core_1.u.Fixed8(rawTx.GAS || 0)
                },
                blockHeight: rawTx.block_index,
                txid: rawTx.txid
            };
        });
    });
}
exports.getTransactionHistory = getTransactionHistory;
/**
 * Get the current height of the light wallet DB
 * @param url - URL of a neonDB service.
 * @return Current height.
 */
function getHeight(url) {
    return __awaiter(this, void 0, void 0, function* () {
        const response = yield axios_1.default.get(url + "/v2/block/height");
        return parseInt(response.data.block_height, 10);
    });
}
exports.getHeight = getHeight;
//# sourceMappingURL=core.js.map