

jQuery(document).ready(function($){
	var window_w = $(window).width();
	var window_h = $(window).height();
	
	$(".scroll-to-sec,.scroll-to-sec a").click(function(e){
		e.preventDefault();
		var target = $(this).attr("href");
		$('body,html').animate({scrollTop:$(target).offset().top - top_offset},500);
	});
	
	
	$(".use-bg").each(function(){
		var bg = $(this).attr("data-bg");
		if(bg != "" ){
			$(this).css({"background-image":"url("+ bg +")"});
		}
	});
	
		 
	$('.menu-icon').click(function(e){
		e.preventDefault();
		$(this).toggleClass('active');
		$('body,#main-nav').toggleClass('push');
	});
	
    $('#main-nav:after').click(function(e){
		e.preventDefault();
		$(this).toggleClass('active');
		$('body,#main-nav').toggleClass('push');
	 });
	
	 
	 
	//Checking Click outside a element
	$(document).mouseup(function (e){
        
        var mobile_menu_ico = $(".menu-icon");

        if ((!mobile_menu_ico.is(e.target) && mobile_menu_ico.has(e.target).length === 0) )
        {
            $("body,#main-nav").removeClass("push");
            $(".menu-icon").removeClass("active");
        }
		
    });
	
	
	$("#home-experience .slider").slick({
	  dots: true,
	  arrows:true,
	  prevArrow:'<span class="slick-arrow slick-prev"><i class="fa fa-angle-left"></i></span>',
	  nextArrow:'<span class="slick-arrow slick-next"><i class="fa fa-angle-right"></i></span>',
	  infinite: true,
	  slidesToShow: 1,
	  slidesToScroll: 1,
	});
	
	$('#testimonials .slider').slick({
	  dots: true,
	  arrows:false,
	  infinite: true,
	  slidesToShow: 1,
	  slidesToScroll: 1,
	});
	
	$(window).on('scroll',function(){
		var scrollTop = $(window).scrollTop();

        $(".animate-num").each(function () {
            var $this = $(this);
            var elementOffset = $this.offset().top;
            distance = (elementOffset - scrollTop);
            if ((distance <= window_h - 100) && !$this.hasClass("done")) {
                num = parseInt($this.text());
                $this.animateNumber({
					number: num,
					//numberStep: comma_separator_number_step

				},3000);
                $this.addClass("done");
            }

        }); //End each	
		
		$('.process-bar').each(function(){
			var $this = $(this);
            var elementOffset = $this.offset().top;
            distance = (elementOffset - scrollTop);
            if ((distance <= window_h - 100) && !$this.hasClass("done")) {
                var percent = $this.data('percent');
				$this.find('span').animate({"width":percent},2000).addClass('done');
            }
		});
	});
	
	if(window_w < 767){
		$("#main-nav-area").sticky({
			zIndex:999,
			topSpacing:0
		})
	} //Mobile < 767
	
	$(window).on('resize',function(){
		window_w = $(window).width();
		if(window_w < 767){
			$("#main-nav-area").sticky({
				zIndex:999,
				topSpacing:0
			})
		} //Mobile < 767
	});
	
	$(window).on('load',function(){
		/*
		$('.hover-img').each(function(){
			var img_h = $(this).find('img').height();
			$(this).height(img_h/2);
		});*/
		
	}); //End on load
	
	$('.hover-img img').each(function(){
			var img_h = $(this).height();
			$(this).parent().height(img_h/2);
			
	});
	
	$('.hover-img img').on('load', function() {
			var img_h = $(this).height();
			$(this).parent().height(img_h/2);
			console.log(img_h);
	});
	
}) //End jQuery





