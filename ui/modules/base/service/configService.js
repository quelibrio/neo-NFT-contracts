angular.module('base').factory('configService',function($rootScope) {
    let defaultNeoConfig = _.clone(window.neo.config);
    var config = localStorage.getItem('config') ? JSON.parse(localStorage.getItem('config')): _.clone(defaultNeoConfig);


    var configService = {
        get() {
            return config;
        },
        set(value) {
            $rootScope.safeApply(() => {
                config = value;
            })
        },
        undo () {
            let prev = localStorage.getItem('configPrev');
            if(prev) {
                localStorage.setItem('configPrev', localStorage.getItem('config'));
                configService.set(JSON.parse(prev));
            }
        },
        save (){
            localStorage.setItem('configPrev', localStorage.getItem('config'));
            localStorage.setItem('config', JSON.stringify(config));
        },
        reset() {
            localStorage.removeItem('config');
            configService.set(_.clone(defaultNeoConfig));
        },
        hasPending() {
            return localStorage.getItem('config') !== JSON.stringify(configService.get());
        }
    };

    return configService;
});
