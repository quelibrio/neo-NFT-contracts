var setValueFunction;
var a;


$(document).ready(function ($) {

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


function stop() {
    $("#output").text("stop");
    $("#resume").show();
    $("#stop").hide();
}

function resume() {
    $("#output").text("resume");
    $("#resume").hide();
    $("#stop").show();
}

function rewind(x) {
    $("#output").text("rewind " + x);
}

function speed(x) {
    $("#output").text("speed " + x);
}

function testMethod(arg) {
    $("#fromUnity").text(arg);
    if (arg != 'hello') {
        setValueFunction(arg);
    }
    var obj = swfobject.getObjectById("unityPlayer");
    if (obj) {
        $('#count').text(count);
        var command = $("#output").text();

        obj.callFromJavascript(command);
        $("#output").text("");

        count++;
    }
}
