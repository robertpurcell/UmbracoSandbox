angular.module("umbraco").controller("Zone.CustomTextareaController",
 function ($scope) {
     // Check if a value is set.  If not then use the default settings
    $scope.model.rows = $scope.model.config.rows;
    if ($scope.model.rows === null || $scope.model.rows <= 0) {
        $scope.model.rows = 10;
    }
 });