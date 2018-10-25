angular.module('heroes').controller('HeroesMainCtrl', function ($scope, $rootScope) {
    let otherAddress = window.sc.get.sc.ContractParam.byteArray('ASP3X76d9JunQosUds3npubiDsSpm3RMXF', 'address')

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

    $scope.generate = () => {
        window.sc.call.invoke('mintToken', [
                "Lalallala".hexEncode(),
                "1111111111111".hexEncode(),
                otherAddress.value])
            .then(function (result) {
                $("#nextCommand").text(result);
                let txid = result.response.txid;

                generateCharStats(txid)
            }).catch((err) => alert(JSON.stringify(err)));
    };

    $scope.battle = () => {
        alert('Not implemented');
    };

    $scope.getCharacterCount = () => {
        window.sc.get.invoke('totalSupply', [otherAddress.value]).then(function (result) {
            $("#nextCommand").text(result[0].value);
            console.log(JSON.stringify(result[0].value))
        }).catch((err) => alert(JSON.stringify(err)));
    };


    function generateCharStats(txid) {
        let chunks = chunkSubstr(txid, 8);
        let stats = {
            health: chunks[0],
            mana: chunks[1],
            agility: chunks[2],
            stamina: chunks[3],
            critical: chunks[4],
            haste: chunks[5],
            mastery: chunks[6],
            versatility: chunks[7]
        };
        populateStats(stats);
        populateCaroucell(stats);
        console.log(stats);
    }

    let heroes = 2, page = 0;

    function populateCaroucell(chunks) {
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
        heroes++

        if (heroes % 4 == 0) {
            page = page + 1
        }
        $('.heroes:eq(' + page + ')').append(
            `<li>
        <div class="ui-tooltip">
            <div class="tooltip-content">
                <div class="d3-tooltip d3-tooltip-item">
                    <div class="tooltip-head tooltip-head-green">
                        <h3 class="${epicLevel}">Feet</h3>
                    </div>
                    <div class="tooltip-body effect-bg effect-bg-holy">
                        <div class="d3-item-properties">
                            <ul class="item-type-right">
                                <li class="item-slot">Foot up </li>
                            </ul>
                            <ul class="item-itemset">
                                <li class="item-itemset-name">
                                    <span onclick="rewind(0)" class="d3-color-green">
                                        <a>Foot down</a>
                                    </span>
                                </li>
                                <li class="item-itemset-piece indent">
                                    <span class="d3-color-gray">
                                        <a>Foot in </a>
                                    </span>
                                </li>
                                <li class="item-itemset-piece indent">
                                    <span class="d3-color-gray">Foot out</span>
                                </li>
                                <li class="item-itemset-piece indent">
                                    <span class="d3-color-gray">Circlesin both directions</span>
                                </li>
                                <li class="item-itemset-piece indent">
                                    <span class="d3-color-gray">Leg circles in both directions</span>
                                </li>
                            </ul>
                            <div class="item-before-effects"></div>
                            <span class="clear"></span>
                        </div>
                    </div>
                    <div class="tooltip-extension ">
                        <div class="flavor">Note on last exercise.</div>
                    </div>
                </div>
            </div>
        </div>
    </li>`);
    }

    function populateStats(stats) {
        $rootScope.safeApply(() => {
            $scope.stats = stats
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
});
