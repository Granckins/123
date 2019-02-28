var ReactController = function ($scope, $stateParams, $location, $http, LoginFactory) {
    $scope.myModel = {
        message: 'World'
    };
}

ReactController.$inject = ['$scope', '$stateParams', '$location', '$http', 'LoginFactory'];