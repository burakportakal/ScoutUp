$(document).on('click',"#follow", function (e) {
        e.preventDefault();
        var self = $(this);
        var url = self.attr('href');
        $.get(url, function (o) {
            if (o.Result == 1) {
                self.text("Takip ediliyor");
                var url = self.attr('href').replace("follow", "stopfollow");
                self.attr('href',url);
                self.attr("id", "stopfollow");
            }
            else {

            }
        })
        
});
$(document).on('click', "#stopfollow", function (e) {
    e.preventDefault();
    var self = $(this);
    var url = self.attr('href');
    $.get(url, function (o) {
        if (o.Result == 1) {
            self.text("Takip Et");
            var url = self.attr("href").replace("stopfollow", "follow");
            self.attr("href", url);
            self.attr("id", "follow");
        }
        else {

        }
    })
});

