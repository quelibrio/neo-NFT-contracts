angular.module('marketplace').controller('MarketplaceMainCtrl', function ($scope, $rootScope, configService, auctionService, marketplaceService) {
    $scope.myAddress = configService.get().myAddress;
    $scope.reloadAuctions = () => {
        auctionService.get().then((auctions) => $rootScope.safeApply(() => $scope.auctions = auctions));
    };
    $scope.create = {startPrice: 5000000000, endPrice: 100000000, duration: 30};
    $scope.createSaleAuction = async (create) => {
        $scope.createResult = null;
        return marketplaceService.createSaleAuction({owner: $scope.myAddress, ...create})
            .then((result) => $rootScope.safeApply(() => $scope.createResult = result))
            .catch((err) => $rootScope.safeApply(() => $scope.createResult = {
                error: createResult
            }));
    }
    $scope.reloadAuctions();
});
