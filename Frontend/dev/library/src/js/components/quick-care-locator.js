import { GoogleMap } from "../utils/maps/GoogleMap";

export class QuickCareLocator {

    static main() {

        // Helper funtions
        function toRad(value) {
			return value * Math.PI / 180;
		}

        function calcDistance(lat1, lng1, lat2, lng2) {
			var earth = 6371, //km
				dLat = toRad(lat2 - lat1),
				dLng = toRad(lng2 - lng1);

			lat1 = toRad(lat1);
			lat2 = toRad(lat2);

			var a = Math.sin(dLat/2) * Math.sin(dLat/2) + Math.sin(dLng/2) * Math.sin(dLng/2) * Math.cos(lat1) * Math.cos(lat2);
			var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
			var distance = (earth * c) / 1.609; //convert from km to miles

			return distance;
		}

        $('.quick-care-locator').each(function(index, element) {

            const mapDiv = $(element).find('.map-div');
            const zipField = $(element).find('.quick-care-locator-input');
            const locations = JSON.parse(mapDiv.attr('data-locations'));
            const autocomplete = new google.maps.places.Autocomplete(zipField[0], {
                componentRestrictions: { country: ["us", "pr", "vi", "gu", "mp"] }
            });

            let pinOptions = {};
            let lat;
            let lng;
    
            // Initialize map code
            let map = new GoogleMap({
                locations: locations,
                container: `#${mapDiv.attr('id')}`,
                searchedLocation: pinOptions
            });
    
            map.main();

            // Filter locations based on user input
            google.maps.event.addListener(autocomplete, 'place_changed', function() {

                let place = autocomplete.getPlace();

                lat = place.geometry.location.lat();
                lng = place.geometry.location.lng();

                pinOptions = {};
                pinOptions.position = new google.maps.LatLng(lat, lng);

                let filteredLocations = [];

                locations.forEach((location) => {

                    if (calcDistance(lat, lng, location.address[0], location.address[1]) <= 20) {
                        filteredLocations.push(location);
                    }
                });

                map = new GoogleMap({
                    locations: filteredLocations,
                    container: `#${mapDiv.attr('id')}`,
                    searchedLocation: pinOptions
                });

                map.main();
            });

            // Reset on resize
            $(window).on('resize', () => {
                map.map = null;
                map.container = `#${mapDiv.attr('id')}`;
                mapDiv.html("");
                map.main();
            });
        });
    }
} 