let {default: Neon, api, nep5, rpc, sc, wallet} = require('@cityofzion/neon-js');

var neo = {
    get: require('./getWorking'),
    call: require('./mintWorking'),
    Neon,
    api,
    nep5,
    sc,
    wallet,
    config: require('./config.js')
};
window.neo = neo;
