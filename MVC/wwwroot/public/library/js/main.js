'use strict';

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

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
var ImageLoader = function () {
    function ImageLoader() {
        var options = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};

        _classCallCheck(this, ImageLoader);

        this.queue = [];
        this.backgroundQueue = [];
        this.lazyImageObserver = null;
        this.lazyBgObserver = null;
        this.options = options;
    }

    _createClass(ImageLoader, [{
        key: 'initialize',
        value: function initialize() {
            var _this = this;

            this.getImagesToLazyLoad();

            if (!("IntersectionObserver" in window)) {
                this.abort();
                return;
            }

            this.onContentLoaded();
            this.setGenericPlaceholder();

            this.queue.forEach(function (lazyImage) {
                _this.lazyImageObserver.observe(lazyImage);
            });

            this.backgroundQueue.forEach(function (lazyBg) {
                _this.lazyBgObserver.observe(lazyBg);
            });
        }
    }, {
        key: 'getImagesToLazyLoad',
        value: function getImagesToLazyLoad() {
            this.queue = document.querySelectorAll('.img-load-lazy');
            this.backgroundQueue = document.querySelectorAll('.lazy-load-bg');
        }
    }, {
        key: 'setGenericPlaceholder',
        value: function setGenericPlaceholder() {
            var _this2 = this;

            if (this.options.genericImagePlaceholder) {
                this.queue.forEach(function (img) {
                    img.setAttribute('src', _this2.options.genericImagePlaceholder);
                });
            }

            if (this.options.genericBgPlaceholder) {
                this.backgroundQueue.forEach(function (bg) {
                    bg.style.backgroundColor = _this2.options.genericBgPlaceholder;
                });
            }
        }
    }, {
        key: 'onContentLoaded',
        value: function onContentLoaded() {
            var _this3 = this;

            this.lazyImageObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        var lazyImage = entry.target;
                        lazyImage.src = lazyImage.dataset.src;
                        lazyImage.srcset = lazyImage.dataset.srcset;
                        lazyImage.classList.remove('img-load-lazy');
                        _this3.lazyImageObserver.unobserve(lazyImage);
                    }
                });
            });

            this.lazyBgObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.remove('lazy-load-bg');
                        _this3.lazyBgObserver.unobserve(entry.target);
                    }
                });
            });
        }
    }, {
        key: 'abort',
        value: function abort() {
            for (var i = 0; i < this.queue.length; i++) {
                this.queue[i].setAttribute('src', this.queue[i].getAttribute('data-src'));
                this.queue[i].classList.remove('img-load-lazy');
            }

            for (var _i = 0; _i < this.backgroundQueue.length; _i++) {
                this.backgroundQueue[_i].classList.remove('lazy-load-bg');
            }
        }
    }]);

    return ImageLoader;
}();

var SiteHeader = function () {
    function SiteHeader() {
        _classCallCheck(this, SiteHeader);
    }

    _createClass(SiteHeader, null, [{
        key: 'main',
        value: function main() {

            // On Desktop, show nav dropdown when mouseover event occurs
            $('.site-header .nav-item.dropdown').on('mouseover', function (event) {

                if (window.innerWidth < 1200) return;

                var listItem = event.currentTarget;

                if (!listItem.classList.contains('dropdown')) return;

                var position = listItem.getBoundingClientRect();
                var dropdown = listItem.querySelector('.dropdown-menu');

                if (position.left + dropdown.offsetWidth > window.innerWidth) {
                    dropdown.classList.add('right-align');
                } else {
                    dropdown.classList.remove('right-align');
                }

                listItem.querySelector('.nav-link.dropdown-toggle').classList.add('show');
                dropdown.classList.add('show');
            });

            // On Desktop, hide nav dropdown when mouse leave event occurs
            $('.site-header .nav-item.dropdown').on('mouseleave', function (event) {
                if (window.innerWidth < 1200) return;

                var listItem = event.currentTarget;

                if (!listItem.classList.contains('dropdown')) return;

                var dropdown = listItem.querySelector('.dropdown-menu');

                dropdown.classList.remove('show');
                listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');
            });

            // On first link keydown, tab display dropdown, shift tab hide dropdown
            $('.site-header .nav-item.dropdown .nav-link').on('keydown', function (event) {
                if (window.innerWidth < 1200) return;

                var listItem = event.target.closest('.nav-item.dropdown');
                var position = listItem.getBoundingClientRect();
                var dropdown = listItem.querySelector('.dropdown-menu');

                if ((event.key === 'Tab' || event.code === 'Tab') && event.shiftKey) {

                    dropdown.classList.remove('show');
                    listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');
                } else if (event.key === 'Tab' || event.code === 'Tab') {

                    if (position.left + dropdown.offsetWidth > window.innerWidth) {
                        dropdown.classList.add('right-align');
                    } else {
                        dropdown.classList.remove('right-align');
                    }

                    dropdown.classList.add('show');
                    listItem.querySelector('.nav-link.dropdown-toggle').classList.add('show');
                }
            });

            // On last link focus, hide dropdown when tabbed away
            $('.site-header .nav-item.dropdown .dropdown-menu li:last-child .dropdown-item').on('keydown', function (event) {

                if (window.innerWidth < 1200) return;

                if ((event.key === 'Tab' || event.code === 'Tab') && !event.shiftKey) {
                    var listItem = event.target.closest('.nav-item.dropdown');
                    var dropdown = listItem.querySelector('.dropdown-menu');

                    dropdown.classList.remove('show');
                    listItem.querySelector('.nav-link.dropdown-toggle').classList.remove('show');
                }
            });

            // On mobile menu btn click, open menu
            $('.site-header-menu-btn').on('click', function () {

                $('.site-header-search').removeClass('show');

                var searchIcon = $('.site-header-search-toggle').attr('data-icon');
                $('.site-header-search-toggle use').attr('xlink:href', searchIcon);

                $('.site-header-nav').toggleClass('show');

                if ($('.site-header-nav').hasClass('show')) {
                    var closeIcon = $(this).attr('data-close-icon');
                    $(this).find('use').attr('xlink:href', closeIcon);
                } else {
                    var icon = $(this).attr('data-icon');
                    $(this).find('use').attr('xlink:href', icon);
                }
            });

            // On mobile search btn click, open search display
            $('.site-header-search-toggle').on('click', function () {

                $('.site-header-search input').val('');
                $('.site-header-nav').removeClass('show');

                var menuIcon = $('.site-header-menu-btn').attr('data-icon');
                $('.site-header-menu-btn use').attr('xlink:href', menuIcon);

                $('.site-header-search').toggleClass('show');

                if ($('.site-header-search').hasClass('show')) {
                    var closeIcon = $(this).attr('data-close-icon');
                    $(this).find('use').attr('xlink:href', closeIcon);
                    $('.site-header-search input').focus();
                } else {
                    var icon = $(this).attr('data-icon');
                    $(this).find('use').attr('xlink:href', icon);
                }
            });
        }
    }]);

    return SiteHeader;
}();

var SiteFooter = function () {
    function SiteFooter() {
        _classCallCheck(this, SiteFooter);
    }

    _createClass(SiteFooter, null, [{
        key: 'main',
        value: function main() {

            $('.site-footer .copyright-year').text(new Date().getFullYear().toString());
        }
    }]);

    return SiteFooter;
}();

var StatCards = function () {
    function StatCards() {
        _classCallCheck(this, StatCards);
    }

    _createClass(StatCards, null, [{
        key: 'main',
        value: function main() {

            var didAnimate = false;
            var statCards = $('.stat-cards');
            var totalStats = $('.stat-cards .card').length;
            if (totalStats < 5) {
                $('.stat-cards').addClass('less-than-5');
            }

            if (statCards.length) {
                statCards.slick({
                    dots: true,
                    arrows: false,
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                    responsive: [{
                        breakpoint: 992,
                        settings: {
                            slidesToShow: 2,
                            slidesToScroll: 2
                        }
                    }]
                });

                $(window).scroll();

                // animate numbers when visible
                $(window).scroll(function () {
                    var top_of_element = $(".stat-cards").offset().top;
                    var bottom_of_element = $(".stat-cards").offset().top + $(".stat-cards").outerHeight();
                    var bottom_of_screen = $(window).scrollTop() + $(window).innerHeight();
                    var top_of_screen = $(window).scrollTop();

                    if (bottom_of_screen > top_of_element && top_of_screen < bottom_of_element) {
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

                    $(this).prop('Counter', 0).animate({
                        Counter: countTo
                    }, {
                        duration: 1000,
                        easing: 'swing',
                        step: function step() {
                            $(this).text(commaSeparateNumber(Math.floor(this.Counter)));
                        },
                        complete: function complete() {
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
    }]);

    return StatCards;
}();

var LatestNewsArticles = function () {
    function LatestNewsArticles() {
        _classCallCheck(this, LatestNewsArticles);
    }

    _createClass(LatestNewsArticles, null, [{
        key: 'setHeaderHeight',
        value: function setHeaderHeight() {
            $('.latest-news-articles').each(function (index, element) {

                var largestHeight = 0;

                $(element).find('.news-card .card-title').each(function (index, subElement) {
                    $(subElement).css('height', '');
                    largestHeight = $(subElement).height() > largestHeight ? $(subElement).height() : largestHeight;
                });

                $(element).find('.news-card .card-title').each(function (index, subElement) {
                    $(subElement).css('height', largestHeight + 'px');
                });
            });
        }
    }, {
        key: 'main',
        value: function main() {

            $('.latest-news-articles').on('init', function (event, slick) {
                LatestNewsArticles.setHeaderHeight();
            });

            $('.latest-news-articles').on('setPosition', function (slick) {
                LatestNewsArticles.setHeaderHeight();
            });

            $('.latest-news-articles').slick({
                dots: false,
                arrows: false,
                slidesToShow: 4,
                slidesToScroll: 4,
                infinite: true,
                responsive: [{
                    breakpoint: 992,
                    settings: {
                        dots: true,
                        slidesToShow: 2,
                        slidesToScroll: 2
                    }
                }]
            });
        }
    }]);

    return LatestNewsArticles;
}();

function MapMarker(model, map, bounds, onMarkerClick, allInfoWindows) {
    var marker = null;
    var locations = $('.locations-listings article.enable-on-map');
    var greatestMarkerDistance = -1;

    var options = {};
    options.position = new google.maps.LatLng(model.lat, model.long);
    options.map = map;
    options.label = "";

    if (model.icon) {
        options.icon = model.icon;
    }

    marker = new google.maps.Marker(options);

    bounds.extend(marker.position);
    map.fitBounds(bounds);

    function toRad(value) {
        return value * Math.PI / 180;
    }

    function calcDistance(lat1, lng1, lat2, lng2) {
        var earth = 6371,
            //km
        dLat = toRad(lat2 - lat1),
            dLng = toRad(lng2 - lng1);

        lat1 = toRad(lat1);
        lat2 = toRad(lat2);

        var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.sin(dLng / 2) * Math.sin(dLng / 2) * Math.cos(lat1) * Math.cos(lat2);
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var distance = earth * c / 1.609; //convert from km to miles

        return distance;
    }

    if (locations.length < 6) {
        // check greatest distance between markers shown
        for (var i = 0; i < locations.length; i++) {
            for (var j = 1; j < locations.length; j++) {
                var lat1 = locations[i].getAttribute("data-lat"),
                    lng1 = locations[i].getAttribute("data-lng"),
                    lat2 = locations[j].getAttribute("data-lat"),
                    lng2 = locations[j].getAttribute("data-lng"),
                    distance;

                distance = calcDistance(lat1, lng1, lat2, lng2);

                if (distance > greatestMarkerDistance) {
                    greatestMarkerDistance = distance;
                }
            }
        }

        // if the greatest distance is less than half a mile zoom out 
        if (greatestMarkerDistance <= 0.5) {
            var listener = google.maps.event.addListener(map, "idle", function () {
                if (map.getZoom() > 15) map.setZoom(15);
                google.maps.event.removeListener(listener);
            });
        }
    }

    var link = void 0;
    if (model.link != null && model.link != "") {
        link = '<a href="' + model.link + '">More Info</a><br/><br/>';
    } else {
        link = "";
    }

    var hoursetContent = '';
    if (model.hoursets) {
        var section = '';
        model.hoursets.each(function () {
            var title = $(this).find('.hourset-title').text();
            var hours = $('.hour', this);

            section += '<span style="font-weight: bold;">';
            section += title;
            section += '</span>';
            section += '<br />';

            if (hours) {
                hours.each(function () {
                    var startDay = $(this).find('.start-day').text();
                    var endDay = $(this).find('.end-day').text();
                    var openTime = $(this).find('.open-time').text();
                    var closeTime = $(this).find('.close-time').text();

                    section += '<span style="padding-left: 10px;">';
                    section += startDay + ' - ' + endDay + ' ' + openTime + ' - ' + closeTime;
                    section += '</span>';
                    section += '<br />';
                    hoursetContent += section;
                    section = '';
                });
            }
        });
    }

    var infowindow = new google.maps.InfoWindow({
        content: '\n            <div class="info-window">\n                <span><h3 class="h5">' + model.name + '</h3></span>\n                <br />\n                ' + hoursetContent + '\n                <br/>\n                ' + link + '\n                <a target="_blank" href="https://www.google.com/maps/dir/current+location/' + model.address + '">Get Directions</a>\n            </div>\n        '
    });

    // need to save instance of each infoWindow so they can be closed
    allInfoWindows.push(infowindow);

    marker.addListener('click', function () {
        // close all infoWindows before opening new one
        for (var _i2 = 0; _i2 < allInfoWindows.length; _i2++) {
            allInfoWindows[_i2].close();
        }

        infowindow.open(map, marker);
        onMarkerClick(model.id, marker);
    });

    this.id = model.id;

    this.closeWindow = function () {
        $('#' + this.id).removeClass('active');
        infowindow.close();
    };

    this.openWindow = function () {
        infowindow.open(map, marker);
    };
}

/*
    :: Custom Google Map
*/

var GoogleMap = function () {
    function GoogleMap(options) {
        _classCallCheck(this, GoogleMap);

        this.locations = options.locations || [/* {address: string || [lat,lon], name: string, link: string, icon: string(img src)}*/];
        this.container = options.container || '#not_available';
        this.map = null;
        this.mapBounds = null;
        this.markers = [];
        this.styling = options.styling || false;
        this.onMarkerClick = options.onMarkerClick || function () {};
        this.allInfoWindows = [];
        this.searchedLocation = options.searchedLocation;
        this.searchedMarker = null;
    }

    _createClass(GoogleMap, [{
        key: 'main',
        value: function main() {
            var _this4 = this;

            this.container = document.querySelector(this.container);

            if (!this.container) {
                return;
            }

            this.mapBounds = new google.maps.LatLngBounds();

            // add pin for searched location to bounds
            if (this.searchedLocation.position) {
                this.mapBounds.extend(this.searchedLocation.position);
            }

            this.map = new google.maps.Map(this.container, {
                disableDefaultUI: true,
                zoomControl: true,
                gestureHandling: 'cooperative',
                styles: this.styling
            });

            this.map.fitBounds(this.mapBounds);

            // Initialize google maps bounding box.
            if (this.locations.length == 0 && this.searchedLocation.position) {
                var zoomListener = google.maps.event.addListener(this.map, "idle", function () {
                    _this4.map.setZoom(13);
                    google.maps.event.removeListener(zoomListener);
                });
            } else {
                var listener = google.maps.event.addListener(this.map, "idle", function () {
                    _this4.map.fitBounds(_this4.mapBounds);
                    google.maps.event.removeListener(listener);
                });
            }

            var _iteratorNormalCompletion = true;
            var _didIteratorError = false;
            var _iteratorError = undefined;

            try {
                for (var _iterator = this.locations[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
                    var location = _step.value;

                    this.setupMarker(location, this.allInfoWindows, location.id);
                    this.linkMarker(location.id, this.markers);
                }

                // drop pin at searched location (if it exists)
            } catch (err) {
                _didIteratorError = true;
                _iteratorError = err;
            } finally {
                try {
                    if (!_iteratorNormalCompletion && _iterator.return) {
                        _iterator.return();
                    }
                } finally {
                    if (_didIteratorError) {
                        throw _iteratorError;
                    }
                }
            }

            if (this.searchedLocation.position) {
                this.searchedMarker = new google.maps.Marker({
                    position: this.searchedLocation.position,
                    map: this.map,
                    animation: google.maps.Animation.DROP
                });
            }
        }
    }, {
        key: 'linkMarker',
        value: function linkMarker(id, markers) {
            $('#' + id).unbind('click');

            $('#' + id).bind('click', function () {
                var index = $('article.enable-on-map').index($(this));

                // close all open infowindows
                var _iteratorNormalCompletion2 = true;
                var _didIteratorError2 = false;
                var _iteratorError2 = undefined;

                try {
                    for (var _iterator2 = markers[Symbol.iterator](), _step2; !(_iteratorNormalCompletion2 = (_step2 = _iterator2.next()).done); _iteratorNormalCompletion2 = true) {
                        var marker = _step2.value;

                        marker.closeWindow();
                    }

                    // open infowindow for this location
                } catch (err) {
                    _didIteratorError2 = true;
                    _iteratorError2 = err;
                } finally {
                    try {
                        if (!_iteratorNormalCompletion2 && _iterator2.return) {
                            _iterator2.return();
                        }
                    } finally {
                        if (_didIteratorError2) {
                            throw _iteratorError2;
                        }
                    }
                }

                $(this).addClass('active');
                markers[index].openWindow();
            });
        }

        // Convert address string to lat long object

    }, {
        key: 'setupMarker',
        value: function setupMarker(location, allInfoWindows, locationId) {
            var _this5 = this;

            // Lat and Long was provided for the location address
            if (Array.isArray(location.address)) {
                this.makeMarker(location, location.address[0], location.address[1], allInfoWindows, locationId);
                return;
            }

            // Address string was provided
            if (typeof location.address == "string") {
                var geocoder = new google.maps.Geocoder();
                var geoModel = {
                    'address': location.address
                };

                geocoder.geocode(geoModel, function (results, status) {
                    if (status != google.maps.GeocoderStatus.OK) {
                        console.warn('Google maps Geocode: Can\'t convert ' + location.address + ' to lat/lon: ' + status);
                        return;
                    }

                    _this5.makeMarker(location, results[0].geometry.location.lat(), results[0].geometry.location.lng(), allInfoWindows, locationId);
                });

                return;
            }

            // Error, or nothing was provided
            console.warn('Map Marker address for ' + location.name + ' is invalid or not set');
        }
    }, {
        key: 'makeMarker',
        value: function makeMarker(location, lat, long, allInfoWindows, locationId) {
            var marker = null;
            // let image = {
            //     url: originURL + '/ResourcePackages/ST/library/img/location-marker.png'
            // };

            marker = new MapMarker({
                name: location.name,
                address: location.address,
                link: location.link,
                hoursets: location.hoursets,
                // icon: image,
                lat: lat,
                long: long,
                id: locationId
            }, this.map, this.mapBounds, this.onMarkerClick, allInfoWindows);

            this.markers.push(marker);
        }
    }]);

    return GoogleMap;
}();

var QuickCareLocator = function () {
    function QuickCareLocator() {
        _classCallCheck(this, QuickCareLocator);
    }

    _createClass(QuickCareLocator, null, [{
        key: 'main',
        value: function main() {

            // Helper funtions
            function toRad(value) {
                return value * Math.PI / 180;
            }

            function calcDistance(lat1, lng1, lat2, lng2) {
                var earth = 6371,
                    //km
                dLat = toRad(lat2 - lat1),
                    dLng = toRad(lng2 - lng1);

                lat1 = toRad(lat1);
                lat2 = toRad(lat2);

                var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.sin(dLng / 2) * Math.sin(dLng / 2) * Math.cos(lat1) * Math.cos(lat2);
                var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
                var distance = earth * c / 1.609; //convert from km to miles

                return distance;
            }

            $('.quick-care-locator').each(function (index, element) {

                var mapDiv = $(element).find('.map-div');
                var zipField = $(element).find('.quick-care-locator-input');
                var locations = JSON.parse(mapDiv.attr('data-locations'));
                var autocomplete = new google.maps.places.Autocomplete(zipField[0], {
                    componentRestrictions: { country: ["us", "pr", "vi", "gu", "mp"] }
                });

                var pinOptions = {};
                var lat = void 0;
                var lng = void 0;

                // Initialize map code
                var map = new GoogleMap({
                    locations: locations,
                    container: '#' + mapDiv.attr('id'),
                    searchedLocation: pinOptions
                });

                map.main();

                // Filter locations based on user input
                google.maps.event.addListener(autocomplete, 'place_changed', function () {

                    var place = autocomplete.getPlace();

                    lat = place.geometry.location.lat();
                    lng = place.geometry.location.lng();

                    pinOptions = {};
                    pinOptions.position = new google.maps.LatLng(lat, lng);

                    var filteredLocations = [];

                    locations.forEach(function (location) {

                        if (calcDistance(lat, lng, location.address[0], location.address[1]) <= 20) {
                            filteredLocations.push(location);
                        }
                    });

                    map = new GoogleMap({
                        locations: filteredLocations,
                        container: '#' + mapDiv.attr('id'),
                        searchedLocation: pinOptions
                    });

                    map.main();
                });

                // Reset on resize
                $(window).on('resize', function () {
                    map.map = null;
                    map.container = '#' + mapDiv.attr('id');
                    mapDiv.html("");
                    map.main();
                });
            });
        }
    }]);

    return QuickCareLocator;
}();

// Keyboard Enum


var _KEYBOARD_$1 = Object.freeze({
    'enter': 13,
    'arrow-u': 38,
    'arrow-r': 39,
    'arrow-d': 40,
    'arrow-l': 37,
    'esc': 27,
    'tab': 9,
    'space': 32
});

/**
 * Breakpoints are situatued such that
 * comparison would be "and down" --
 * 
 * if (window.innerWidth < _BREAKPOINTS_.sm) { ... }
 */
var _BREAKPOINTS_ = Object.freeze({
    'xs': 576,
    'sm': 767,
    'md': 991,
    'lg': 1199,
    'xl': 1399,
    'giant': 1799
});

var QuickLinksDropdowns = function () {
    function QuickLinksDropdowns() {
        _classCallCheck(this, QuickLinksDropdowns);
    }

    _createClass(QuickLinksDropdowns, null, [{
        key: 'main',
        value: function main() {

            function openDropdown(element) {

                $('.quick-link-dropdown.show').removeClass('show');
                $('.quick-dropdown-menu.show').removeClass('show');

                var dropdown = $(element).closest('.quick-link-dropdown').find('.quick-dropdown-menu');
                $(element).closest('.quick-link-dropdown').addClass('show');
                // $(element).addClass('show');
                $(dropdown).addClass('show');
            }

            function closeDropdown(element) {

                var dropdown = $(element).closest('.quick-link-dropdown').find('.quick-dropdown-menu');

                if (!$(element).closest('.quick-link-dropdown').hasClass('current')) {

                    $(element).closest('.quick-link-dropdown').removeClass('show');
                    // $(element).removeClass('show');
                    $(dropdown).removeClass('show');
                }
            }

            $('.quick-link-dropdown').each(function (index, element) {
                $(element).on('click', function (event) {

                    if (window.innerWidth > 1200) return;
                    var linkItem = event.currentTarget;
                    if ($(linkItem).closest('.quick-link-dropdown').hasClass('show')) {
                        closeDropdown(linkItem);
                    } else {
                        openDropdown(linkItem);
                    }
                });

                $(element).on('mouseenter', function (event) {
                    if (window.innerWidth < 1200) return;

                    var linkItem = event.currentTarget;

                    if ($(linkItem).closest('.quick-link-dropdown').hasClass('show')) {
                        return;
                    } else {
                        openDropdown(linkItem);
                    }
                });

                $(element).on('mouseleave', function (event) {

                    if (window.innerWidth < 1200) return;

                    var linkItem = event.currentTarget;

                    if (!$(linkItem).closest('.quick-link-dropdown').hasClass('show')) {
                        return;
                    } else {
                        closeDropdown(linkItem);
                    }
                });

                $(element).on('keyup', function (event) {
                    var linkItem = event.currentTarget;

                    if (event.which === _KEYBOARD_$1.enter) {
                        openDropdown(linkItem);
                    } else if (event.which === _KEYBOARD_$1['arrow-u']) {
                        closeDropdown(linkItem);
                    } else if (event.which === _KEYBOARD_$1['arrow-d']) {
                        openDropdown(linkItem);
                    } else {
                        return false;
                    }
                });
            });
        }
    }]);

    return QuickLinksDropdowns;
}();

var VideoEmbedController = function () {
    function VideoEmbedController() {
        _classCallCheck(this, VideoEmbedController);
    }

    _createClass(VideoEmbedController, null, [{
        key: 'main',
        value: function main() {

            $('.video-embed-btn').each(function (index, element) {
                $(element).on('click', function (event) {
                    event.preventDefault();

                    // disable transition if inside slick slider
                    var slickTrack = $(this).closest('.slick-track');
                    var slickList = $(this).closest('.slick-list');

                    if (slickTrack.length > 0) {
                        slickTrack.addClass('modal-open');
                        slickList.addClass('modal-open');
                    }

                    var modal = $(this).closest('.video-embed').find('.video-embed-content');
                    var frame = $(modal).find('iframe');

                    $(modal).addClass('open');
                    $(frame).attr('src', $(frame).attr('data-src'));
                });
            });

            $('.video-overlay').each(function (index, element) {
                $(element).on('click', function (event) {

                    // enable transition if inside slick slider
                    var slickTrack = $(this).closest('.slick-track');
                    var slickList = $(this).closest('.slick-list');

                    if (slickTrack.length > 0) {
                        slickTrack.removeClass('modal-open');
                        slickList.removeClass('modal-open');
                    }

                    var modal = $(this).closest('.video-embed').find('.video-embed-content');
                    var frame = $(modal).find('iframe');

                    $(modal).removeClass('open');
                    $(frame).removeAttr('src');
                });
            });

            $('.close-frame').each(function (index, element) {
                $(element).on('click', function (event) {
                    event.preventDefault();

                    // enable transition if inside slick slider
                    var slickTrack = $(this).closest('.slick-track');
                    var slickList = $(this).closest('.slick-list');

                    if (slickTrack.length > 0) {
                        slickTrack.removeClass('modal-open');
                        slickList.removeClass('modal-open');
                    }

                    var modal = $(this).closest('.video-embed').find('.video-embed-content');
                    var frame = $(modal).find('iframe');

                    $(modal).removeClass('open');
                    $(frame).removeAttr('src');
                });
            });

            var modalElement = $('.video-embed-content');

            var firstFocusable = $(modalElement).find('.close-frame');
            var lastFocusable = $(modalElement).find('.btn-link.sr-only');

            // Set focus on first focusable item in modal
            modalElement.on('shown.bs.modal', function () {
                firstFocusable.focus();
            });

            // If reverse tabbing go to end of modal
            firstFocusable.on('keydown', function (event) {
                if ((event.key === 'Tab' || event.code === 'Tab') && event.shiftKey) {
                    event.preventDefault();

                    lastFocusable.focus();
                }
            });

            // If tabbing at end of modal, return back to beginning
            lastFocusable.on('keydown', function (event) {
                if ((event.key === 'Tab' || event.code === 'Tab') && !event.shiftKey) {
                    event.preventDefault();
                    firstFocusable.focus();
                }
            });
        }
    }]);

    return VideoEmbedController;
}();

var VideoSection = function () {
    function VideoSection() {
        _classCallCheck(this, VideoSection);
    }

    _createClass(VideoSection, null, [{
        key: 'main',
        value: function main() {

            if (window.innerWidth < 992) {
                $('.video-layout').slick({
                    dots: false,
                    arrows: false,
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    infinite: true,
                    responsive: [{
                        breakpoint: 992,
                        settings: {
                            dots: true,
                            slidesToShow: 2,
                            slidesToScroll: 2
                        }
                    }, {
                        breakpoint: 768,
                        settings: {
                            dots: true,
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }]
                });
            }

            window.addEventListener('resize', function () {
                if (window.innerWidth < 992) {
                    if (!$('.video-layout').hasClass('slick-slider')) {
                        $('.video-layout').slick({
                            dots: false,
                            arrows: false,
                            slidesToShow: 4,
                            slidesToScroll: 4,
                            infinite: true,
                            responsive: [{
                                breakpoint: 992,
                                settings: {
                                    dots: true,
                                    slidesToShow: 2,
                                    slidesToScroll: 2
                                }
                            }, {
                                breakpoint: 768,
                                settings: {
                                    dots: true,
                                    slidesToShow: 1,
                                    slidesToScroll: 1
                                }
                            }]
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
    }]);

    return VideoSection;
}();

var SearchFilter = function () {
    function SearchFilter() {
        _classCallCheck(this, SearchFilter);
    }

    _createClass(SearchFilter, null, [{
        key: 'main',
        value: function main() {

            function openDropdown(element) {

                var dropdown = $(element).closest('.dropdown').find('.dropdown-menu');
                $(element).closest('.dropdown').addClass('show');
                $(element).addClass('show');
                $(dropdown).addClass('show');

                $(element).text('CLOSE FILTER');
            }

            function closeDropdown(element) {

                var dropdown = $(element).closest('.dropdown').find('.dropdown-menu');
                $(element).closest('.dropdown').removeClass('show');
                $(element).removeClass('show');
                $(dropdown).removeClass('show');

                $(element).text('SEARCH FILTER');
            }

            $('.search-filter').each(function (index, element) {

                // const clearFiltersBtn = $(element).find('.clear-filters');
                // $(clearFiltersBtn).on('click', function(event) {
                // clearFilters(event, clearFiltersBtn);
                // });

                var dropdownToggle = $(element).find('.dropdown-toggle');
                $(dropdownToggle).on('click', function (event) {

                    if (window.innerWidth > 1200) return;

                    var linkItem = event.currentTarget;

                    if ($(linkItem).closest('.dropdown').hasClass('show')) {
                        closeDropdown(linkItem);
                    } else {
                        openDropdown(linkItem);
                    }
                });

                $(dropdownToggle).on('keyup', function (event) {
                    var linkItem = event.currentTarget;

                    if (event.which === _KEYBOARD_.enter) {
                        openDropdown(linkItem);
                    } else if (event.which === _KEYBOARD_['arrow-u']) {
                        closeDropdown(linkItem);
                    } else if (event.which === _KEYBOARD_['arrow-d']) {
                        openDropdown(linkItem);
                    } else {
                        return false;
                    }
                });
            });
        }
    }]);

    return SearchFilter;
}();

var UmcCareFilter = function () {
    function UmcCareFilter() {
        _classCallCheck(this, UmcCareFilter);
    }

    _createClass(UmcCareFilter, null, [{
        key: 'main',
        value: function main() {

            var locations = document.querySelectorAll('.location-card');
            var checkboxes = Array.from(document.querySelectorAll('.form-check-input'));

            checkboxes.forEach(function (input) {
                input.addEventListener('click', function (event) {

                    if (document.querySelector('.form-check-input.active')) {
                        document.querySelector('.form-check-input.active').classList.remove('active');
                    }

                    input.classList.add('active');
                    var catagory = input.getAttribute('value');

                    switch (catagory) {
                        case 'all':
                            document.querySelector('.PRIMARY-list').classList.remove('d-none');
                            document.querySelector('.QUICK-list').classList.remove('d-none');
                            break;
                        case 'primary-care':
                            document.querySelector('.PRIMARY-list').classList.add('d-block');
                            document.querySelector('.PRIMARY-list').classList.remove('d-none');
                            document.querySelector('.QUICK-list').classList.add('d-none');
                            document.querySelector('.QUICK-list').classList.remove('d-block');
                            break;
                        case 'quick-care':
                            document.querySelector('.QUICK-list').classList.add('d-block');
                            document.querySelector('.QUICK-list').classList.remove('d-none');
                            document.querySelector('.PRIMARY-list').classList.add('d-none');
                            document.querySelector('.PRIMARY-list').classList.remove('d-block');
                            break;
                        default:
                            document.querySelectorAll('.clinic-location-list.d-none').classList.remove('d-none');
                    }
                });
            });
        }
    }]);

    return UmcCareFilter;
}();

var DatePicker = function () {
    function DatePicker() {
        _classCallCheck(this, DatePicker);

        this.datepickers = $('.datepicker');
        if (this.datepickers.length) {
            this.init();
        }
    }

    _createClass(DatePicker, [{
        key: 'init',
        value: function init() {
            this.datepickers.each(function () {
                $(this).datepicker();
            });
        }
    }]);

    return DatePicker;
}();

var GenericHero = function () {
    function GenericHero() {
        _classCallCheck(this, GenericHero);
    }

    _createClass(GenericHero, null, [{
        key: 'main',
        value: function main() {
            var windowSize = window.innerWidth;
            $('.generic-hero').each(function (index, element) {
                // default to desktop img if no mobile img is passed in;
                var mobileSrc = $(element).attr('data-mobile-src') || $(element).attr('data-desktop-src');
                var desktopSrc = $(element).attr('data-desktop-src');
                if (windowSize < 992) {
                    $(element).css({ "background-image": 'url(\'' + mobileSrc + '\')' });
                } else {
                    $(element).css({ "background-image": 'url(\'' + desktopSrc + '\')' });
                }
                var mediaQuery = '(max-width: 992px)';
                var mediaQueryList = window.matchMedia(mediaQuery);
                mediaQueryList.addEventListener('change', function (event) {
                    if (event.matches) {
                        $(element).css({ "background-image": 'url(\'' + mobileSrc + '\')' });
                    } else {
                        $(element).css({ "background-image": 'url(\'' + desktopSrc + '\')' });
                    }
                });
            });
        }
    }]);

    return GenericHero;
}();

var AllArticles = function () {
    function AllArticles() {
        _classCallCheck(this, AllArticles);

        this.allArticlesListings = document.querySelectorAll('.all-news-articles');
        if (this.allArticlesListings.length) {
            this.init();
        }
    }

    _createClass(AllArticles, [{
        key: 'init',
        value: function init() {
            var _this6 = this;

            this.allArticlesListings.forEach(function (element) {
                var cards = element.querySelectorAll('.news-card');
                cards.forEach(function (card, i) {
                    // show the first 4
                    if (i >= 4) {
                        card.classList.add('d-none');
                    }
                });
                element.querySelector('.load-more-btn').addEventListener('click', function (e) {
                    e.preventDefault();

                    var hiddenCards = element.querySelectorAll('.news-card.d-none');

                    if (!hiddenCards.length) {
                        _this6.hideCards(cards, e.target);
                    } else {
                        _this6.showMoreCards(hiddenCards, e.target);
                    }
                });
            });
        }
    }, {
        key: 'showMoreCards',
        value: function showMoreCards(cards, element) {
            if (cards.length <= 4) {
                element.innerHTML = 'Show Less';
            }

            Array.from(cards).slice(0, 4).map(function (card) {
                card.classList.remove('d-none');
            });
        }
    }, {
        key: 'hideCards',
        value: function hideCards(cards, element) {
            element.innerHTML = 'Load More';

            Array.from(cards).slice(4).map(function (card) {
                card.classList.add('d-none');
            });
        }
    }]);

    return AllArticles;
}();

/* Site Specific Javascript Component Imports */

var MainScripts = function () {

    // Shared
    if (typeof svg4everybody == 'function') {
        svg4everybody();
    }

    var imageLoader = new ImageLoader();
    imageLoader.initialize();

    SiteHeader.main();
    SiteFooter.main();
    StatCards.main();
    LatestNewsArticles.main();
    QuickCareLocator.main();
    QuickLinksDropdowns.main();
    VideoEmbedController.main();
    VideoSection.main();
    SearchFilter.main();
    GenericHero.main();
    new DatePicker();
    new AllArticles();

    window.locationsReady = function () {
        UmcCareFilter.main();
    };
}();
//# sourceMappingURL=main.js.map
