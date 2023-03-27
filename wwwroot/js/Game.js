$(function () {
    console.log("Page is ready");

    $(document).bind("contextmenu", (e) => {
        e.preventDefault();
    })
    $(document).on("mousedown", ".cellButton", function (event) {
        switch (event.which) {
            case 3:
                event.preventDefault();
                let col = $(this)[0].form[0].value;
                let row = $(this)[0].form[1].value;
                doFlag(col, row);
        }
    })
})

function doFlag(col, row) {
    $.ajax({
        datatype: "json",
        method: "post",
        url: "/game/rightClick",
        data: {
            "col": col,
            "row": row
        }
     })
}
