(function () {

    var app = angular.module("plunker", ["ui.bootstrap"]);

    app.controller("MainCtrl", function ($scope, $http) {

        $http({
            method: "GET",
            url: "/api/Logs/"
        }).then(function mySucces(response) {

            $scope.allLogs = response.data;
            $scope.totalItems = $scope.allLogs.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 20;

        }, function myError(response) {
            alert(response.statusText);
        });



        $scope.show = function () {
            $scope.$watch("currentPage", function () {
                setPagingData($scope.currentPage);
            });
        }


        $scope.show();

        function setPagingData(page) {
            $scope.Logs = $scope.allLogs.slice(
                (page - 1) * $scope.itemsPerPage,
                page * $scope.itemsPerPage
            );
        }
    });


})();