angular.module('brawl').controller('BrawlMainCtrl', function ($scope, configService) {
    $scope.myAddress = configService.get().myAddress;

});
