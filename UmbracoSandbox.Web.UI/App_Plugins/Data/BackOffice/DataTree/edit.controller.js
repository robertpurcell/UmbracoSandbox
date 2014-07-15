angular.module("umbraco")
    .controller("Data.DataTree.EditController", function ($scope, dataResource) {
        dataResource.getAll().then(function (response) {
            $scope.currentPage = 0;
            $scope.itemsPerPage = 10;
            $scope.gap = 10;
            $scope.gapBeforeAndAfter = Math.floor($scope.gap / 2);
            $scope.data = response.data;
            $scope.pagedItems = [];

            $scope.init = function () {
                $scope.currentPage = 0;
                $scope.groupToPages();
            };

            $scope.groupToPages = function () {
                $scope.pagedItems = [];

                for (var i = 0; i < $scope.data.length; i++) {
                    if (i % $scope.itemsPerPage === 0) {
                        $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.data[i]];
                    } else {
                        $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.data[i]);
                    }
                }
            };

            $scope.range = function (currentPage) {
                var pages = [];
                var start;
                var end;

                if (currentPage - $scope.gapBeforeAndAfter <= 0) {
                    start = 0;
                } else if ($scope.pagedItems.length < $scope.gap) {
                    start = 0;
                } else if (currentPage + $scope.gapBeforeAndAfter >= $scope.pagedItems.length) {
                    start = $scope.pagedItems.length - $scope.gap;
                } else {
                    start = currentPage - $scope.gapBeforeAndAfter;
                }

                if ($scope.pagedItems.length <= $scope.gap) {
                    end = $scope.pagedItems.length;
                } else if ((currentPage + 1) + $scope.gapBeforeAndAfter <= $scope.gap) {
                    end = $scope.gap;
                } else if (currentPage + $scope.gapBeforeAndAfter >= $scope.pagedItems.length) {
                    end = $scope.pagedItems.length;
                } else {
                    end = currentPage + $scope.gapBeforeAndAfter;
                }

                for (var i = start; i < end; i++) {

                    pages.push(i);
                }

                return pages;
            };

            $scope.prevPage = function () {
                if ($scope.currentPage > 0) {
                    $scope.currentPage--;
                }
            };

            $scope.nextPage = function () {
                if ($scope.currentPage < $scope.pagedItems.length - 1) {
                    $scope.currentPage++;
                }
            };

            $scope.setPage = function () {
                $scope.currentPage = this.n;
            };

            $scope.init();
        });
    });

angular.module('umbraco')
    .filter('myCurrency', ['$filter', function ($filter) {
        return function (input) {
            input = parseFloat(input);
            if (input % 1 === 0) {
                input = input.toFixed(0);
            }
            return '£' + input;
        };
    }]);