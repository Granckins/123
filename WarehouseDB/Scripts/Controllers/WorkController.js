var WorkController = function ($scope, $http, DTOptionsBuilder, DTColumnBuilder, $compile) {
    $scope.message = "fdf";
    $scope.vm = {};
    $scope.vm.dtInstance = {};
    $scope.vm.dtOptions = DTOptionsBuilder.newOptions()
      .withOption('order', [0, 'asc']);
    $scope.customer = {
        name: 'Naomi',
        address: '1600 Amphitheatre'
    };
    function formatJSONDate(jsonDate) {
        var newDate = dateFormat(jsonDate, "mm/dd/yyyy");
        return newDate;
    }
    $scope.userList = [];
  
        $http({
            url: '/Work/GetDocuments',
            method: "GET",
            params: {
                page: 1,
                limit: 10
            }
        }).
  then(function(response) {
      $scope.userList = response.data.rows;
      //$scope.userList.value.forEach(function callback(currentValue, index, array) {
      //    currentValue.Data_priyoma = new Date(parseInt(currentValue.Data_priyoma.substr(6)));
      //    currentValue.Data_vydachi = new Date(parseInt(currentValue.Data_vydachi.substr(6)));
      //});
      angular.forEach($scope.userList, function (obj) {
          obj["showEdit"] = true;
          obj["showSub"] = false;
          obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6))).toLocaleDateString();
          obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6))).toLocaleDateString();

      })
  });
        $scope.toggleEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
        };

        $scope.cancelEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
        };
    $scope.childInfo = function (emp) {
        emp.showSub = emp.showSub ? false : true;
        
    }

  

}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile'];
 