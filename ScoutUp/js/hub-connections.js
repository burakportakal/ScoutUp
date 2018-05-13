$(document).ready(function() {
    $(function() {
        // Post beğeni bildirimini gönderme kısmı
        if ($(".btn").length > 0) {
            var postClient = $.connection.postHub;

            postClient.client.updateLikeCount = function(post) {
                var self = $("#" + post.PostId);
                self.html('<i class="icon ion-thumbsup"></i> Beğen ' + post.LikeCount);
            };

            $("body").on("click",
                ".btn",
                function() {
                    var code = $(this).attr("id");
                    var self = $(this);
                    var clas = self.attr('class');
                    var isLiked = false;
                    if (navigator.userAgent.search("MSIE") >= 0) {
                        isLiked = clas.includes('text-red');
                    } else {
                        isLiked = clas.endsWith('text-red');
                    }

                    if (isLiked) {
                        self.removeClass('text-red');
                        self.addClass('text-green');
                    } else {
                        self.removeClass('text-green');
                        self.addClass('text-red');
                    }
                    var userid = $('#UserId').val();
                    postClient.server.like(code, userid);
                });
        }
    });
    $(function() {
        //Mesajlaşma hub ı
        var messageHub = $.connection.messageHub;
        //ilk girişte online kullanıcıları çeker
        messageHub.client.onlineUsers = function(data) {
            for (var i = 0; i < data.length; i++) {
                var html =
                    '<li id="online-user-' +
                        data[i].UserId +
                        '"><a href="/users/messages/' +
                        data[i].UserId +
                        '" class="chat" data-id="' +
                        data[i].UserId +
                        '" title="' +
                        data[i].UserName +
                        '"><img src="' +
                        data[i].UserProfilePhoto +
                        '" alt="user" class="img-responsive profile-photo" /><span class="online-dot"></span></a></li>';
                $('#online-users').append(html);
            }
        };
        //bağlantım olan bir kullanıcı pencere açıkken online olduğunda onu online listesine ekler
        messageHub.client.updateOnlineUsers = function(data) {
            var isListed = $('#online-user-' + data.UserId).attr('id') == null;
            if (isListed) {
                var html =
                    '<li id="online-user-' +
                        data.UserId +
                        '"><a href="/users/messages/' +
                        data.UserId +
                        '" class="chat" data-id="' +
                        data.UserId +
                        '" title="' +
                        data.UserName +
                        '"><img src="' +
                        data.UserProfilePhoto +
                        '" alt="user" class="img-responsive profile-photo" /><span class="online-dot"></span></a></li>';
                $('#online-users').append(html);
            }
        };
        //kullanıcının tüm penceleri kapandıysa bir bağlantısı kalmadıysa diğer kullanıcılara 10 saniye sonra offline olur.
        messageHub.client.updateUserOffline = function(data) {
            var self = $('#online-user-' + data.UserId);
            self.remove();
        }
        //bir kullanıcı şu an ki kullanıcıya mesaj gönderdiğinde çalışacak
        messageHub.client.recieveMessage = function(data) {
            var self = $('#list-' + data.UserId);
            //zaten tab var mı ?
            if (self.attr('id') == null) {
                //tab yok yeni tab oluşaçak
                //messageController bu tab açma olaylarını hallediyor
                var g1 = $.get('/message/MessageTabReciever/',
                    data,
                    function(e) {
                        var leftContactList = $('#left-contact-list');
                        leftContactList.append(e);
                        self = $('#list-' + data.UserId);
                        self.removeClass('active');
                    });
                var g2 = $.get('/message/messagetabpane/' + data.UserId,
                    function(e) {
                        var tabPane = $('#tab-pane');
                        tabPane.append(e);
                        id = 'contact-' + data.UserId;
                        $('#' + id).removeClass('active');

                    });
                //soltaki tab ve ona bağlı mesaj tabları açıldığında çalışır. $.when olmasa çok hızlı çalışıp geçiyor çalışmıyor.
                $.when(g1, g2).done(function() {
                    $.get('/message/MessageReciever/',
                        data,
                        function(e) {
                            $('#contact-' + data.UserId).children().children().append(e);
                            $('#chat-alert-' + data.UserId).text("1");
                        });
                });
            } else { // zaten bir tab açık yenisine gerek yok açık olan tab a sadece mesajları gönderelim
                $.get('/message/MessageReciever/',
                    data,
                    function(e) {
                        $('#contact-' + data.UserId).children().children().append(e);
                        var newMessageCount = $('#chat-alert-' + data.UserId).text();
                        if ($('#list-' + data.UserId).hasClass("active") == false) {
                            if (newMessageCount == "") {
                                newMessageCount = 1;
                            } else {
                                newMessageCount++;
                            }
                            $('#chat-alert-' + data.UserId).text(newMessageCount);
                        }
                        var x = document.getElementById('tab-pane');
                        x.scrollTop = x.scrollHeight;
                    });

            }
            //tab a tıklandığında bildirimi siler
            $('body').on('click',
                '.left-tab',
                function() {
                    var self = $(this);
                    $('li').removeClass('active');
                    $('div').removeClass('active');
                    var id = self.attr('id').replace('list-', '');
                    $('#contact-' + id).addClass('active');
                    self.addClass('active');
                    $('#chat-alert-' + id).text("");
                });

        }
        //mesaj gönderme kısmı
        $("body").on("click",
            '#buton',
            function() {
                var self = $(this);
                var userid = $('#UserId').val();
                var message = $('.input-group').children().val();
                var targetUser = $(".left-tab.active").attr('id').replace("list-", "");
                var data = { UserId: userid, MessageText: message, RecieverUserId: targetUser };
                $.get('/message/MessageSender/',
                    data,
                    function(e) {
                        $('#contact-' + targetUser).children().children().append(e);
                        var x = document.getElementById('tab-pane');
                        x.scrollTop = x.scrollHeight;
                        $('.input-group').children().val("");
                    });

                //sunucuda ki fonksiyonu ateşler.o da client fonksiyonunu ateşler.
                messageHub.server.sendMessage(userid, targetUser, message);
            });

    });
    $(function() {

        // Bildirim alma ve gönderme

        var notification = $.connection.notificationHub;
        var newNotificitaionsToUpdate = [];
        //Girişte online değilken gelen bildirimler gelir
        notification.client.refreshNotification = function(data) {
            for (var i = 0; i < data.length; i++) {
                $("#bildirim").append("<li><a href='" +
                    data[i].NotificationLink +
                    "'>" +
                    data[i].UserNotificationsMessage +
                    "</a></li>");
            }
            if (data.length > 0) {
                $("#bildirimler").html('Bildirimler ' +
                    data.length +
                    '<span><img src="../../images/down-arrow.png" alt="" /></span>');
            }
            $("body").on("click",
                ".dropdown-toggle",
                function() {
                    var self = $(this);
                    notification.server.updateNotification(data);
                    $("#bildirimler").html('Bildirimler<span><img src="../../images/down-arrow.png" alt="" /></span>');
                });

        }

        // sitede iken gelen bildirilem
        notification.client.updateNotification = function(data) {
            $("#bildirim").append("<li><a href='" +
                data.NotificationLink +
                "'>" +
                data.UserNotificationsMessage +
                "</a></li>");
            newNotificitaionsToUpdate.push(data);
            $("#bildirimler").html('Bildirimler ' +
                newNotificitaionsToUpdate.length +
                '<span><img src="../../images/down-arrow.png" alt="" /></span>');
            $("body").on("click",
                ".dropdown-toggle",
                function() {
                    var self = $(this);
                    notification.server.updateNotification(newNotificitaionsToUpdate);
                    newNotificitaionsToUpdate = [];
                    $("#bildirimler").html('Bildirimler<span><img src="../../images/down-arrow.png" alt="" /></span>');
                });
        }

        // Tüm hublara bağlantıyı başlatır
        $.connection.hub.start().done(function() {

            // Gönderi beğendi bildirimini karşı kullanıcıya gönderir
            $("body").on("click",
                ".btn",
                function() {
                    var self = $(this);
                    var userid = self.attr("data-id");
                    var postid = self.attr("id");
                    var clas = self.attr('class');
                    var isLiked = false;
                    if (navigator.userAgent.search("MSIE") >= 0) {
                        isLiked = clas.includes('text-red');
                    } else {
                        isLiked = clas.endsWith('text-red');
                    }
                    if (!isLiked) {
                        var UserName = $('#UserName').val();
                        var UserSurname = $('#UserSurname').val();
                        notification.server.sendNotification(userid,
                            UserName + " " + UserSurname + " gönderini beğendi",
                            "",
                            "/users/index/#" + postid);
                    }
                });
            //Takip edildi bildirimini karşı kullanıcıya gönderir
            $("body").on("click",
                ".follow",
                function() {
                    var self = $(this);
                    var userid = self.attr("data-id");
                    var currentUserId = $('#UserId').val();
                    var clas = self.attr('class');
                    var UserName = $('#UserName').val();
                    var UserSurname = $('#UserSurname').val();
                    notification.server.sendNotification(userid,
                        UserName + " " + UserSurname + " seni takip etmeye başladı.",
                        "",
                        "/users/index/" + currentUserId);
                });
            //yorum yapıldı bildirimini karşı kullanıcıya gönderir
            $(".form-control").keypress(function(e) {
                if (e.which == 13) {
                    var self = $(this);
                    var postid = self.attr("data-id");
                    var currentUserId = $('#UserId').val();
                    var UserName = $('#UserName').val();
                    var UserSurname = $('#UserSurname').val();
                    notification.server.sendCommentNotification(postid,
                        UserName + " " + UserSurname + " gönderine yorum yaptı.",
                        "",
                        "/users/index#post" + postid);
                }
            });

        });
    });
});