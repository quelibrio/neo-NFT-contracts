angular.module('heroes', ['ui.bootstrap', 'ui.router', 'ngAnimate']);

angular.module('heroes').config(function ($stateProvider) {


    $stateProvider.state('base.heroes', {
        url: '/heroes',
        templateUrl: 'modules/heroes/partial/heroes-main/heroes-main.html'
    });
    /* Add New States Above */

});

