export class GenericHero {
    static main() {
        const windowSize =  window.innerWidth;
        $('.generic-hero').each(function(index, element) {
            // default to desktop img if no mobile img is passed in;
            const mobileSrc = $(element).attr('data-mobile-src') || $(element).attr('data-desktop-src');
            const desktopSrc = $(element).attr('data-desktop-src');
            if (windowSize < 992) {
                $(element).css({"background-image": `url('${mobileSrc}')`});
            } else {
                $(element).css({"background-image": `url('${desktopSrc}')`});
            }
            const mediaQuery = '(max-width: 992px)';
            const mediaQueryList = window.matchMedia(mediaQuery);
            mediaQueryList.addEventListener('change', event => {
                if (event.matches) {
                    $(element).css({"background-image": `url('${mobileSrc}')`});
                } else {
                    $(element).css({"background-image": `url('${desktopSrc}')`});
                }
            });
        });
    }
}