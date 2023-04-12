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

                doCellUpdate(col, row, '/game/leftClick');
                break;
            case 2:
                alert('Middle mouse cell is pressed');
                break;
            case 3:
                event.preventDefault();
                var col = $(this).data("col");
                var row = $(this).data("row");

                console.log("cell at col and row " + col + " " + row + " was right clicked");
                doCellUpdate(col, row, '/game/rightClick');
        }
    })

    $("#save-button").on("click", function (event) {
        event.preventDefault();
        doSaveGame();
    });

    $("#load-button").on("click", function (event) {
        event.preventDefault();
        doLoadGame($("#loadId").val());
    });

    $("#view-button").on("click", function (event) {
        event.preventDefault();
        doViewGames();
    });
});


function doCellUpdate(col, row, urlstring) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: urlstring,
        data: {
            "col": col,
            "row": row
        },
        success: function (data) {
            $("#board").html(data);
        }
    });
};

function doSaveGame() {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: '/game/saveGame',
        success: function (data) {
            $("#save-message").html(data);
        }
    });
}

function doViewGames() {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: '/game/viewGames',
        success: function (data) {
            $("#board").html(data);
        }
    });
}

function doLoadGame(id) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: '/game/loadGame',
        data: {
            "id": id
            },
        success: function (data) {
            $("#board").html(data);
        }
    });
}
