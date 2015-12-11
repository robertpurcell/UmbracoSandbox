angular.module("umbraco").controller("Zone.GoogleMapsAreaPickerController",
    function ($rootScope, $scope, notificationsService, dialogService, assetsService, $log, $timeout) {

        assetsService.loadJs('http://www.google.com/jsapi')
            .then(function () {
                google.load('maps', '3',
                            {
                                callback: initMap,
                                other_params: 'libraries=places,drawing&sensor=false'
                            });
            });

        var map;
        var geocoder;

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
            }

            // Create the map
            var mapDiv = document.getElementById($scope.model.alias + '_map');
            var mapOptions = {
                zoom: parseInt(zoom, 10),
                center: latLng,
                mapTypeControl: false,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(mapDiv, mapOptions);
            geocoder = new google.maps.Geocoder();
            if ($scope.model.value !== '') {
            }

            // Add the controls
            var clear = document.getElementById('pac-button');
            map.controls[google.maps.ControlPosition.TOP_CENTER].push(clear);

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

            var drawingManager = new google.maps.drawing.DrawingManager({
                drawingMode: google.maps.drawing.OverlayType.POLYGON,
                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_CENTER,
                    drawingModes: [
                      google.maps.drawing.OverlayType.POLYGON,
                      google.maps.drawing.OverlayType.POLYLINE,
                      google.maps.drawing.OverlayType.RECTANGLE
                    ]
                },
                polygonOptions: {
                    editable: true
                }
            });
            drawingManager.setMap(map);

            google.maps.event.addListener(drawingManager, 'polygoncomplete', function (polygon) {
                var coords = polygon.getPath();
            });
        }

        // Update map when value changes on the server
        $scope.model.onValueChanged = function (newVal, oldVal) {
            initMap();
        };

        // Clear the map
        $scope.clear = function () {

            $scope.model.value = '';
        };
    });