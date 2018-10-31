import * as get from "./get";
import * as call from "./call";
import * as neoConfig from "./neoConfig";
import Neon, {api, nep5, rpc, sc, wallet} from "@cityofzion/neon-js";
import * as _ from "lodash";

export default class NeoJs {
    config: any;
    Neon: any;
    sc: any;

    constructor(config) {
        this.config = _.merge(neoConfig, config);
        this.Neon = Neon;
        this.sc = sc;
    }

    public call(...args) {
        return call(...args, this.config);
    }

    public get(...args) {
        return get.apply(null, args.concat([this.config]));
    }
}
