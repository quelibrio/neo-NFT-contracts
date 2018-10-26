let {default: Neon, api, nep5, rpc, sc, wallet} = require('@cityofzion/neon-js');

var neo = {
    get: require('./get'),
    call: require('./call'),
    Neon,
    api,
    nep5,
    sc,
    wallet,
    config: require('./neoConfig.js'),
    getByteArrayAddress: (address)=>{
        if(address.length !== 34) {
            alert(`Address ${address} needs to be 34 length. Instead it's ${address.length}.`)
        } else {
            return sc.ContractParam.byteArray(address, 'address');
        }
    }
};
window.neo = neo;
