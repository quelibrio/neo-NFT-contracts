angular.module('heroes').directive('heroStats', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            stats: '='
        },
        templateUrl: 'modules/heroes/directive/hero-stats/hero-stats.html',
        link: function (scope, element, attrs, fn) {

        }
    };
});
