angular.module('heroes').directive('fightDemo', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            hero1: '=',
            hero2: '='
        },
        templateUrl: 'modules/heroes/directive/fight-demo/fight-demo.html',
        link: function (scope, element, attrs, fn) {

        }
    };
});
