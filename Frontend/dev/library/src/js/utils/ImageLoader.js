
/**
 * Lazy loads images that are set up properly.
 * For img tags: 
 *      <img
            class="img-load-lazy"
            src="small-placeholder.jpeg"
			data-src="full-sized-actual-img.jpg"
			data-srcset="full-sized-actual-img.jpg 2x, mobile-sized-actual-img.jpg 1x"
            alt="">
            
    For BG Images: <div class="lazy-load-bg" style="background-image: url('/my-image.jpeg')"></div> 

    more info: https://developers.google.com/web/fundamentals/performance/lazy-loading-guidance/images-and-video
 */
export class ImageLoader {

    constructor(options = {}) {
        this.queue = [];
        this.backgroundQueue = [];
        this.lazyImageObserver = null;
        this.lazyBgObserver = null;
        this.options = options;
    }

    initialize() {
        this.getImagesToLazyLoad();

        if (!("IntersectionObserver" in window)) {
            this.abort();
            return;
        }

        this.onContentLoaded();
        this.setGenericPlaceholder();

        this.queue.forEach((lazyImage) => {
            this.lazyImageObserver.observe(lazyImage);
        });

        this.backgroundQueue.forEach((lazyBg) => {
            this.lazyBgObserver.observe(lazyBg);
        });
    }

    getImagesToLazyLoad() {
        this.queue = document.querySelectorAll('.img-load-lazy');
        this.backgroundQueue = document.querySelectorAll('.lazy-load-bg');
    }

    setGenericPlaceholder() {
        if (this.options.genericImagePlaceholder) {
            this.queue.forEach((img) => {
                img.setAttribute('src', this.options.genericImagePlaceholder);
            });
        }

        if (this.options.genericBgPlaceholder) {
            this.backgroundQueue.forEach((bg) => {
                bg.style.backgroundColor = this.options.genericBgPlaceholder;
            });
        }
    }

    onContentLoaded() {
        this.lazyImageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    let lazyImage = entry.target;
                    lazyImage.src = lazyImage.dataset.src;
                    lazyImage.srcset = lazyImage.dataset.srcset;
                    lazyImage.classList.remove('img-load-lazy');
                    this.lazyImageObserver.unobserve(lazyImage);
                }
            });
        });

        this.lazyBgObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.remove('lazy-load-bg');
                    this.lazyBgObserver.unobserve(entry.target);
                }
            });
        });
    }

    abort() {
        for (let i = 0; i < this.queue.length; i++) {
            this.queue[i].setAttribute('src', this.queue[i].getAttribute('data-src'));
            this.queue[i].classList.remove('img-load-lazy');
        }

        for (let i = 0; i < this.backgroundQueue.length; i++) {
            this.backgroundQueue[i].classList.remove('lazy-load-bg');
        }
    }

}
