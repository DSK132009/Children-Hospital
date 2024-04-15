export class VideoSection {

    static main() {


        if (window.innerWidth < 992) {
            $('.video-layout').slick({
                dots: false,
                arrows: false,
                slidesToShow: 4,
                slidesToScroll: 4,
                infinite: true,
                responsive: [
                    {
                        breakpoint: 992,
                        settings: {
                            dots: true,
                            slidesToShow: 2,
                            slidesToScroll: 2,
                        }
                    },
                    {
                        breakpoint: 768,
                        settings: {
                            dots: true,
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }
                ]
            });
        }
        
        window.addEventListener('resize', function() {
            if (window.innerWidth < 992) {
                if (!$('.video-layout').hasClass('slick-slider')) {
                    $('.video-layout').slick({
                        dots: false,
                        arrows: false,
                        slidesToShow: 4,
                        slidesToScroll: 4,
                        infinite: true,
                        responsive: [
                            {
                                breakpoint: 992,
                                settings: {
                                    dots: true,
                                    slidesToShow: 2,
                                    slidesToScroll: 2,
                                }
                            },
                            {
                                breakpoint: 768,
                                settings: {
                                    dots: true,
                                    slidesToShow: 1,
                                    slidesToScroll: 1
                                }
                            }
                        ]
                    });
                } else {
                    return false;
                }
            } else {
                if ($('.video-layout').hasClass('slick-slider')) {
                    $('.video-layout').slick('unslick');
                }
            }
        });
    }
}