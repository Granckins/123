  var ExportController = function ($scope, $q, $timeout, FileUploader, moment, $http, SweetAlert, FileSaver, Blob) {
 
    
    $scope.get_all_events = function () {
        $scope.all_events = $scope.all_events ? false : true; 
       
    }
    $scope.Data_ismenen_str1 = null;
    $scope.Data_ismenen_str2 = null; 
    $scope.getdateiz= function () {
        if ($scope.Data_ismenen_str1 != null && $scope.Data_ismenen_str2 != null) {
            var Data_ismenen_str1 = new Date($scope.Data_ismenen_str1);
            var curr_date = $scope.Data_ismenen_str1.getDate();
            var DD = "" + curr_date;
            if (curr_date < 10)
                DD = "0" + curr_date;
            var curr_month = $scope.Data_ismenen_str1.getMonth() + 1;
            var MM = "" + curr_month;
            if (curr_month < 10)
                MM = "0" + curr_month;
            var curr_year = $scope.Data_ismenen_str1.getFullYear();
            var sdsd1 = curr_year + "-" + MM + "-" + DD;
            var Data_priyoma_str2 = new Date($scope.Data_priyoma_str2);
            var curr_date1 = $scope.Data_ismenen_str2.getDate();
            DD = "" + curr_date1;
            if (curr_date1 < 10)
                DD = "0" + curr_date1;
            var curr_month1 = $scope.Data_ismenen_str2.getMonth() + 1;
            MM = "" + curr_month1;
            if (curr_month1 < 10)
                MM = "0" + curr_month1;
            var curr_year1 = $scope.Data_ismenen_str2.getFullYear();
            var sdsd2 = curr_year1 + "-" + MM + "-" + DD;
            return sdsd1 + ";" + sdsd2;
        }
        return ";";
    }
    $scope.updateCalcs = function () {
        obj = new Object();
     
        obj["dateiz"] = $scope.getdateiz();
        obj["all_events"] = $scope.all_events ;
        $http({
            url: "/Export/ExportDocument",
            method: "Post",
            data: obj
        }).
            then(function (response) {

                var data = new Blob([JSON.stringify(response.data)], { type: 'application/json;charset=utf-8' });
                var filename = "";
                var currentdate = new Date();
                var datetime =  currentdate.getDate() + "/"
                    + (currentdate.getMonth() + 1) + "/"
                    + currentdate.getFullYear() + " @ "
                    + currentdate.getHours() + ":"
                    + currentdate.getMinutes() + ":"
                    + currentdate.getSeconds();
                FileSaver.saveAs(data, 'backup-' + datetime+'.json');
            });
    };
    $scope.loadMore = function () {
        if ($scope.logimport + 5 < $scope.logimport.length) {
            vm.logimport += 5;
        } else {
            $scope.logimport = $scope.logimport.length;
        }
    };
    $scope.upload = function () {
        angular.element(document.querySelector('#fileInput')).click();
    };
    
}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
ExportController.$inject = ['$scope', '$q', '$timeout', 'FileUploader', 'moment', '$http', 'SweetAlert', 'FileSaver', 'Blob'];