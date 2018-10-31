angular.module('heroes').directive('myHeroesList', function ($rootScope, auctionService) {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            address: '=',
            reload: '='
        },
        require: '?ngModel',
        templateUrl: 'modules/heroes/directive/my-heroes-list/my-heroes-list.html',
        link: function (scope, element, attrs, ngModel) {
            scope.$watch('address', (address) => {
                if (address) {
                    scope.heroes = [];
                    scope.reload(address);
                } else {
                    scope.heroes = null;
                }
            });

            scope.hero = null;
            scope.$watch('hero', (hero) => {
                if (ngModel) {
                    ngModel.$setViewValue(hero);
                }
            });

            scope.reload = () => {
                auctionService.myTokens(scope.address).then((items) =>
                    $rootScope.safeApply(() => scope.heroes = items));
            }
        }
    }
});
