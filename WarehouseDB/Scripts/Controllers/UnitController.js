var UnitController = function ($scope, $http, DTOptionsBuilder, DTColumnBuilder, $compile, SweetAlert, moment, HistoryUpdateFactory, $q, $timeout, FileUploader, FileSaver, Blob) {
    $scope.message = "fdf";
 
    $scope.getUnits= function () {
        obj = new Object();
       
        $http({
            url: "/Unit/GetUnits",
            method: "POST",
            data: obj
        }).
           then(function (response) {
              
           });
    }
 
     
 
}

UnitController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'SweetAlert', 'moment', 'HistoryUpdateFactory', '$q', '$timeout', 'FileUploader', 'FileSaver', 'Blob'];
