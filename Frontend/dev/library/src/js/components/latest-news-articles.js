export class LatestNewsArticles {

    static setHeaderHeight() {
        $('.latest-news-articles').each(function(index, element) {

            let largestHeight = 0;

            $(element).find('.news-card .card-title').each(function(index, subElement) {
                $(subElement).css('height', '');
                largestHeight = $(subElement).height() > largestHeight ? $(subElement).height() : largestHeight;
            });

            $(element).find('.news-card .card-title').each(function(index, subElement) {
                $(subElement).css('height', `${largestHeight}px`);
            });
        });
    }

    static main() {

        $('.latest-news-articles').on('init', function(event, slick) {
            LatestNewsArticles.setHeaderHeight();
        });

        $('.latest-news-articles').on('setPosition', function(slick) {
            LatestNewsArticles.setHeaderHeight();
        });

        $('.latest-news-articles').slick({
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
                }
            ]
        });
    }
}