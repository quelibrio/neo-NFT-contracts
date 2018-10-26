angular.module('heroes').controller('HeroesMainCtrl', function ($scope, $rootScope, nftService, $compile) {

    $scope.owner = 'AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y';

    $scope.generate = () => {
        nftService.mintToken(
            $scope
        ).then(function (txid) {
            generateCharStats(txid);
        }).catch((err) => alert(JSON.stringify(err)));
    };
    $scope.strength = 65;
    $scope.agile = 55;
    $scope.power = 85;
    $scope.speed = 37;
    $scope.gen = 0;

    $scope.battle = () => {
        alert('Not implemented');
    };

    $scope.getCharacterCount = () => {
        nftService.balanceOf($scope).then((count) => {
            $("#output").html(count);
            console.log(JSON.stringify(count))
        }).catch((err) => alert(JSON.stringify(err)));
    };

    $scope.getOwner = (tokenId) => {
            nftService.ownerOf({tokenId}).then($("#output").html.bind($("#output")));
    };

    $scope.tokensOfOwner = () => {
        nftService.tokensOfOwner({owner: $scope.owner}).then($("#output").html.bind($("#output")));
    };

    function generateCharStats(txid) {
        let chunks = chunkSubstr(txid, 8);
        let hero = {
            health: chunks[0],
            mana: chunks[1],
            agility: chunks[2],
            stamina: chunks[3],
            critical: chunks[4],
            attackSpeed: chunks[5],
            mastery: chunks[6],
            versatility: chunks[7],
            gen: 0,
            txid
        };
        populateStats(hero);
        populateCaroucell(hero);
    }

    let heroes = 0, page = 0;

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

        if (heroes % 4 == 0) {
            page = page + 1
        }
        var newElement = $compile(`<li>
                                <div class="ui-tooltip" style="left: 614.5px; top: 510px; display: block;">
                                    <div class="tooltip-content">
                                        <div class="d3-tooltip d3-tooltip-item">
                                            <div class="tooltip-head ${epicnessLevel}" ng-click="getOwner('${stats.txid}')">
                                                <h3 class="${epicLevel}">${stats.txid}</h3>
                                            </div>
                                            <div class="tooltip-body effect-bg effect-bg-cold">
                                                <div class="d3-item-properties">
                                                    <ul class="item-itemset">
                                                        <li class="item-itemset-name">
                                                            <span class="d3-color-red">
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
                                                                Critical Strike: <a>${stats.critical}%</a>
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
                                                <div class="flavor">Generation of ${stats.gen}</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </li>`)($scope);
        $('.heroes:eq(' + page + ')').append(newElement);
    }

    function populateStats(hero) {
        $rootScope.safeApply(() => {
            $scope.hero = hero
        });
    }

    function chunkSubstr(str, size) {
        const numChunks = Math.ceil(str.length / size)
        const chunks = new Array(numChunks)

        for (let i = 0, o = 0; i < numChunks; ++i, o += size) {
            chunks[i] = parseInt(str.substr(o, size), 16)
            if (i < 4)
                chunks[i] = Math.round(random(chunks[i]) * 1000)
            else
                chunks[i] = Math.round(random(chunks[i]) * 100)
        }

        return chunks
    }

    function random(seed) {
        var x = Math.sin(seed) * 10000;
        return x - Math.floor(x)
    }

    $('.royalSlider').royalSlider({
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

    function sliderOnChange(e) {
        rewind(e.value);
    }

    function sliderTileOnSlide(e) {
        speed(e.value / 100);
    }

    $("#slider").kendoSlider({
        increaseButtonTitle: "Right",
        decreaseButtonTitle: "Left",
        change: sliderOnChange,
        value: 1,
        min: 1,
        max: 20,
        smallStep: 1,
        largeStep: 3,
        showButtons: false,
    }).data("kendoSlider");

    $("#sliderTile").kendoSlider({
        increaseButtonTitle: "Speed Up",
        decreaseButtonTitle: "Speed down",
        slide: sliderTileOnSlide,
        value: 100,
        min: 60,
        max: 140,
        smallStep: 20,
        largeStep: 40,
        showButtons: true
    }).data("kendoSlider1");

});
