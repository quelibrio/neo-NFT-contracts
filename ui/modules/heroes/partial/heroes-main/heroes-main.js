angular.module('heroes').controller('HeroesMainCtrl', function ($scope, $rootScope, nftService) {

    $scope.owner = 'AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y';

    $scope.generate = () => {
        nftService.mintToken(
            $scope
        ).then(function (txid) {
            $rootScope.safeApply(() => {
                generateCharStats(txid);
            });
        }).catch((err) => alert(JSON.stringify(err)));
    };
    $scope.heroes = [];
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
        $scope.heroes.push(hero);
        populateStats(hero);
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
