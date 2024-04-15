/*
    :: Custom Google Map
*/

import { MapMarker } from './MapMarker';

export class GoogleMap {

    constructor(options) {
        this.locations = options.locations || [/* {address: string || [lat,lon], name: string, link: string, icon: string(img src)}*/];
        this.container = options.container || '#not_available';
        this.map = null;
        this.mapBounds = null;
        this.markers = [];
        this.styling = options.styling || false;
        this.onMarkerClick = options.onMarkerClick || function () { };
        this.allInfoWindows = [];
        this.searchedLocation = options.searchedLocation;
        this.searchedMarker = null;
    }

    main() {
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
            var zoomListener = google.maps.event.addListener(this.map, "idle", () => { 
                this.map.setZoom(13); 
                google.maps.event.removeListener(zoomListener); 
            });
        }
        else {
            let listener = google.maps.event.addListener(this.map, "idle", () => {
                this.map.fitBounds(this.mapBounds);
                google.maps.event.removeListener(listener);
            });
        }

        for (let location of this.locations) {
            this.setupMarker(location, this.allInfoWindows, location.id);
            this.linkMarker(location.id, this.markers);
        }

        // drop pin at searched location (if it exists)
        if (this.searchedLocation.position) {
            this.searchedMarker = new google.maps.Marker({
                position: this.searchedLocation.position,
                map: this.map,
                animation: google.maps.Animation.DROP
            });
        }
    }

    linkMarker(id, markers) {
        $('#' + id).unbind('click');

        $('#' + id).bind('click', function() {
            let index = $('article.enable-on-map').index($(this));

            // close all open infowindows
            for (let marker of markers) {
                marker.closeWindow();
            }

            // open infowindow for this location
            $(this).addClass('active');
            markers[index].openWindow();
        });
    }

    // Convert address string to lat long object
    setupMarker(location, allInfoWindows, locationId) {
        // Lat and Long was provided for the location address
        if (Array.isArray(location.address)) {
            this.makeMarker(location, location.address[0], location.address[1], allInfoWindows, locationId);
            return;
        }

        // Address string was provided
        if (typeof location.address == "string") {
            const geocoder = new google.maps.Geocoder();
            const geoModel = {
                'address': location.address
            };

            geocoder.geocode(geoModel, (results, status) => {
                if (status != google.maps.GeocoderStatus.OK) {
                    console.warn(`Google maps Geocode: Can't convert ${location.address} to lat/lon: ${status}`);
                    return;
                }

                this.makeMarker(location, results[0].geometry.location.lat(), results[0].geometry.location.lng(), allInfoWindows, locationId);
            });

            return;
        }

        // Error, or nothing was provided
        console.warn(`Map Marker address for ${location.name} is invalid or not set`);
    }

    makeMarker(location, lat, long, allInfoWindows, locationId) {
        let marker = null;
        let originURL = window.location.origin;
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

}
