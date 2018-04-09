angular.module('oitozero.ngSweetAlert', [])
.factory('SweetAlert', ['$rootScope', function ($rootScope) {

    var swal = window.swal;

    //public methods
    var self = {

        swal: function (arg1, arg2, arg3) {
            $rootScope.$evalAsync(function () {
                if (typeof (arg2) === 'function') {
                    swal(arg1, function (isConfirm) {
                        $rootScope.$evalAsync(function () {
                            arg2(isConfirm);
                        });
                    }, arg3);
                } else {
                    swal(arg1, arg2, arg3);
                }
            });
        },
        success: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'success');
            });
        },
        error: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'error');
            });
        },
        warning: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'warning');
            });
        },
        info: function (title, message) {
            $rootScope.$evalAsync(function () {
                swal(title, message, 'info');
            });
        }
    };

    return self;
}]);

var app = angular.module('app', ['ui.router', 'bw.paging', 'ngAnimate', 'ngMaterial', 'md.chips.select', 'md-steppers', 'angularFileUpload', 'datatables', 'oitozero.ngSweetAlert', 'angularMoment']);

app.controller('appCtrl', appCtrl);
app.controller('ImportController', ImportController);
app.controller('ExportController',ExportController);
app.controller('LoginController', LoginController);
app.controller('WorkController', WorkController);
app.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);
app.factory('LoginFactory', LoginFactory);
app.factory('PreviewFactory', PreviewFactory);
app.factory('WorkFactory', WorkFactory);
app.directive('tmpl', testComp);


function testComp($compile) {
    console.log('sss');
    var directive = {};

    directive.restrict = 'A';
    directive.templateUrl = 'Scripts/Templates/_childWorkTable.html';
    directive.transclude = true;
    directive.link = function (scope, element, attrs) {

    }
    return directive;
}
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider

           .state('home', {
               url: '/home',
               templateUrl: 'Scripts/Templates/work.html',
               controller: WorkController
           })
           .state('default', {
               url: '/',
               templateUrl: 'Scripts/Templates/work.html',
               controller: WorkController
           })
            .state('import', {
                url: '/import',
                templateUrl: 'Scripts/Templates/import.html',
                controller: ImportController
        })
        .state('export', {
            url: '/export',
            templateUrl: 'Scripts/Templates/export.html',
            controller: ExportController
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
    .state('uploadpreview', {
        url: '/uploadpreview',
        templateUrl: '/Upload/UploadPreview'
    }). state('work', {
        url: '/work',
        templateUrl: 'Scripts/Templates/work.html',
        controller: WorkController
    })
    ;

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');
});
