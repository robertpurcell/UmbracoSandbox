// Adds the resource to the umbraco.resources module:
angular.module('umbraco.resources').factory('dataResource',
    function ($q, $http) {
        return {
            // This calls the Api Controller
            getAll: function () {
                return $http.get("BackOffice/Data/DataApi/GetAll");
            }
        };
    }
);