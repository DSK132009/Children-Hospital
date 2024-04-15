export class VideoEmbedController {

    static main() {

        $('.video-embed-btn').each(function(index, element) {
            $(element).on('click', function(event) {
                event.preventDefault();

                // disable transition if inside slick slider
                const slickTrack = $(this).closest('.slick-track');
                const slickList = $(this).closest('.slick-list');
                
                if (slickTrack.length > 0) {
                    slickTrack.addClass('modal-open');
                    slickList.addClass('modal-open');
                }

                const modal = $(this).closest('.video-embed').find('.video-embed-content');
                const frame = $(modal).find('iframe');

                $(modal).addClass('open');
				$(frame).attr('src', $(frame).attr('data-src'));
            });
        });

        $('.video-overlay').each(function(index, element) {
            $(element).on('click', function(event) {

                // enable transition if inside slick slider
                const slickTrack = $(this).closest('.slick-track');
                const slickList = $(this).closest('.slick-list');
                
                if (slickTrack.length > 0) {
                    slickTrack.removeClass('modal-open');
                    slickList.removeClass('modal-open');
                }

                const modal = $(this).closest('.video-embed').find('.video-embed-content');
                const frame = $(modal).find('iframe');

                
                $(modal).removeClass('open');
				$(frame).removeAttr('src');
            });
        });

        $('.close-frame').each(function(index, element) {
            $(element).on('click', function(event) {
                event.preventDefault();

                // enable transition if inside slick slider
                const slickTrack = $(this).closest('.slick-track');
                const slickList = $(this).closest('.slick-list');
                
                if (slickTrack.length > 0) {
                    slickTrack.removeClass('modal-open');
                    slickList.removeClass('modal-open');
                }

                const modal = $(this).closest('.video-embed').find('.video-embed-content');
                const frame = $(modal).find('iframe');

                $(modal).removeClass('open');
				$(frame).removeAttr('src');
            });
        });

        let modalElement = $('.video-embed-content');

        let firstFocusable = $(modalElement).find('.close-frame');
		let lastFocusable = $(modalElement).find('.btn-link.sr-only');

        // Set focus on first focusable item in modal
        modalElement.on('shown.bs.modal', function() {
            firstFocusable.focus();
        });

        // If reverse tabbing go to end of modal
        firstFocusable.on('keydown', function(event) {
            if ((event.key === 'Tab' || event.code === 'Tab') && event.shiftKey) {
                event.preventDefault();

                lastFocusable.focus();
            }
        });

        // If tabbing at end of modal, return back to beginning
        lastFocusable.on('keydown', function(event) {
            if ((event.key === 'Tab' || event.code === 'Tab') && !event.shiftKey ) {
                event.preventDefault();
                firstFocusable.focus();
            }
        });
    }
}