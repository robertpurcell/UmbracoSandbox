//adds the resource to umbraco.resources module:
angular.module('umbraco.resources').factory('dataResource',
    function ($q, $http) {
        //the factory object returned
        return {
            //this cals the Api Controller we setup earlier
            getAll: function () {
                return $http.get("BackOffice/Data/DataApi/GetAll");
            }
        };
    }
);