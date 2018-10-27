angular.module('marketplace').controller('MarketplaceMainCtrl', function ($scope, $rootScope, configService, auctionService, marketplaceService) {
    $scope.myAddress = configService.get().myAddress;
    $scope.reload = () => {
        auctionService.get().then((items) => $rootScope.safeApply(() => $scope.items = items));
    };

    $scope.createSaleAuction = async (create) => {
        $scope.createResult = null;
        return marketplaceService.createSaleAuction({owner: $scope.address, ...create})
            .then((result) => $rootScope.safeApply(() => $scope.createResult = result))
            .catch((err) => $rootScope.safeApply(() => $scope.createResult = {
                error: createResult
            }));
    }
    $scope.reload();
});
