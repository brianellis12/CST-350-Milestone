$(function () {
    console.log("Page is ready");

    //$(document).on("click", ".game-button", function (event) {
    //     event.preventDefault();

    //     var buttonNumber = $(this).val();
    //     console.log("Button number " + buttonNumber + " was clicked");

    //     doButtonUpdate(buttonNumber);
    //     checkForWin(buttonNumber);
    // });
    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
        console.log("Right click. Prevent context menu from showing.")
    });

    $(document).on("mousedown", ".game-button", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();

                var buttonNumber = $(this).val();
                console.log("Button number " + buttonNumber + " was clicked");

                doButtonUpdate(buttonNumber, '/button/showOneButton');
                checkForWin(buttonNumber);
                break;
            case 2:
                alert('Middle mouse button is pressed');
                break;
            case 3:
                event.preventDefault();

                var buttonNumber = $(this).val();
                console.log("Button number " + buttonNumber + " was right clicked");

                doButtonUpdate(buttonNumber, "/button/RightClickShowOneButton");
                checkForWin(buttonNumber);
                break;
            default:
                alert('Nothing');
        }
    });
});

function doButtonUpdate(buttonNumber, urlString) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: urlString,
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            console.log(data);
            $("#" + buttonNumber).html(data);
        }
    });
};

function checkForWin(buttonNumber) {
    $.ajax({
        type: 'json',
        method: 'POST',
        data: {
            "buttonNumber": buttonNumber
        },
        url: '/button/ShowWinOrLossMessage',
        success: function (data) {
            console.log(data);

            $("#win-message").html(data);
        }
    });
};