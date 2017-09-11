 
var app = angular.module('app',   ['ngRoute']);

app.controller('appCtrl', appCtrl);

app.controller('LoginController', LoginController);

app.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);
app.factory('LoginFactory', LoginFactory);

var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider.
        when('/home', {
            templateUrl: '/Home/Index'
        })
   .when('/logoff', {
       templateUrl: '/Account/LogOff'
    })
        .when('/Login', {
            templateUrl: '/Account/Login',
            controller: LoginController
        });
    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');
}
configFunction.$inject = ['$routeProvider', '$httpProvider'];

app.config(configFunction);
