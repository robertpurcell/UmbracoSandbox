angular.module("umbraco").controller('Data.DataTree.EditController',
     function dataEditController($scope, $routeParams) {
         $scope.content = { tabs: [{ id: 1, label: "Tab 1" }, { id: 2, label: "Tab 2" }] };
         //set a property on the scope equal to the current route id
         $scope.id = $routeParams.id;
     }
 );
