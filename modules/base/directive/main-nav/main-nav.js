angular.module('base').directive('mainNav', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {},
        templateUrl: 'modules/base/directive/main-nav/main-nav.html',
        link: function (scope, element, attrs, fn) {
            scope.sections = [
                {
                    sref: 'base.heroes',
                    label: 'Heroes',
                    base: 'heroes'
                }, {
                    sref: 'base.breed',
                    label: 'Breed',
                    base: 'breed'
                }, {
                    sref: 'base.brawl',
                    label: 'Brawl',
                    base: 'brawl'
                }, {
                    sref: 'base.marketplace',
                    label: 'Marketplace',
                    base: 'marketplace'
                }
            ];

        }
    };
});
