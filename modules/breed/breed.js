angular.module('breed', ['ui.bootstrap', 'ui.router', 'ngAnimate']);

angular.module('breed').config(function ($stateProvider) {


    $stateProvider.state('base.breed', {
        url: '/breed',
        templateUrl: 'modules/breed/partial/breed-main/breed-main.html'
    });
    /* Add New States Above */

});

