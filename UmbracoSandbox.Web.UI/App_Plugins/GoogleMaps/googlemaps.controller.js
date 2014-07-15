angular.module("umbraco").controller("Zone.GoogleMapsController",
    function ($rootScope, $scope, notificationsService, dialogService, assetsService, $log, $timeout) {

        assetsService.loadJs('http://www.google.com/jsapi')
            .then(function () {
                google.load('maps', '3',
                            {
                                callback: initMap,
                                other_params: 'libraries=places&sensor=false'
                            });
            });

        var map;
        var geocoder;
        var markers = [];

        // Initialise map when Google maps is available
        function initMap() {

            var latLng;
            var zoom;

            // Check if a value is set.  If not then use the default settings
            if ($scope.model.value === '') {
                var coordArray = $scope.model.config.defaultLocation.split(',');
                if (coordArray.length > 1) {
                    latLng = new google.maps.LatLng(coordArray[0], coordArray[1]);
                } else {
                    latLng = new google.maps.LatLng(51.5286416, -0.1015987);
                }

                zoom = $scope.model.config.defaultZoom;
                if (zoom === '') {
                    zoom = '10';
                }
            } else {
                latLng = new google.maps.LatLng($scope.model.value.lat, $scope.model.value.lng);
                zoom = $scope.model.value.zoom;
                $scope.location = $scope.model.value.location;
            }

            // Create the map
            var mapDiv = document.getElementById('map-canvas');
            var mapOptions = {
                zoom: parseInt(zoom, 10),
                center: latLng,
                mapTypeControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(mapDiv, mapOptions);
            geocoder = new google.maps.Geocoder();
            if ($scope.model.value !== '') {
                placeMarker(latLng, $scope.model.value.name);
            }

            // Add the controls
            var input = document.getElementById('pac-input');
            var clear = document.getElementById('pac-button');
            var searchBox = new google.maps.places.SearchBox(input);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(clear);

            // Places search event handler
            google.maps.event.addListener(searchBox, 'places_changed', function () {
                var places = searchBox.getPlaces();
                if (places.length == 0) {
                    return;
                }

                var place = places[0];
                var location = input.value;
                latLng = place.geometry.location;
                placeMarker(latLng, place.name);
                notificationsService.success('Location', location);
                if (place.geometry.viewport) {
                    map.fitBounds(place.geometry.viewport);
                }

                $scope.model.value = {
                    lat: latLng.lat(),
                    lng: latLng.lng(),
                    name: place.name,
                    location: location,
                    zoom: map.getZoom()
                };
            });

            // Click event handler
            google.maps.event.addListener(map, 'click', function (event) {
                codeLatLng(event.latLng, geocoder);
            });

            // Zoom event handler
            google.maps.event.addListener(map, 'zoom_changed', function () {
                google.maps.event.addListenerOnce(map, 'bounds_changed', function (e) {
                    if ($scope.model.value !== '') {
                        $scope.model.value.zoom = map.getZoom();
                    }
                });
            });

            // Idle event handler
            google.maps.event.addListenerOnce(map, 'idle', function () {
                var center = map.getCenter();
                google.maps.event.trigger(map, 'resize');
                map.setCenter(center);
            });

            // Tab event handler
            $('a[data-toggle="tab"]').on('shown', function (e) {
                var center = map.getCenter();
                google.maps.event.trigger(map, 'resize');
                map.setCenter(center);
            });
        }

        // Add marker to map
        function placeMarker(latLng, name) {
            deleteOverlays();
            var marker = new google.maps.Marker({
                position: latLng,
                title: name,
                map: map,
                draggable: true
            });
            markers.push(marker);
            map.setCenter(latLng);

            // Drag event handler
            google.maps.event.addListener(marker, 'dragend', function (e) {
                codeLatLng(marker.getPosition(), geocoder);
            });
        }

        // Deletes all markers in the array
        function deleteOverlays() {
            if (markers) {
                for (i in markers) {
                    markers[i].setMap(null);
                }

                markers.length = 0;
            }
        }

        // Get location data from coordinates
        function codeLatLng(latLng, geocoder) {
            geocoder.geocode({ 'latLng': latLng },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var location = results[0].formatted_address;
                        var name = '';
                        if (results[0].address_components) {
                            name = [
                              (results[0].address_components[0] && results[0].address_components[0].short_name || ''),
                              (results[0].address_components[1] && results[0].address_components[1].short_name || '')
                            ].join(' ');
                        }

                        placeMarker(latLng, name);
                        $rootScope.$apply(function () {
                            notificationsService.success('Location', location);
                            $scope.location = location;
                            $scope.model.value = {
                                lat: latLng.lat(),
                                lng: latLng.lng(),
                                name: name,
                                location: location,
                                zoom: map.getZoom()
                            };
                        });
                    } else {
                        notificationsService.error('Invalid location');
                    }
                });
        }

        // Update map when value changes on the server
        $scope.model.onValueChanged = function (newVal, oldVal) {
            initMap();
        };

        // Clear the map
        $scope.clear = function () {
            deleteOverlays();
            $scope.location = '';
            $scope.model.value = '';
        };
    });