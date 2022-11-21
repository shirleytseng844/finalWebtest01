
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


    // Header carousel
/*   $(".header-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1500,
        items: 1,
        dots: true,
        loop: true,
        nav : true,
        navText : [
            '<i class="bi bi-chevron-left"></i>',
            '<i class="bi bi-chevron-right"></i>'
        ]
    });*/


/*    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        margin: 24,
        dots: false,
        loop: true,
        nav : true,
        navText : [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsive: {
            0:{
                items:1
            },
            992:{
                items:2
            }
        }
    });
    
})(jQuery);*/

  //開啟編輯菜單分頁 
  function editMenu(){
    let Menu = document.getElementById('editMenu')
    window.open('./editMenu copy.html', '編輯菜單', config='height=800,width=600');
  }

  function additem(){
    let additem = document.getElementById('addProduct')
    window.open('./addProduct.html', '新增菜單', config='height=800,width=600');
}

  function doFirst(){
    // 跟 HTML 畫面產生關連，再建事件聆聽功能
      document.getElementById('upload_file').onchange = fileChange
}
    function fileChange(){

        let file = document.getElementById('upload_file').files[0];

        console.log(file);

        let readFile = new FileReader();

        readFile.readAsDataURL(file);

        readFile.addEventListener('load',function(){
            let image = document.getElementById('menuPhoto');
            image.src = readFile.result;
            image.style.maxWidth = '200px';
            image.style.maxHeight = '200px';
        })
    };
    window.addEventListener('load', doFirst);

  //預覽商店管理者頭像 End