
export function MapMarker(model, map, bounds, onMarkerClick, allInfoWindows) {
    let marker = null;
    let locations = $('.locations-listings article.enable-on-map');
    let greatestMarkerDistance = -1;

    const options = {};
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
            var listener = google.maps.event.addListener(map, "idle", function() { 
                if (map.getZoom() > 15) map.setZoom(15); 
                google.maps.event.removeListener(listener); 
            });
        }
    }

    let link;
    if (model.link != null && model.link != "") {
        link = `<a href="${model.link}">More Info</a><br/><br/>`;
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
                <span><h3 class="h5">${model.name}</h3></span>
                <br />
                ${hoursetContent}
                <br/>
                ${link}
                <a target="_blank" href="https://www.google.com/maps/dir/current+location/${model.address}">Get Directions</a>
            </div>
        `
    });

    // need to save instance of each infoWindow so they can be closed
    allInfoWindows.push(infowindow);

    marker.addListener('click', () => {
        // close all infoWindows before opening new one
        for (let i = 0; i < allInfoWindows.length; i++) {
            allInfoWindows[i].close();
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
