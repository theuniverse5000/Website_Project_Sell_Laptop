var swiper = new Swiper(".mySwiper", {
    initialSlide: 1,
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: "auto",
    // slidesPerView: 1,
    spaceBetween: 30,
    setWrapperSize: true,
    roundLengths: true,
    autoHeight: false,
    autoplay: false,
    loop: false,
    coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 100,
        modifier: 2.5,
        slideShadows: false,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});
//
var swiperEnd;

function initializeSwiper() {
    // Kiểm tra kích thước cửa sổ và cấu hình Swiper dựa trên điều kiện
    if ($(window).width() < 768) {
        swiperEnd = new Swiper(".mySwiperEnd", {
            slidesPerView: 1,
            spaceBetween: 30,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
        });
    } else {
        swiperEnd = new Swiper(".mySwiperEnd", {
            slidesPerView: 3,
            spaceBetween: 30,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
        });
    }
}

initializeSwiper();

$(window).on("resize", function () {
    if (swiperEnd !== undefined && swiperEnd !== null) {
        swiperEnd.destroy();
        swiperEnd = null;
    }
    initializeSwiper();
});