$(function () {

    if ($(".btn").length > 0) {
        var postClient = $.connection.postHub;

        postClient.client.updateLikeCount = function (post) {
            var self = $("#"+post.PostId);
            self.html('<i class="icon ion-thumbsup"></i> Beğen ' + e.likes);
        };

        $("body").on("click",".btn", function () {
            var code = $(this).attr("data-id");
            postClient.server.like(code,1);
        });

        $.connection.hub.start();
    }

});