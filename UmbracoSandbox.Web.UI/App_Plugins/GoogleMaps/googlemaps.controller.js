angular.module("umbraco").controller("My.GoogleMapsController",
    function ($rootScope, $scope, notificationsService, dialogService, assetsService, $log, $timeout) {

        assetsService.loadJs('http://www.google.com/jsapi')
            .then(function () {
                google.load("maps", "3",
                            {
                                callback: initMap,
                                other_params: "sensor=false"
                            });
            });

        var map;
        var geocoder;
        var markersArray = [];

        function initMap() {
            // Google maps is available and all components are ready to use.

            var latLng;
            if ($scope.model.value === '') {
                var valueArray = $scope.model.config.defaultLocation.split(',');
                latLng = new google.maps.LatLng(valueArray[0], valueArray[1]);
            } else {
                var valueArray = $scope.model.value.split(',');
                latLng = new google.maps.LatLng(valueArray[0], valueArray[1]);
            }

            var mapDiv = document.getElementById($scope.model.alias + '_map');
            var mapOptions = {
                zoom: parseInt($scope.model.config.defaultZoom, 10),
                center: latLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            geocoder = new google.maps.Geocoder();
            map = new google.maps.Map(mapDiv, mapOptions);

            if ($scope.model.value != '')
            {
                placeMarker(latLng);
            }

            // add a click event handler to the map object
            google.maps.event.addListener(map, "click", function (event) {
                // place a marker
                placeMarker(event.latLng);
                codeLatLng(event.latLng, geocoder);
            });

            var center = map.getCenter();
            google.maps.event.trigger(map, "resize");
            map.setCenter(center);

            $('a[data-toggle="tab"]').on('shown', function (e) {
                var center = map.getCenter();
                google.maps.event.trigger(map, "resize");
                map.setCenter(center);
            });
        }

        function placeMarker(location) {
            // first remove all markers if there are any
            deleteOverlays();
            var marker = new google.maps.Marker({
                position: location,
                map: map,
                draggable: true
            });

            var newLat = marker.getPosition().lat();
            var newLng = marker.getPosition().lng();
            $scope.coords = newLat + ", " + newLng;
            $scope.model.value = newLat + "," + newLng;

            // add marker in markers array
            markersArray.push(marker);
            map.setCenter(location);
            google.maps.event.addListener(marker, "dragend", function (e) {
                placeMarker(marker.getPosition());
                codeLatLng(marker.getPosition(), geocoder);
            });
        }

        // Deletes all markers in the array by removing references to them
        function deleteOverlays() {
            if (markersArray) {
                for (i in markersArray) {
                    markersArray[i].setMap(null);
                }
                markersArray.length = 0;
            }
        }

        function codeLatLng(latLng, geocoder) {
            geocoder.geocode({ 'latLng': latLng },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var location = results[0].formatted_address;
                        $rootScope.$apply(function () {
                            notificationsService.success("Location", location);
                        });
                    } else {
                        notificationsService.error("Invalid location!");
                    }
                });
        }

        // Here we declare a special method which will be called whenever the value has changed from the server
        // this is instead of doing a watch on the model.value = faster
        $scope.model.onValueChanged = function (newVal, oldVal) {
            // Update the display val again if it has changed from the server
            initMap();
        };

        $scope.clear = function () {
            deleteOverlays();
            $scope.coords = '';
            $scope.model.value = '';
        };
    });