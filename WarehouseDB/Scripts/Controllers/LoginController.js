var LoginController = function ($scope, $stateParams, $location, $http, LoginFactory) {
    $scope.loginForm = {
        loginName: '',
        password: '',
        rememberMe: false,
        returnUrl: $stateParams.returnUrl,
        loginFailure: false
    };

    $scope.login = function () {
        var result = LoginFactory($scope.loginForm.loginName, $scope.loginForm.password, $scope.loginForm.rememberMe);
        result.then(function (result) {
            if (result.success) {
                if ($scope.loginForm.returnUrl == undefined) {
                    window.location.replace('/home')
                } else {
                 window.location.replace('/home')
                }
            } else {
                $scope.loginForm.loginFailure = true;
            }
        });
    }
    $scope.logoff = function () {
        $http.post(
           '/Account/LogOff' 
       ).
        then(function (data) {
            if (data.data == "True") {
                window.location.replace('/home')
            } else {
                window.location.replace('/home')
            }
        })
    }
}

LoginController.$inject = ['$scope', '$stateParams', '$location', '$http', 'LoginFactory'];