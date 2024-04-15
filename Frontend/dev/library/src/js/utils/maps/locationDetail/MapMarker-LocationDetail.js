
export function MapMarker(model, map, bounds, onMarkerClick) {
    let marker = null;

    const options = {};
    options.position = new google.maps.LatLng(model.lat, model.long);
    options.map = map;

    if (model.icon) {
        options.icon = model.icon;
    }

    marker = new google.maps.Marker(options);

    map.setCenter(options.position);
    map.setZoom(15);

    let link;
    if (model.link != null && model.link != "") {
        link = `<a href="${model.link}">More Info</a><br /><br/>`;
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

    let infowindow = new google.maps.InfoWindow({
        content: `
        <div class="info-window">
            <span><h3>${model.name}</h3></span>
            <br />
            ${hoursetContent}
            <br/>
            ${link}
            <a target="_blank" href="https://www.google.com/maps/dir/current+location/${model.address}">Get Directions</a>
        </div>
        `
    });

    marker.addListener('click', () => {
        infowindow.open(map, marker);
        onMarkerClick(model, marker);
    });

    this.id = model.link;
    this.closeWindow = function () {
        infowindow.close();
    };
    this.openWindow = function () {
        infowindow.open(map, marker);
    };
}
