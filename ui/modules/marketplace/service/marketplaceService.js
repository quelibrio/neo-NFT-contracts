angular.module('marketplace').factory('marketplaceService', function (configService) {
    let neo = window.neo;

    var marketplaceService = {
        createSaleAuction({
                              owner,
                              hero: {
                                  txId
                              },
                              startPrice,
                              endPrice,
                              duration
                          }) {
            let address = neo.getByteArrayAddress(owner);
            return neo.call('createSaleAuction', [address.value, +txId, +startPrice, +endPrice, +duration], {
                    ... configService.get(),
                    scriptHash: configService.get().marketHash
                })
                .then(function (result) {
                    return JSON.stringify(result.response, null, 4);
                })
        }
    };

    return marketplaceService;
});
