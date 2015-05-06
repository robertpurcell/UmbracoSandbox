angular.module("umbraco").controller("Zone.TimePickerController", dateTimePickerController);

function dateTimePickerController($scope, notificationsService, assetsService, angularHelper, userService, $element) {

    //lists the custom language files that we currently support
    var customLangs = ["pt-BR"];

    //setup the default config
    var config = {
        pickDate: false,
        pickTime: true,
        pick12HourFormat: false,
        format: "hh:mm:ss"
    };

    //map the user config
    $scope.model.config = angular.extend(config, $scope.model.config);

    //handles the date changing via the api
    function applyDate(e) {
        angularHelper.safeApply($scope, function () {
            // when a date is changed, update the model
            if (e.localDate) {
                $scope.model.value = formatTime(e.localDate);
            }
        });
    }

    function formatTime(date) {
        var hour = date.getHours().toString();
        if (hour.length === 1) {
            hour = "0" + hour;
        }
        var mins = date.getMinutes().toString();
        if (mins.length === 1) {
            mins = "0" + mins;
        }
        var secs = date.getSeconds().toString();
        if (secs.length === 1) {
            secs = "0" + secs;
        }
        return hour + ":" + mins + ":" + secs;
    }
    //get the current user to see if we can localize this picker
    userService.getCurrentUser().then(function (user) {

        assetsService.loadCss('lib/datetimepicker/bootstrap-datetimepicker.min.css').then(function () {
            var filesToLoad = ["lib/datetimepicker/bootstrap-datetimepicker.min.js"];

            //if we support this custom culture, set it, then we'll need to load in that lang file
            if (_.contains(customLangs, user.locale)) {
                $scope.model.config.language = user.locale;
                filesToLoad.push("lib/datetimepicker/langs/datetimepicker." + user.locale + ".js");
            }

            assetsService.load(filesToLoad).then(
                function () {
                    //The Datepicker js and css files are available and all components are ready to use.

                    // Get the id of the datepicker button that was clicked
                    var pickerId = $scope.model.alias;

                    // Open the datepicker and add a changeDate eventlistener
                    $element.find("div:first")
                        .datetimepicker($scope.model.config)
                        .on("changeDate", applyDate);

                    if ($scope.model.value) {
                        //manually assign the date to the plugin
                        $element.find("div:first").datetimepicker("setValue", $scope.model.value);
                    }

                    //Ensure to remove the event handler when this instance is destroyted
                    $scope.$on('$destroy', function () {
                        $element.find("div:first").datetimepicker("destroy");
                    });
                });
        });


    });
}