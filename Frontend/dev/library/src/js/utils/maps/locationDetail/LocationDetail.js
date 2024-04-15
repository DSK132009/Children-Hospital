import { LocationDetailMap } from './LocationDetailMap';

export class LocationDetail {

	static displayLocation() {
		const locations = $('.enable-on-map');
		const map = $('#detailMap');

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
				hoursets: $('.hourset', this)
			});
		});

		let locationsMap = new LocationDetailMap({
			locations: data,
			container: '#detailMap',
			onMarkerClick: (location, marker) => {
				$(`.location-item[href="${location.link}"]`).focus();
			}
		});

		locationsMap.main();

		//reset on resize
		$(window).on('resize', () => {
			locationsMap.map = null;
			locationsMap.container = '#detailMap';
			$('#detailMap').html("");
			locationsMap.main();
		});
	}

}
