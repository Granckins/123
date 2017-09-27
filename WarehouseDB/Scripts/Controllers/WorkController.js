var WorkController = function ($scope, $http, DTOptionsBuilder, DTColumnBuilder, $compile,SweetAlert) {
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
    $scope.buferList = [];
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
          if (obj.value.Data_priyoma != null)
              obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6))).toLocaleDateString();
          if (obj.value.Data_vydachi != null)
          obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6))).toLocaleDateString();

      })
  });
        $scope.addRow = function () {

         
            obj = new Object();
            obj["showEdit"] = false;
            obj["showSub"] = false;
            $scope.userList.unshift(obj);
        }
        $scope.delete = function (user) {
            obj = new Object();
            obj["page"] = 1;
            obj["limit"] = 10;
            obj["entity"] = user;
            SweetAlert.swal({
                title: "Вы уверены, что хотите удалить запись безвозвратно из базы данных?",
                text: "",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Да, удалить!",
                cancelButtonText: "Нет, отменить!",
                closeOnConfirm: false
            }, function () {
                SweetAlert.swal("Запись удалена!");
            });
        }

        $scope.toggleEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
            $scope.buferList.push(emp.key);
        };
        $scope.deleteSub = function (user, idx) {
            var idx2 = -1;
            for (var i = 0, len = $scope.userList.length; i < len; i++) {
                if ($scope.userList[i].id === user.id) {
                    idx2 = i;
                    break;
                }
            }
            //$scope.userList[idx2].Soderzhimoe.splice(idx, 1);
            $scope.userList[idx2].value.Soderzhimoe.splice(idx, 1);
//            obj = new Object();
//            obj["edit_event"] = user;
//            obj["subidx"] = idx;

//            $http({
//                url: '/Work/DeleteEventSubDocument',
//                method: "POST",
//                data: obj
//            }).
//then(function (response) {
//    var idx2 = -1;
//    for (var i = 0, len = $scope.userList.length; i < len; i++) {
//        if ($scope.userList[i].id === user.id) {
//            idx2 = i;
//            break;
//        }
//    }
//    $scope.userList[idx2].value = response.data;
  
//});
             };
        $scope.AddSub = function (user ) {
            obj = new Object();
            obj["Naimenovanie_sostavnoj_edinicy"] = "";
            obj["Oboznachenie_sostavnoj_edinicy"] = "";
            obj["Kolichestvo_sostavnyh_edinic"] = 1;
            user.value.Soderzhimoe.push(obj);
        };
        $scope.cancelEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
            var idx = $scope.buferList.indexOf(emp.key);
            $scope.buferList.splice(idx);
            $http({
                url: '/Work/GetEventDocument',
                method: "GET",
                params: {
                    id: emp.key
                }
            }).
 then(function (response) {
     var idx2 = -1; 
     for (var i = 0, len = $scope.userList.length; i < len; i++) {
         if ($scope.userList[i].id === emp.id) {
             idx2 = i;
             break;
         }
     }
     $scope.userList[idx2].value = response.data;
     if (response.data.Data_priyoma!=null)
         $scope.userList[idx2].value.Data_priyoma = new Date(parseInt(response.data.Data_priyoma.substr(6))).toLocaleDateString();
     if (response.data.Data_vydachi != null)
     $scope.userList[idx2].value.Data_vydachi = new Date(parseInt(response.data.Data_vydachi.substr(6))).toLocaleDateString();
 });
        };
        $scope.acceptEdit = function (emp) {
            emp.showEdit = emp.showEdit ? false : true;
            var idx = $scope.buferList.indexOf(emp.key);
            $scope.buferList.splice(idx);

            $http({
                url: '/Work/ChangeEventDocument',
                method: "POST",
                data: emp
            }).
then(function (response) {
    if(response.data!=""){
    var idx2 = -1;
    for (var i = 0, len = $scope.userList.length; i < len; i++) {
        if ($scope.userList[i].id === emp.id) {
            idx2 = i;
            break;
        }
    }
    }
    else {
        $scope.userList.shift();
    }
    
});

        };
    $scope.childInfo = function (emp) {
        emp.showSub = emp.showSub ? false : true;
        
    }

  

}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'SweetAlert'];
 