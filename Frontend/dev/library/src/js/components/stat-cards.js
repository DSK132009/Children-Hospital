export class StatCards {

    static main() {

        let didAnimate = false;
        const statCards = $('.stat-cards');
        const totalStats = $('.stat-cards .card').length;
        if (totalStats < 5) { $('.stat-cards').addClass('less-than-5'); }

        if (statCards.length) {
            statCards.slick({
                dots: true,
                arrows: false,
                slidesToShow: 4,
                slidesToScroll: 4,
                infinite: true,
                responsive: [
                    {
                        breakpoint: 992,
                        settings: {
                            slidesToShow: 2,
                            slidesToScroll: 2,
                        }
                    }
                ]
            });

            $(window).scroll();
    
            // animate numbers when visible
            $(window).scroll(function() {
                var top_of_element = $(".stat-cards").offset().top;
                var bottom_of_element = $(".stat-cards").offset().top + $(".stat-cards").outerHeight();
                var bottom_of_screen = $(window).scrollTop() + $(window).innerHeight();
                var top_of_screen = $(window).scrollTop();
            
                if ((bottom_of_screen > top_of_element) && (top_of_screen < bottom_of_element)){
                    if (!didAnimate) {
                        animateNumbers();
                        didAnimate = true;
                    }
                }
            });
        }
        

        function animateNumbers() {
            $('.stat-card .card-title').each(function () {
                var countTo = parseFloat($(this).attr('data-num').replace(/,/g, ''));

                $(this).prop('Counter',0).animate({                    
                    Counter: countTo
                }, {
                    duration: 1000,
                    easing: 'swing',
                    step: function() {
                        $(this).text(commaSeparateNumber(Math.floor(this.Counter)));
                      },
                      complete: function() {
                        $(this).text(commaSeparateNumber(this.Counter));
                        //alert('finished');
                      }
                });

            });
        }

        function commaSeparateNumber(val) {
            while (/(\d+)(\d{3})/.test(val.toString())) {
              val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
            }
            return val;
        }
    }
}