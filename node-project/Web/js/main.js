var setValueFunction;
var a;


$(document).ready(function ($) {
    otherAddress = window.sc.get.sc.ContractParam.byteArray('ASP3X76d9JunQosUds3npubiDsSpm3RMXF', 'address')

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

	$("#stop").hide();
	stop();
	//$('#music').hide();
	$('#musicOff').hide();


    var exerciseSlider = $("#slider").kendoSlider({
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

    var required = $("#required").kendoMultiSelect().data("kendoMultiSelect");

    

    setValueFunction = function sliderSetValue(x) {
        exerciseSlider.value(x);
    }

    function sliderOnChange(e) {
        rewind(e.value);
    }

    function sliderTileOnSlide(e) {
        speed(e.value / 100);
    }

    $('.playpause').on('click', function () {
        if (isPlaying(a)) {
            a.pause();
            $('#musicOn').show();
            $('#musicOff').hide();
        } else {
            a.play();
            $('#musicOff').show();
            $('#musicOn').hide();
        }
    });
});

function setUpUnitySWF(){
	swfobject.registerObject("unityPlayer", "11.2.0");
}

var count = 1;

function music() {
    document.getElementById('music').play();
}


function stopMusic(){
    document.getElementById('music').pause();
}

String.prototype.hexEncode = function(){
    var hex, i;

    var result = "";
    for (i=0; i<this.length; i++) {
        hex = this.charCodeAt(i).toString(16);
        result += ("000"+hex).slice(-4);
    }

    return result
}

String.prototype.hexDecode = function(){
    var j;
    var hexes = this.match(/.{1,4}/g) || [];
    var back = "";
    for(j = 0; j<hexes.length; j++) {
        back += String.fromCharCode(parseInt(hexes[j], 16));
    }

    return back;
}

function previous(){
    window.sc.call.invoke('mintToken', [
            "Lalallala".hexEncode(),
            "1111111111111".hexEncode(), 
            otherAddress.value])
        .then(function(result) {
        $("#nextCommand").text(result);
        txid = result.response.txid
        generateCharStats(txid)
    });
}

function next() {
    window.sc.get.invoke('totalSupply', [otherAddress.value]).then(function(result) {
        $("#nextCommand").text(result[0].value);
        console.log(JSON.stringify(result[0].value))
    });
}

function generateCharStats(txid){
    chunks = chunkSubstr(txid, 8)
    populateStats(chunks)
    console.log(chunks)
}

function populateStats(chunks){
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[1]/a/div/div[2]/span').text(chunks[0])
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[2]/a/div/div[2]/span').text(chunks[1])
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[3]/a/div/div[2]/span').text(chunks[2])
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[4]/a/div/div[2]/span').text(chunks[3])

    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[5]/a/div/div[2]/span').text(chunks[4] + "%")
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[6]/a/div/div[2]/span').text(chunks[5] + "%")
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[7]/a/div/div[2]/span').text(chunks[6] + "%")
    $(document).xpathEvaluate('//*[@id="start"]/div[2]/div[1]/div/div[1]/div[8]/a/div/div[2]/span').text(chunks[7] + "%")
}

$.fn.xpathEvaluate = function (xpathExpression) {
    // NOTE: vars not declared local for debug purposes
    $this = this.first(); // Don't make me deal with multiples before coffee
 
    // Evaluate xpath and retrieve matching nodes
    xpathResult = this[0].evaluate(xpathExpression, this[0], null, XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
 
    result = [];
    while (elem = xpathResult.iterateNext()) {
       result.push(elem);
    }
 
    $result = jQuery([]).pushStack( result );
    return $result;
 }

function chunkSubstr(str, size) {
    const numChunks = Math.ceil(str.length / size)
    const chunks = new Array(numChunks)
  
    for (let i = 0, o = 0; i < numChunks; ++i, o += size) {
      chunks[i] =  parseInt(str.substr(o, size), 16)
      if(i < 4)
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

function stop() {
    $("#nextCommand").text("stop");
    $("#resume").show();
    $("#stop").hide();
}

function resume() {
    $("#nextCommand").text("resume");
    $("#resume").hide();
    $("#stop").show();
}

function rewind(x) {
    $("#nextCommand").text("rewind " + x);
}

function speed(x) {
    $("#nextCommand").text("speed " + x);
}

function testMethod(arg) {
    $("#fromUnity").text(arg);
    if (arg != 'hello') {
        setValueFunction(arg);
    }
    var obj = swfobject.getObjectById("unityPlayer");
    if (obj) {
        $('#count').text(count);
        var command = $("#nextCommand").text();

        obj.callFromJavascript(command);
        $("#nextCommand").text("");

        count++;
    }
}