angular.module('marketplace').controller('MarketplaceMainCtrl', function ($scope, $rootScope, auctionService, marketplaceService) {
    $scope.address = 'AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y';
    $scope.reload = () => {
        auctionService.get().then((items) => $scope.items = items);
    }
    $scope.reloadMine = () => {
        auctionService.myTokens($scope.address).then((items) => $scope.heroes = items);
    }
    $scope.createSaleAuction = async (create) => {
        $scope.createResult = null;
        return marketplaceService.createSaleAuction({owner: $scope.address, ...create})
            .then((result) => $rootScope.safeApply(() => $scope.createResult = result))
            .catch((err) => $rootScope.safeApply(() => $scope.createResult = {
                error: createResult
            }));
    }
    $scope.reload();
    $scope.reloadMine();
});
