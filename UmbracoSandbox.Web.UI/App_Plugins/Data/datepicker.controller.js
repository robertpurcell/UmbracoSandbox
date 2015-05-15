angular.module("umbraco").controller("Custom.DatepickerController",
        function ($scope, notificationsService, assetsService, angularHelper, userService, $element) {

            //lists the custom language files that we currently support
            var customLangs = ["pt-BR"];

            //setup the default config
            var config = {
                pickDate: true,
                pickTime: false,
                pick12HourFormat: false,
                format: "dd/MM/yyyy"
            };

            //handles the date changing via the api
            function applyDate(e) {
                angularHelper.safeApply($scope, function () {
                    // when a date is changed, update the model
                    if (e.localDate) {
                        if (config.format == "yyyy-MM-dd hh:mm:ss") {
                            $scope.criteria[$element.attr('id')] = e.localDate.toIsoDateTimeString();
                        }
                        else {
                            $scope.criteria[$element.attr('id')] = e.localDate.toIsoDateString();
                        }
                    }
                });
            }

            //get the current user to see if we can localize this picker
            userService.getCurrentUser().then(function (user) {

                assetsService.loadCss('lib/datetimepicker/bootstrap-datetimepicker.min.css').then(function () {
                    var filesToLoad = ["lib/datetimepicker/bootstrap-datetimepicker.min.js"];

                    //if we support this custom culture, set it, then we'll need to load in that lang file
                    if (_.contains(customLangs, user.locale)) {
                        config.language = user.locale;
                        filesToLoad.push("lib/datetimepicker/langs/datetimepicker." + user.locale + ".js");
                    }

                    assetsService.load(filesToLoad).then(
                        function () {
                            //The Datepicker js and css files are available and all components are ready to use.

                            // Open the datepicker and add a changeDate eventlistener
                            $element.find("div:first")
                                .datetimepicker(config)
                                .on("changeDate", applyDate);

                            if ($scope.criteria[$element.attr('id')]) {
                                //manually assign the date to the plugin
                                $element.find("div:first").datetimepicker("setValue", $scope.criteria[$element.attr('id')]);
                            }

                            //Ensure to remove the event handler when this instance is destroyted
                            $scope.$on('$destroy', function () {
                                $element.find("div:first").datetimepicker("destroy");
                            });
                        });
                });
            });
        });