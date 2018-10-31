angular.module('heroes').directive('heroesList', function ($compile, nftService) {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            heroes: '=',
            onSelect: '='
        },
        templateUrl: 'modules/heroes/directive/heroes-list/heroes-list.html',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {

            scope.select = (heroIndex) => {
                let hero = scope.heroes[heroIndex - 1];
                $(element).find(`.d3-tooltip-item`).parents('.tooltip-content').css('background', 'black');
                $(element).find(`#hero-${heroIndex}`).css('background', 'darkslategrey');
                ngModel && ngModel.$setViewValue(hero); //set value of bound ng-model of the heroes-list directive
                scope.onSelect && scope.onSelect(hero); //call onSelect if it has been attached
            };

            $(element).find('.royalSlider').royalSlider({
                controlNavigation: 'bullets',
                keyboardNavEnabled: true,
                arrowsNav: true,
                arrowsNavAutoHide: true,
                controlsInside: true,
                navigateByClick: false,
                loop: true,
                autoPlay: {
                    enabled: false,
                    pauseOnHover: true,
                    delay: 3000,
                    stopAtAction: false
                }
            });

            let heroes, page;
            scope.$watch('heroes', () => {
                if (scope.heroes && scope.heroes.length) {
                    heroes = 0;
                    page = 0;
                    _.each(scope.heroes, populateCaroucell);
                }
            }, true);
            var currentPage;

            function populateCaroucell(stats) {
                //.d3-color-default
                epicness = ["d3-color-blue",
                    "d3-color-gray",
                    "d3-color-gold",
                    "d3-color-green",
                    "d3-color-orange",
                    "d3-color-purple",
                    "d3-color-red",
                    "d3-color-white",
                    "d3-color-yellow"]

                epicnessItem = ["tooltip-head-gray",
                    "tooltip-head-white ",
                    "tooltip-head-blue  ",
                    "tooltip-head-yellow ",
                    "tooltip-head-orange ",
                    "tooltip-head-purple ",
                    "tooltip-head-green  "]

                epicLevel = epicness[Math.floor(Math.random() * epicness.length)];
                epicnessLevel = epicnessItem[Math.floor(Math.random() * epicnessItem.length)];
                heroes++

                if (heroes % 3 == 0 || heroes == 1) {
                    page = page + 1;

                    var slider = $(element).find(".royalSlider").data('royalSlider');
                    currentPage = $(`<div class="rsSlide sliderImages">
                <ul class="heroes">
                </ul>
            </div>`);
                    slider.appendSlide(currentPage);
                }
                var newElement = $compile(`<li>
                                <div class="ui-tooltip" style="left: 614.5px; top: 510px; display: block;">
                                    <div class="tooltip-content" id="hero-${heroes}">
                                        <div class="d3-tooltip d3-tooltip-item">
                                            <div class="tooltip-head ${epicnessLevel}" ng-click="select(${heroes})">
                                                <h3 class="${epicLevel}">${stats.txId}</h3>
                                            </div>
                                            <div class="tooltip-body effect-bg effect-bg-cold">
                                                <div class="d3-item-properties">
                                                    <ul class="item-itemset">
                                                        <li class="item-itemset-name">
                                                            <span class="d3-color-green">
                                                                Health: <a>${stats.health}</a>
                                                            </span>
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-blue">
                                                                Mana: <a>${stats.mana}</a>
                                                            </span>
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-gray">
                                                                Agility: <a>${stats.agility}</a>
                                                            </span>                                                     
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-orange">
                                                                Stamina: <a>${stats.stamina}</a>
                                                            </span> 
                                                        </li>
                                                        <span class="clear"></span>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-red">
                                                                Critical Strike: <a>${stats.criticalStrike}%</a>
                                                            </span> 
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-yellow">
                                                                Attack Speed: <a>${stats.attackSpeed}%</a>
                                                            </span> 
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-cyan" style="color: white">
                                                                Mastery: <a>${stats.mastery}%</a>
                                                            </span> 
                                                        </li>
                                                        <li class="item-itemset-piece indent">
                                                            <span class="d3-color-green">
                                                                Versatility: <a>${stats.versatility}%</a>
                                                            </span> 
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>

                                            <div class="tooltip-extension ">
                                                <div class="flavor">Generation of ${stats.gen || 0}</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </li>`)(scope);
                $(currentPage).find('.heroes').append(newElement);
            }

        }
    };
});
