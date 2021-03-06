﻿angular.module('oitozero.ngSweetAlert', [])
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

var app = angular.module('app', ['ui.router', 'bw.paging', 'ngAnimate', 'ngMaterial', 'md.chips.select', 'md-steppers', 'angularFileUpload', 'datatables', 'oitozero.ngSweetAlert', 'angularMoment','ngFileSaver','sly']);

app.controller('appCtrl', appCtrl);
app.controller('ImportController', ImportController);
app.controller('ImportWordController', ImportController);
app.controller('ExportController', ExportController);
app.controller('UpdateController', UpdateController);
app.controller('LoginController', LoginController);
app.controller('UnitController', UnitController);
app.controller('WorkController', WorkController);
app.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);
app.factory('LoginFactory', LoginFactory);
app.controller('ReactController', ReactController);
app.factory('PreviewFactory', PreviewFactory);
app.factory('WorkFactory', WorkFactory);
app.factory('HistoryUpdateFactory', HistoryUpdateFactory);
app.factory('HistoryUpdateFactoryUnit', HistoryUpdateFactoryUnit);
app.directive('tmpl', testComp);
app.directive('pics', () => {
    return {
        template: `<h2>AngularJS is here!</h2>`
    };
});

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
        .state('update', {
            url: '/update',
            templateUrl: 'Scripts/Templates/update.html',
            controller: UpdateController
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
      
        .state('unit', {
            url: '/unit',
            templateUrl: 'Scripts/Templates/unit.html',
            controller: UnitController
        })
         .state('upload', {
             url: '/upload',
             templateUrl: '/Upload/UploadFile' 
         })
    .state('uploadpreview', {
        url: '/uploadpreview',
        templateUrl: '/Upload/UploadPreview'
    }).state('uploadword', {
        url: '/uploadword',
        templateUrl: '/Upload/UploadWordFile' 
    })
    .state('uploadwordpreview', {
        url: '/uploadwordpreview',
        templateUrl: '/Upload/UploadWordPreview'
    }). state('work', {
        url: '/work',
        templateUrl: 'Scripts/Templates/work.html',
        controller: WorkController
        })
        .state('test', {
            url: '/test',
            templateUrl: 'Scripts/Templates/react.html',
            controller: ReactController
        })

    ;

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');
});
