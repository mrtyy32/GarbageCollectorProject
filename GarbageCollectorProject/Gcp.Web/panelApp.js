var app = angular.module("panelApp", []);

app.Controller("panelController",
    function($scope, $http) {
        $scope.getAllpersonel = function() {
            $http({
                method: "GET",
                url: "Personel/GetAll",
                data: product
            }).then(function(response) {
                $scope.personel = response.data;
            });
            return $scope.personel;
        };

        $scope.getAllpersonel();
    });