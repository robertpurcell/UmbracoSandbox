angular.module("umbraco").controller("My.GoogleMapsController",
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
            if ($scope.model.value === '') {
                var coordArray = $scope.model.config.defaultLocation.split(',');
                latLng = new google.maps.LatLng(coordArray[0], coordArray[1]);
            } else {
                var valueArray = $scope.model.value.split('|');
                var coordArray = valueArray[0].split(',');
                latLng = new google.maps.LatLng(coordArray[0], coordArray[1]);
                if (valueArray.length > 1)
                {
                    $scope.location = valueArray[1];
                }
            }

            // Create the map
            var mapDiv = document.getElementById($scope.model.alias + '_map');
            var mapOptions = {
                zoom: parseInt($scope.model.config.defaultZoom, 10),
                center: latLng,
                mapTypeControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            geocoder = new google.maps.Geocoder();
            map = new google.maps.Map(mapDiv, mapOptions);

            if ($scope.model.value != '') {
                placeMarker(latLng);
            }

            // Add the controls
            var input = document.getElementById('pac-input');
            var clear = document.getElementById('pac-button');
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(clear);
            var searchBox = new google.maps.places.SearchBox(input);

            // Places search event handler
            google.maps.event.addListener(searchBox, 'places_changed', function () {
                var places = searchBox.getPlaces();
                if (places.length == 0) {
                    return;
                }

                latLng = places[0].geometry.location;
                var address = places[0].formatted_address;
                placeMarker(latLng);
                console.log(places[0]);
                notificationsService.success('Location', address);
                $scope.model.value = latLng.lat() + ',' + latLng.lng() + '|' + address;
                $scope.location = address;
            });

            // Click event handler
            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng);
                codeLatLng(event.latLng, geocoder);
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
        function placeMarker(latLng) {
            deleteOverlays();
            var marker = new google.maps.Marker({
                position: latLng,
                title: latLng.formatted_address,
                map: map,
                draggable: true
            });

            markers.push(marker);
            map.setCenter(latLng);
            google.maps.event.addListener(marker, 'dragend', function (e) {
                placeMarker(marker.getPosition());
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
                        var address = results[0].formatted_address;
                        console.log(results[0]);
                        $rootScope.$apply(function () {
                            notificationsService.success('Location', address);
                            $scope.model.value = latLng.lat() + ',' + latLng.lng() + '|' + address;
                            $scope.location = address;
                        });
                    } else {
                        notificationsService.error('Invalid location!');
                    }
                });
        }

        // Update map when value changes on the server
        $scope.model.onValueChanged = function (newVal, oldVal) {
            initMap();
        };

        // Clear the map and 
        $scope.clear = function () {
            deleteOverlays();
            $scope.location = '';
            $scope.model.value = '';
        };
    });