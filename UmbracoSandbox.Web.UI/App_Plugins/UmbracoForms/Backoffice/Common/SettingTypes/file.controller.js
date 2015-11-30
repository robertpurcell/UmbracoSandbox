angular.module("umbraco").controller("UmbracoForms.SettingTypes.File",
	function ($scope, dialogService) {

	    function populateFile(item) {
	        for (var i = 0, iLen = item.properties.length; i < iLen; i++) {
	            if (item.properties[i].alias === "umbracoFile") {
	                $scope.setting.value = item.properties[i].value;
                    break;
	            }
	        }
	    }

	    $scope.openMediaPicker = function () {
	        dialogService.mediaPicker({ callback: populateFile });
	    };
	});