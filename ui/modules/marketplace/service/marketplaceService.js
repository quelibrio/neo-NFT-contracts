angular.module('marketplace').factory('marketplaceService', function (configService) {
    let neo = window.neo;

    var marketplaceService = {
        createSaleAuction({
                              owner,
                              tokenId,
                              startPrice,
                              endPrice,
                              duration
                          }) {
            let address = neo.getByteArrayAddress(owner);
            return neo.call.invoke('createSaleAuction', [address, tokenId, startPrice, endPrice, duration], configService.get())
                .then(function (result) {
                    return JSON.stringify(result.response, null, 4);
                })
        }
    };

    return marketplaceService;
});
