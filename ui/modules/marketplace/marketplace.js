angular.module('marketplace', ['ui.bootstrap', 'ui.router', 'ngAnimate']);

angular.module('marketplace').config(function ($stateProvider) {


    $stateProvider.state('base.marketplace', {
        url: '/marketplace',
        templateUrl: 'modules/marketplace/partial/marketplace-main/marketplace-main.html'
    });
    /* Add New States Above */

});

