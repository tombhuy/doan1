var common = {
    init: function () {

    },
    registerEvent: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/ChapterDetail/Search",
                    dataType: "json",
                    data: {
                        searchkey: request.term
                    },
                    success: function (response) {
                        response(response.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);

                return false;
            }
        })
    .autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
          .append("<a>" + item.label + "</a>")
          .appendTo(ul);
    };
    }
}
common.init();