angular.module('base').directive('configurator', function(configService) {
    return {
        restrict: 'E',
        replace: true,
        scope: {

        },
        templateUrl: 'modules/base/directive/configurator/configurator.html',
        link: function(scope, element, attrs, fn) {
            scope.config = configService.get();
            let initial = _.cloneDeep(scope.config);
            scope.save = () => {
                configService.save();
                scope.hasPending = configService.hasPending();
            };
            scope.reset = () => {
                configService.reset();
                scope.hasPending = configService.hasPending();
            };
            scope.undo = () => {
                configService.undo();
                scope.hasPending = configService.hasPending();
            };
            scope.$watch(configService.get, ()=> {
                scope.config = configService.get();
            })
            scope.$watch('config', () => {
                if(JSON.stringify(scope.config) === JSON.stringify(initial)) {
                    return;
                }
                configService.set(scope.config);
                scope.hasPending = configService.hasPending();
            }, true);
        }
    };
});
