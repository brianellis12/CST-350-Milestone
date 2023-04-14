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
                // console.log("cell at col and row " + col + " " + row + " was left clicked");

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

    $(document).on("click", ".save-btn", function (event) {
        event.preventDefault();
        doSaveGame();
    });

    $(document).on("click", ".load-btn", function (event) {
        event.preventDefault();
        var id = $(this).data("id")
        doLoadGame(id);
    });

    $(document).on("click", ".view-btn", function (event) {
        event.preventDefault();
        doViewGames();
    });

    $(document).on("click", ".delete-btn", function (event) {
        event.preventDefault();
        var id = $(this).data("id")
        doDeleteGame(id);
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

function doDeleteGame(id) {
    $.ajax({
        datatype: "json",
        method: 'POST',
        url: '/game/deleteGame',
        data: {
            "id": id
        },
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
