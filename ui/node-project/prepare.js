let config = require('./neoConfig.js');
let {default: Neon, api, nep5, rpc, sc, wallet} = require('@cityofzion/neon-js');

module.exports.getRequest = (operation, args, configOverride) =>{
    let useConfig = configOverride || config;
    if(!useConfig.account.address) //when storing in json usually the configOverride needs to restore the account
    {
        useConfig.account = Neon.create.account(useConfig.account._WIF);
    }
    let hash = useConfig.scriptHash;

    let script = Neon.create.script(
        {
            scriptHash: hash,
            operation: operation,
            args: args
        }
    );

    let account = useConfig.account;
    return {
        net: useConfig.neoscanUrl,
        url: useConfig.networkUrl,
        script,
        account: account,
        address: account.address,
        privateKey: account.privateKey,
        publicKey: account.publicKey,
        gas: 0,
        balance: null
    }
};
