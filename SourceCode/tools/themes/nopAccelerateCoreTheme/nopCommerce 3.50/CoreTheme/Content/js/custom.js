/*Custom js for stick header menu*/
//http://www.jqueryscript.net/menu/Sticky-Navigation-Bar-with-jQuery-Bootstrap.html

$(document).ready(function () {
    $(window).bind('scroll', function() {
        //var navHeight = $("nav.top-menu").height();
        var navHeight = $("div.header").height();
        var navWidth = $("div.header").width();
        ($(window).scrollTop() > navHeight) ? $('.navbar.navbar-inverse').addClass('goToTop').width(navWidth) : $('.navbar.navbar-inverse').removeClass('goToTop');
    });
});

$(window).resize(function () {    
    $(".navbar.navbar-inverse").width("100%");
    $(window).scrollTop(1);
});
$(document).ready(function () {
    $('#exit ').click(function (e) {
        $('.responsive').hide();
        $('.master-wrapper-page').css('margin-top', '0');
        $('.header-links').css('margin-top', '20px');
    });
});