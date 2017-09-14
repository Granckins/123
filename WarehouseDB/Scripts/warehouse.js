﻿
var app = angular.module('app', ['ui.router', 'ngMaterial', 'md-steppers', 'angularFileUpload']);

app.controller('appCtrl', appCtrl);
app.controller('ImportController', ImportController);

app.controller('LoginController', LoginController); 
app.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);
app.factory('LoginFactory', LoginFactory);
  
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider

           .state('home', {
               url: '/home',
               templateUrl: 'Scripts/Templates/home.html',
               controller: appCtrl
           })
           .state('default', {
               url: '/',
               templateUrl: 'Scripts/Templates/home.html',
               controller: appCtrl
           })
            .state('import', {
                url: '/import',
                templateUrl: 'Scripts/Templates/import.html',
                controller: ImportController
            })
          .state('logoff', {
              url: '/logoff',
              templateUrl: '/Account/LogOff'
          })
           .state('Login', {
               url: '/Login',
               templateUrl: '/Account/Login',
               controller: LoginController
           })
           
         .state('upload', {
             url: '/upload',
             templateUrl: '/Upload/UploadFile' 
         })
    ;

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');
});
