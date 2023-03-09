jQuery(document).ready(function ($) {
  var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent);
  if (iOS) {
    $(document.body).addClass('ios');
  };
  //input date
  if ($('.input-date').length) {
    var pkcont = 'body';
    if ($('.picker-container').length) {
      pkcont = '.picker-container';
    }
    $('.input-date').datepicker({
      todayHighlight: true,
      startDate: "0d",
      container: pkcont
    });
  }

  //Show/Hide scroll-top on Scroll
  // hide #back-top first
  $("#scroll-top").hide();
  // fade in #back-top
  $(function () {
    $(window).scroll(function () {

      if ($(this).scrollTop() > 100) {
        $('#scroll-top').fadeIn();
      } else {
        $('#scroll-top').fadeOut();
      }

    });
    // scroll body to 0px on click
    $('#scroll-top').click(function () {
      $('body,html').animate({
        scrollTop: 0
      }, 1000);
    });


  });
  $('.navbar-toggle').on('click', function (e) {
    $(this).toggleClass('open');
    $('body').toggleClass('menuin');
  });
  $('.nav-overlay').on('click', this, function (e) {
    $('.navbar-toggle').trigger('click');
  });
  $('.nav-ul .close > a').on('click', this, function (e) {
    $('.navbar-toggle').trigger('click');
  });
  $('.dropdown').hover(function () {
    var parent = $(this);
    parent.find('.sub-menu-wrap').stop().slideDown(300, function () {
      $(this).addClass('open');
    });
  }, function () {
    $(this).children('.sub-menu-wrap').stop().slideUp(300, function () {
      $(this).removeClass('open');
    });
  });
  $('.collapse').on('click', '.collapse-heading', function () {
    var container = $(this).parent('.collapse');
    $(container).siblings().removeClass('on').find('.collapse-body').slideUp();
    $(container).find('.collapse-body').is(':visible') ?
      $(container).removeClass('on').find('.collapse-body').slideUp() :
      $(container).addClass('on').find(':hidden').slideDown();

  });
  stickyHeader();
  //    $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
  $(window).scroll(function () {
    //        $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
    stickyHeader();
  });

  function stickyHeader() {
    var hdOffsetTop = $("#header").offset().top;
    if ($(window).scrollTop() > $("#header").height()) {
      $("#header").addClass("sticky");
    } else {
      $("#header").removeClass("sticky");
    }
  }

});

function loadGoogleMap() {
  var mapElement = document.getElementById('map-canvas');
  if (mapElement == null) return;
  google.maps.event.addDomListener(window, 'load', initmap);
  var infowindow = new google.maps.InfoWindow({
    size: new google.maps.Size(150, 50)
  });
  var map;
  var gmarkers = [];

  var icon = {
    //url: 'images/map-icon.png',
    // This marker width , high.
    //scaledSize: new google.maps.Size(30, 35),
    // The origin for this image
    origin: new google.maps.Point(0, 0),
    // The anchor for this image is the base of the flagpole
    anchor: new google.maps.Point(12.5, 35)
  };

  function createMarker(latlng, title) {
    var marker = new google.maps.Marker({
      position: latlng,
      title: title,
      //icon: icon
    });
    infowindow.setContent(title);
    infowindow.open(map, marker);
    google.maps.event.addListener(marker, 'click', function () {
      infowindow.setContent(title);
      infowindow.open(map, marker);
      google.maps.event.addListener(map, 'click', function () {
        infowindow.close();
      });
    });
    gmarkers.push(marker);
    return marker;
  }

  function callinfobox(i) {
    google.maps.event.trigger(gmarkers[i], "click");
  }

  function deleteMarkers() {
    clearMarkers();
    gmarkers = [];
  }
  // Sets the map on all markers in the array.
  function setMapOnAll(map) {
    for (var i = 0; i < gmarkers.length; i++) {
      gmarkers[i].setMap(map);
    }
  }

  // Removes the markers from the map, but keeps them in the array.
  function clearMarkers() {
    setMapOnAll(null);
  }

  var bounds = new google.maps.LatLngBounds();
  function initmap() {
    var mainLatlng = new google.maps.LatLng(10.776924, 106.654758);
    var mapOptions = {
      // How zoomed in you want the map to start at (always required)
      zoom: 14,
      disableDefaultUI: true,
      scrollwheel: false,
      zoomControl: true,
      draggable: true,
      zoomControlOptions: {
        position: google.maps.ControlPosition.TOP_RIGHT
      },
      // The latitude and longitude to center the map (always required)
      center: mainLatlng,
      // How you would like to style the map. 
      // This is where you would paste any style found on Snazzy Maps.
    };

    // Create the Google Map using our element and options defined above
    map = new google.maps.Map(mapElement, mapOptions);
    createMarker(mainLatlng, '<a href="https://mona-media.com/dich-vu/thiet-ke-website-chuyen-nghiep/" title="Công ty thiế kế website chuyên nghiệp">Thiết kế website</a>&nbsp;<img src="http://mona-media.com/logo.png" style="width:20px;vertical-align:sub" alt="MonaMedia"> <strong>Mona Media</strong>').setMap(map);
  }

  
}
