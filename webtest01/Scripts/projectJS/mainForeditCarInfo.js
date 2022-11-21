////(function ($) {
////    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Initiate the wowjs
    new WOW().init();


    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 45) {
            $('.nav-bar').addClass('sticky-top');
        } else {
            $('.nav-bar').removeClass('sticky-top');
        }
    });
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


//    // Header carousel
//    $(".header-carousel").owlCarousel({
//        autoplay: true,
//        smartSpeed: 1500,
//        items: 1,
//        dots: true,
//        loop: true,
//        nav : true,
//        navText : [
//            '<i class="bi bi-chevron-left"></i>',
//            '<i class="bi bi-chevron-right"></i>'
//        ]
//    });


//    // Testimonials carousel
//    $(".testimonial-carousel").owlCarousel({
//        autoplay: true,
//        smartSpeed: 1000,
//        margin: 24,
//        dots: false,
//        loop: true,
//        nav : true,
//        navText : [
//            '<i class="bi bi-arrow-left"></i>',
//            '<i class="bi bi-arrow-right"></i>'
//        ],
//        responsive: {
//            0:{
//                items:1
//            },
//            992:{
//                items:2
//            }
//        }
//    });
    
//})(jQuery);



  // 修改餐車資訊預覽上傳多張圖片
//$("#carsPhoto").change(function(){
//    $("#carsPhoto_imgs").html(""); // 清除預覽
//    readURL(this);
//  });
  
//  function readURL(input){
//    if (input.files && input.files.length >= 0) {
//      for(var i = 0; i < input.files.length; i ++){
//        var reader = new FileReader();
//        reader.onload = function (e) {
//          var img = $("<img width='150' height='100'>").attr('src', e.target.result);
//          $("#carsPhoto_imgs").append(img);
//        }
//        reader.readAsDataURL(input.files[i]);
//      }
//    }else{
//       var noPictures = $("<p>目前沒有圖片</p>");
//       $("#carsPhoto_imgs").append(noPictures);
//    }
//  }
  // 修改餐車資訊預覽上傳多張圖片 End