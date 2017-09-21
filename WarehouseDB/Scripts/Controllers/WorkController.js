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
      angular.forEach($scope.userList, function (obj) {
          obj["showEdit"] = true;
          obj["showSub"] = false;
      })
  });
        $scope.toggleEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
        };

     
    $scope.childInfo = function (emp) {
        emp.showSub = emp.showSub ? false : true;
        
    }

  

}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile'];
 