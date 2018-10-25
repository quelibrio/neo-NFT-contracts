angular.module('brawl', ['ui.bootstrap', 'ui.router', 'ngAnimate']);

angular.module('brawl').config(function ($stateProvider) {

    $stateProvider.state('base.brawl', {
        url: '/brawl',
        templateUrl: 'modules/brawl/partial/brawl-main/brawl-main.html'
    });
    /* Add New States Above */

});

