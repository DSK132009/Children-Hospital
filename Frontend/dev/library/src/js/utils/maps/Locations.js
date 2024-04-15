import { GoogleMap } from './GoogleMap';

export class Locations {

	static displayLocations() {
		const locations = $('.enable-on-map');
		const map = $('#map');

		let pinOptions = {},
			lat,
			lng;

		if (!map.length || !locations.length) {
			return;
		}

		let data = [];
		let address;
		locations.each(function () {
			// If a lat and long was provided, use that.
			// If not, then use the address string
			if ($(this).attr('data-lat') && $(this).attr('data-lng')) {
				address = [$(this).attr('data-lat'), $(this).attr('data-lng')];
			} else {
				address = $(this).attr('data-address');
			}

			data.push({
				address: address,
				name: $(this).find('.location-title').text(),
				link: $(this).find('.location-title').attr('href'),
				hoursets: $('.hourset', this),
				id: $(this).attr('id')
			});
		});

		var locationsMap = new GoogleMap({
			locations: data,
			container: '#map',
			onMarkerClick: (id, marker) => {
				$('article.active').removeClass('active');
				$('#' + id).addClass('active');
			},
			searchedLocation: pinOptions
		});

		locationsMap.main();

		//reset on resize
		$(window).on('resize', () => {
			locationsMap.map = null;
			locationsMap.container = '#map';
			$('#map').html("");
			locationsMap.main();
		});
		
		// utilize Google Places API to filter locations within a radius
		if($('#map').length != 0){

            $('li').click(function () {
				filterLocations($(this));
			});

            placesAutocomplete();
		}
		
		function reloadMap() {
			if (!map.length || !locations.length) {
				return;
			}
	
			// reset shown locations based on filter
			data = [];
			let address;

			$('.enable-on-map').each(function () {
				// If a lat and long was provided, use that.
				// If not, then use the address string
				if ($(this).attr('data-lat') && $(this).attr('data-lng')) {
					address = [$(this).attr('data-lat'), $(this).attr('data-lng')];
				} else {
					address = $(this).attr('data-address');
				}
	
				data.push({
					address: address,
					name: $(this).find('.location-title').text(),
					link: $(this).find('.location-title').attr('href'),
					hoursets: $('.hourset', this),
					id: $(this).attr('id')
				});
			});
	
			locationsMap = new GoogleMap({
				locations: data,
				container: '#map',
				onMarkerClick: (id, marker) => {
					$('article.active').removeClass('active');
					$('#' + id).addClass('active');
				},
				searchedLocation: pinOptions
			});
	
			locationsMap.main();
		}
        
        function hideLocation(item) {
            item.css('display', 'none');
            item.removeClass('enable-on-map');
        }

        function showLocation(item, distance) {
            item.css('display', 'flex');
			item.addClass('enable-on-map');

			if (distance) {
				// display distance of each item
				distance = distance.toFixed(2);
				item.find('.distance p').html(distance + ' miles');

				item.data("distance", distance);
			}
		}
		
        function filterLocations(selectedOption) {
			var optionSelected;

			if (selectedOption) {
				optionSelected = selectedOption.text();
			}
			else {
				optionSelected = $('.location-dropdown li.selectedOption').text();
			}

			$("article").each(function () {
				var options = $(".bankInfo", this).children(".options").text(),
					bankCenters = $('.bankcenters').text().split(','),
					atms = $('.atms').text().split(','),
					lat2 = $(this).attr("data-lat"),
					lng2 = $(this).attr("data-lng"),
					show = false,
					distance = null;

				// check if location fits in filter
				if (optionSelected == "All Offerings" || options.includes(optionSelected)) {
					show = true;
				}
				else if (optionSelected == "Banking Centers") {
					bankCenters.forEach(function (element) {
						if (options.includes(element)) {
							show = true;
						}
					});
				}
				else if (optionSelected == "ATMs") {
					atms.forEach(function (element) {
						if (options.includes(element)) {
							show = true;
						}
					});
				}

				if (lat && lng) {
					distance = calcDistance(lat, lng, lat2, lng2);

					// only show location if it is in a 20 miles radius
					if (distance > 20) {
						show = false;
					}
				}

				if (show && distance) {
					showLocation($(this), distance);
				}
				else if (show && !distance) {
					showLocation($(this));
				}
				else {
					hideLocation($(this));
				}
			});

			if (lat && lng) {
				// sort shown locations by distance
				sortLocations();
			}

			reloadMap();

			// check if any locations are displayed
			if ($('.locations-listings article.enable-on-map').length > 0) {
				$('.noLocationsMessage').hide();
			}
			else {
				$('.noLocationsMessage').show();
			}
		}

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

		function sortLocations() {
			var locations = [];

			// create array of shown articles so we can sort it
			$('article.enable-on-map').each(function() {
				locations.push($(this));
			});

			// sort locations by distance in ascending order
			locations.sort(function(a, b) {
				return a.data("distance") - b.data("distance");
			});

			// iterate through sorted locations and order them on the page
			for (let i = 0; i < locations.length; i++) {
				locations[i].css('order', i);
			}
		}

        function placesAutocomplete() {
			var autocomplete = new google.maps.places.Autocomplete(document.getElementById('locationInput'));
		
            google.maps.event.addListener(autocomplete, 'place_changed', function() {
				let place = autocomplete.getPlace();

				lat = place.geometry.location.lat();
				lng = place.geometry.location.lng();

				pinOptions = {};
				pinOptions.position = new google.maps.LatLng(lat, lng);

				// filter locations to only show those in selected filter
				filterLocations();
            });
        }
	}
}
