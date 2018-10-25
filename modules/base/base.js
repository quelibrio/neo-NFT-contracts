angular.module('base', ['ui.bootstrap', 'ui.router', 'ngAnimate']);

angular.module('base').config(function ($stateProvider) {
    $stateProvider.state('base', {
        url: '',
        templateUrl: 'modules/base/partial/main/main.html',
        resolve: {
            user: function (/*authService*/) {
              /*  return authService.me().catch(function (err) {
                    console.error(err);
                });*/
            }
        }
    });

    $stateProvider.state('base.home', {
        url: '/home',
        templateUrl: 'modules/base/partial/home/home.html'
    });
    /* Add New States Above */

});

