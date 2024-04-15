# ST Core Google Maps plguin
Small library for rendering custom google maps

## Code Overview
### GoogleMap.js
This class creates a new instance of the google maps api. 
Then passes the locations object through the google geocode api 
to get the latitude and longitude. Creates a new instance of the
MapMarker method and assigns it to that map instance
### MapMarker.js
This method is used as the marker instance. It will place the marker on
the map, initialize the info popup, and assign the click handlers for 
each marker.

## Updates - Added Features & Bug Fixes - 9/9/19 LG
- Filtering based on location type 
    - Located: Locations.js
    - this will need to be customized to the project

- Google Places API integration for autocomplete of location search
    - Located: Locations.js
    - This requires the places library to be included with the api key
    - This filters the exisiting full list of locations prebuilt/loaded on page

- Marker drop for location searched
    - Located: GoogleMap.js

- Displays locations only within specific radius of searched locations 
    - Located: MapMarker.js
    - Default: 20 miles
    - Further work can be done on this to add a dropdown for the user specified radius

- Auto adjust zoom if less than 6 locations exists and are less than half a mile from each other
    - Located: MapMarker.js

- Fixed infowindow issue with multiple windows staying open. All infowindows close before opening new window.
    - Located: GoogleMap.js

- Linked location in list to map marker to highlight location and open infowindow when marker or location is selected
    - Located: GoogleMap.js

- Sorts locations by distance from searched address from closest to furthest
    - Located: Locations.js

- Display distance from searched address on each location
    - Located: Locations.js

All the above listed features can be seen here: https://www.intrustbank.com/locations

## Usage
```javascript
import { GoogleMap } from './codepath-to-st-core-library/maps/GoogleMap';

const initMap = () => {
    let map = new GoogleMap({
        locations: [
            {
                address: '680 Centre St. Brockton MA 02302',
                name: 'Signature Healthcare Brockton Hospital'
            },
            {
                address: '545 Bedford St. Bridgewater, MA',
                name: 'Other Location Name'
            }
        ],
        container: '#map-div',
        styling: [/* see google documentation on map styling*/],
        onMarkerClick: (location, marker) => {
            $( `[data-address=${location.address}]`).focus();
        }
    });

    map.main();
}

// <script src="...google-maps-api-link/apikey/callback=initMap" async defer></script>
```