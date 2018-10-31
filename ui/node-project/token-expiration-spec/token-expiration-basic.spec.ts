import * as expect from "expect";
import NeoJs from "../NeoJs";

let neo = new NeoJs({
    scriptHash: '117add91374f98419390059a8327faf4ad447b66'
});

describe("Token Expiration", function () {
    before(async () => {
        let result = await neo.call('mint', [neo.sc.ContractParam.byteArray(neo.config.myAddress, 'address')]);
        // let result = await neo.get('balanceOf', neo.sc.ContractParam.byteArray(neo.config.myAddress, 'address'));
        console.log(result);
    });
    it('should not have active Lend on fresh token', async () => {

    });
});
