$(function () {
    console.log("Page is ready");

    $(document).bind("contextmenu", (e) => {
        e.preventDefault();
    })
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
                var col = $(this)[0].form[0].value;
                var row = $(this)[0].form[1].value;

                console.log("cell at col and row " + col + " " + row + " was right clicked");
                doFlag(col, row);
        }
    })
})

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

function doFlag(col, row) {
    $.ajax({
        datatype: "json",
        method: "post",
        url: "/game/rightClick",
        data: {
            "col": col,
            "row": row
        },
        success: function (data) {
            console.log(data);
            $("#" + col + row).html(data);
        }
     })
}
