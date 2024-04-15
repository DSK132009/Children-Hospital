/*
    :: Custom Google Map
*/

import { MapMarker } from './MapMarker-LocationDetail';

export class LocationDetailMap {

    constructor(options) {
        this.locations = options.locations || [/* {address: string || [lat,lon], name: string, link: string, icon: string(img src)}*/];
        this.container = options.container || '#not_available';
        this.map = null;
        this.mapBounds = null;
        this.markers = [];
        this.styling = options.styling || false;
        this.onMarkerClick = options.onMarkerClick || function () { };
    }

    main() {
        this.container = document.querySelector(this.container);

        if (!this.container) {
            return;
        }

        this.mapBounds = new google.maps.LatLngBounds();

        this.map = new google.maps.Map(this.container, {
            disableDefaultUI: true,
            zoomControl: true,
            gestureHandling: 'cooperative',
            styles: this.styling
        });

        for (let location of this.locations) {
            this.setupMarker(location);
        }
    }

    // Convert address string to lat long object
    setupMarker(location) {

        // Lat and Long was provided for the location address
        if (Array.isArray(location.address)) {
            this.makeMarker(location, location.address[0], location.address[1]);
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

                this.makeMarker(location, results[0].geometry.location.lat(), results[0].geometry.location.lng());
            });

            return;
        }

        // Error, or nothing was provided
        console.warn(`Map Marker address for ${location.name} is invalid or not set`);
    }

    makeMarker(location, lat, long) {
        let marker = null;
        let originURL = window.location.origin;
        let image = {
            url: originURL + '/ResourcePackages/ST/library/img/location-marker.png'
        };

        marker = new MapMarker({
            name: location.name,
            address: location.address,
            link: location.link,
            hoursets: location.hoursets,
            icon: image,
            lat: lat,
            long: long
        }, this.map, this.mapBounds, this.onMarkerClick);

        this.markers.push(marker);
    }

}
