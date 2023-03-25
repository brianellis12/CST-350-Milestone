$(function () {
    console.log("Page is ready");

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
        console.log("Right click. Prevent context menu from showing.")
    });

    $(document).on("mousedown", ".game-cell", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();

                var col = $(this).data("col");
                var row = $(this).data("row");
                console.log("cell at col and row " + col + " " + row + " was left clicked");

                checkForWin();

                doCellUpdate(col, row, '/game/leftClick');
                break;
            case 2:
                alert('Middle mouse cell is pressed');
                break;
            case 3:
                event.preventDefault();

                var col = $("input:hidden#col").val();
                var row = $("input:hidden#row").val();

                console.log("cell at col and row " + col + row + " was right clicked");

                doCellUpdate(col, row, "/game/RightClick");
                break;
            default:
                alert('Nothing');
        }
    });
});

function doCellUpdate(col, row, urlString) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: urlString,
        data: {
            "col": col,
            "row": row
        },
        success: function (data) {
            console.log(data);
            $("#" + col + row).html(data);
        }
    });
};

function checkForWin() {
    $.ajax({
        type: 'json',
        method: 'POST',
        url: 'game/ShowWinOrLossMessage',
        success: function (data) {
            console.log(data);

            window.location.href = data;
        }
    });
};