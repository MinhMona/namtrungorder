jQuery(document).ready(function($) {
	$('.navbar-toggle').on('click',function(e){
        $(this).toggleClass('open');
        $('body').toggleClass('menuin');
    });
    $('.nav-overlay').on('click',this,function(e){
        $('.navbar-toggle').trigger('click');
    });
    // $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
    // $(window).scroll(function () {
    //     $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
    // });

    $(".nav-ul li a").click(function(event) {
        $('.nav-active').removeClass('nav-active');
        $(this).addClass('nav-active');
    });

    $(".search-form-detail ul li a").click(function(event) {
        $('.search-active').removeClass('search-active');
        $(this).addClass('search-active');
    });
    $(".new-product a").click(function(event) {
        $('.new-active').removeClass('new-active');
        $(this).addClass('new-active');
    });


    if ($('#slider').length){
        $("#slider").slick({
            dots: true,
            autoplay: false,
            autoplaySpeed: 500,
            slidesToShow: 5,
            slidesToScroll: 5,
            responsive: [
            {
              breakpoint: 1024,
              settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: true
              }
            },
            {
              breakpoint: 750,
              settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                dots: true,
                autoplay: true,
              }
            },
            {
              breakpoint: 480,
              settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                dots: true,
                autoplay: true,
              }
            }
          ]
        })
    }





    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.scroll-top').fadeIn();
            } else {
                $('.scroll-top').fadeOut();
            }
        });
        // scroll body to 0px on click
        $('.scroll-top').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 1000);
        });
    });
});