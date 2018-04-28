/*$(function () {
    var text1 = '<div class="follow-user">';
    var url = "/users/FollowSuggest";
    $.get(url, function (o) {
        for (var i = 0; i < o.length; i++) {
            var img = '<img src="../../images/users/user-11.jpg" alt="" class="profile-photo-sm pull-left" /> \n'
            var text2 = '<div> \n ';
            var text3 = '<h5><a href="/users/"' + o[i].id + '" data-id="' + o[i].id + '">' + o[i].Name + '</a></h5> \n';
            var text4 = '<a class="follow"  href="/users/follow/' + o[i].id + '" data-id="' + o[i].id + '" class="text-green">Follow</a> \n';
            var text5 = '</div> \n';
            var text6 = '</div> \n';
            var temp = text1 + img + text2 + text3 + text4 + text5 + text6;
            $("#sticky-sidebar").append(temp);
        }
    }, 'json');
});*/
$("body").on('click', '.follow', function (e) {
    e.preventDefault();
    var self = $(this);
    var url = self.attr('href');
    $.get(url, function (o) {
        if (o.Result == 1) {
            self.parent().parent().fadeOut('slow');
            refreshFollowing();
        }
        else
            alert('follow başarısız');
    }, 'json');
    
})
$("body").on('click', '.stop-follow', function (e) {
    e.preventDefault();
    var self = $(this);
    var url = self.attr('href');
    $.get(url, function (o) {
        if (o.Result == 1) {
            self.parent().parent().parent().parent().fadeOut('slow');
        }
        else
            alert('takibi bırak başarısız');
    }, 'json');
})
