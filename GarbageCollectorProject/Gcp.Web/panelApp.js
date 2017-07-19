var app = angular.module("panelApp", []);

app.controller("panelController", function ($scope, $http) {

    $scope.getAllpersonel = function () {
        $http({
            method: "GET",
            url: "/Personel/GetAllAsync"
        }).then(function (response) {
            $scope.personel = response.data;
        });
        return $scope.personel;
    };
    $scope.getAllpersonel();

});
