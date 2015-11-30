angular.module("umbraco").controller("UmbracoForms.SettingTypes.PrevalueFieldMapperController",
	function ($scope, $routeParams, pickerResource, formResource, preValueSourceResource) {

	    pickerResource.getAllFields($routeParams.id).then(function (response) {
	        $scope.fields = response.data;
	        for (var i = 0; i < $scope.fields.length; i++) {
	            for (var j = 0; j < $scope.mappings.length; j++) {
	                if ($scope.mappings[j].id === $scope.fields[i].id) {
	                    $scope.fields[i].alias = $scope.mappings[j].alias;
	                    break;
	                }
	            }
	        }
	    });

	    formResource.getPrevalueSources().then(function (response) {
	        $scope.prevaluesources = response.data;
	    });

	    function setValue() {
	        var val = {};
	        val.prevalueSourceId = $scope.prevalueSourceId;
	        val.mappings = $scope.mappings;
	        $scope.setting.value = JSON.stringify(val);
	    }

	    function getPrevalues(prevalueSourceId) {
	        if (prevalueSourceId !== "") {
	            preValueSourceResource.getPreValuesByGuid(prevalueSourceId)
                    .then(function (response) {
                        $scope.prevalues = response.data;
                        setValue();
                    });
	        } else {
	            $scope.prevalues = null;
	        }
	    }

	    function findAndRemove(array, value) {
	        array.forEach(function (result, index) {
	            if (result.id === value) {
	                array.splice(index, 1);
	            }
	        });
	    }

	    if (!$scope.setting.value) {
            $scope.prevalueSourceId = "";
	        $scope.mappings = [];
	    } else {
	        var setting = JSON.parse($scope.setting.value);
	        $scope.prevalueSourceId = setting.prevalueSourceId;
	        getPrevalues($scope.prevalueSourceId);
	        $scope.mappings = setting.mappings;
	    }

        $scope.getPrevalues = function (prevalueSourceId) {
            getPrevalues(prevalueSourceId);
        };

        $scope.setValue = function () {
            setValue();
        };

        $scope.updateMappings = function () {
            for (var i = 0; i < $scope.fields.length; i++) {
                if ($scope.fields[i].alias) {
                    var mapping = {
                        "id": $scope.fields[i].id,
                        "alias": $scope.fields[i].alias
                    };
                    $scope.mappings.pushIfNotExist(mapping, function (e) {
                        return e.id === mapping.id;
                    });
                } else {
                    findAndRemove($scope.mappings, $scope.fields[i].id);
                }
            }

            setValue();
        };

	    // check if an element exists in array using a comparer function
	    // comparer : function(currentElement)
        Array.prototype.inArray = function (comparer) {
            for (var i = 0; i < this.length; i++) {
                if (comparer(this[i])) return true;
            }
            return false;
        };

	    // adds an element to the array if it does not already exist using a comparer 
	    // function
        Array.prototype.pushIfNotExist = function (element, comparer) {
            if (!this.inArray(comparer)) {
                this.push(element);
            }
        };
	});