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
            url: '/Work/GetDocument',
            method: "GET",
            params: {
                page: 1,
                limit: 10
            }
        }).
  then(function(response) {
      $scope.userList = response.data.rows;
    
  });
       
     
    $scope.childInfo = function (user, event) {

        var scope = $scope.$new(true);
        scope.user = user;

        var link = angular.element(event.currentTarget),
          	icon = link.find('.glyphicon'),
          	tr = link.parent().parent(),
          	table = $scope.vm.dtInstance.DataTable,
          	row = table.row(tr);
        //
        if (row.child.isShown()) {
            icon.removeClass('glyphicon-minus-sign').addClass('glyphicon-plus-sign');
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            icon.removeClass('glyphicon-plus-sign').addClass('glyphicon-minus-sign');
            row.child($compile('<div tmpl class="clearfix"></div>')(scope)).show();
            tr.addClass('shown');
        }
    }

  

}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile'];
 