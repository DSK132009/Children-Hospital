export class ScrollToTop {

    static main() {
        
    	// Scroll To Top
    	$('.scrollToTop').on('click', function(){
    	    $('html, body').stop().animate({scrollTop: 0}, 500, 'linear');
    	    return false;
    	});

       scrollToTopButton();

    	// Fade in/out Scroll To Top button after 500px
    	function scrollToTopButton() {
    	    var button  = $('.scrollToTop'), 
    	        view = $(window),
    	        timeoutKey = -1;

    	    // page load
    	    if (view.scrollTop() > 500) {
    	        button.fadeIn();
    	    }
    	    else {
    	        button.fadeOut();
    	    }

    	    $(document).on('scroll', function() {
    	        if(timeoutKey) {
    	            window.clearTimeout(timeoutKey);
    	        }

    	        timeoutKey = window.setTimeout(function(){
    	            if (view.scrollTop() > 500) {
    	                button.fadeIn();
    	            }
    	            else {
    	                button.fadeOut();
    	            }
    	        }, 100);
    	    });
    	}
	    
    }

}
