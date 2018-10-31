angular.module('base').factory('nftService',function(configService) {
    var nftService = {
        mintToken({
            owner,
            health,
            mana,
            agility,
            stamina,
            criticalStrike,
            attackSpeed,
            versatility,
            mastery,
            level
                  }) {
            let otherAddress = window.neo.getByteArrayAddress(owner);
            let address = otherAddress.value;
			console.log([
                address,
                +health,
                +mana,
                +agility,
                +stamina,
                +criticalStrike,
                +attackSpeed,
                +versatility,
                +mastery,
                +level
                ])
            return window.neo.call('mintToken', [
                address,
                +health,
                +mana,
                +agility,
                +stamina,
                +criticalStrike,
				+attackSpeed,
                +versatility,
                +mastery,
                +level
                ], configService.get()).then(result => result.response.txId);
        },
        balanceOf({
            owner
        }) {
            let otherAddress = window.neo.getByteArrayAddress(owner);
            return window.neo.get('totalSupply', [], configService.get()).then(function (result) {
                return result[0].value || 0;
            })
        },
        ownerOf({
            tokenId
                }) {
            return window.neo.get('ownerOf', [tokenId], configService.get()).then(function (result) {
                return JSON.stringify(result, null, 4);
            })
        },
        tokensOfOwner({
                          owner
                      }) {
            let otherAddress = window.neo.getByteArrayAddress(owner);
            return window.neo.get('tokensOfOwner', [otherAddress], configService.get()).then(function (result) {
                return JSON.stringify(result, null, 4);
            })
        },
    };

    return nftService;
});
