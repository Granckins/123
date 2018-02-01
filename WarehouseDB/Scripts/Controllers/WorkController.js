﻿var WorkController = function ($scope, $http, DTOptionsBuilder, DTColumnBuilder, $compile, SweetAlert,moment) {
    $scope.message = "fdf";
    $scope.vm = {};
    $scope.vm.dtInstance = {};

    $scope.archive = false;

    $scope.currentPage = 1;
    $scope.numPerPage = 10;
  $scope.maxSize = 5;


  $scope.archive_str="";
  $scope.Nomer_upakovki_str = "";
  $scope.Naimenovanie_izdeliya_str = "";
  $scope.Zavodskoj_nomer_str = "";
  $scope.Oboznachenie_str = "";
  $scope.Soderzhimoe_str = "";
  $scope.Sistema_str = "";
  $scope.Prinadlezhnost_str = "";
  $scope.Mestonahozhdenie_na_sklade_str = "";
  $scope.Data_priyoma_str1 = "";
  $scope.Data_vydachi_str1 = "";
  $scope.Data_priyoma_str2 = "";
  $scope.Data_vydachi_str2 = "";
  $scope.Primechanie_str = "";


    $scope.vm.dtOptions = DTOptionsBuilder.newOptions()
      .withOption('order', [0, 'asc']);
    $scope.customer = {
        name: 'Naomi',
        address: '1600 Amphitheatre'
    };
    $scope.maxSize = 5;     // Limit number for pagination display number.  
    $scope.totalCount = 0;  // Total number of items in all pages. initialize as a zero  
    $scope.pageIndex = 1;   // Current page number. First page is 1.-->  
    $scope.pageSizeSelected = 10; // Maximum number of items per page.  
    function formatJSONDate(jsonDate) {
        var newDate = dateFormat(jsonDate, "mm/dd/yyyy");
        return newDate;
    }
    $scope.buferList = [];
    $scope.userList = [];
    $scope.isSearch = false;
    $scope.search = function (str) {
        switch(str){
            case 'Наименование изделия':
                 break 
                    default:
                 break 
        }
    };
    $scope.getList = function () {
        obj = new Object();
        obj["page"] = $scope.pageIndex;
        obj["limit"] = $scope.pageSizeSelected;

        obj["archive_str"] = $scope.archive_str;
        obj["Nomer_upakovki_str"] = $scope.Nomer_upakovki_str;
        obj["Naimenovanie_izdeliya_str"] = $scope.Naimenovanie_izdeliya_str;
        obj["Zavodskoj_nomer_str"] = $scope.Zavodskoj_nomer_str;
        obj["Oboznachenie_str"] = $scope.Oboznachenie_str;
        obj["Soderzhimoe_str"] = $scope.Soderzhimoe_str;
        obj["Sistema_str"] = $scope.Sistema_str;
        obj["Prinadlezhnost_str"] = $scope.Prinadlezhnost_str;
        obj["Mestonahozhdenie_na_sklade_str"] = $scope.Mestonahozhdenie_na_sklade_str;
        obj["Data_priyoma_str1"] = $scope.Data_priyoma_str1;
        obj["Data_vydachi_str1"] = $scope.Data_vydachi_str1;
        obj["Data_priyoma_str2"] = $scope.Data_priyoma_str2;
        obj["Data_vydachi_str2"] = $scope.Data_vydachi_str2;
        obj["Primechanie_str"] = $scope.Primechanie_str;

        obj["entity"] = null;
        $http({
            url: "/Work/GetDocuments",
            method: "POST",
           data:obj
        }).
           then(function (response) {
               $scope.userList = response.data.rows;
               $scope.totalCount=response.data.total_rows;
               //$scope.userList.value.forEach(function callback(currentValue, index, array) {
               //    currentValue.Data_priyoma = new Date(parseInt(currentValue.Data_priyoma.substr(6)));
               //    currentValue.Data_vydachi = new Date(parseInt(currentValue.Data_vydachi.substr(6)));
               //});
               angular.forEach($scope.userList, function (obj) {
                   obj["showEdit"] = true;
                   obj["showSub"] = false;
                   obj["History"] = [];
                   obj["showHistory"] = false;
                   if (obj.value.Data_priyoma != null)
                       obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                   if (obj.value.Data_vydachi != null)
                       obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));

               })
           });
    }
    
    $scope.$watch('pageIndex', function (newVal, oldVal) {
        $scope.pageIndex = newVal;
        $scope.getList();

    });
    $scope.pageSizes = ('5 10 25 50').split(' ').map(function (state) { return { abbrev: state }; });
    $scope.filters = ("Дата приёма;Дата выдачи").split(';').map(function (state) { return { abbrev: state }; });
    $scope.searchfilter = "";
    $scope.searchfilter1 = "";
    $scope.searchfilter2 = "";
    $scope.searchfilter3= "";
    $scope.getList();
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getList();
    };


   
    $scope.selectedOption = '';
    $scope.searchText = '';
    $scope.launchAPIQueryParams = {
        types: [],
    };

    $scope.launchTypeOptions = [
        { name: 'Номер упаковки', value: 'Номер упаковки' },
        { name: 'Наименование', value: 'Наименование' },
        { name: 'Заводской номер', value: 'Заводской номер' },
        { name: 'Обозначение', value: 'Обозначение' },
            { name: 'Система', value: 'Система' },
                        { name: 'Содержимое', value: 'Содержимое' },
                            { name: 'Местонахождение', value: 'Местонахождение' },
                                           { name: 'Примечание', value: 'Примечание' },
    ];
    $scope.searchTextChange = function (searchText) {
        $scope.searchText = searchText;
    };
    $scope.newVeg = function (chip) {
        var obj = new Object();
        
      
        if (chip.name == null) {
            obj["name"] = chip;
            obj["value"] = 'Номер упаковки';
        }
        else
            obj = chip;
        obj.name = $scope.searchText + " (" + obj.value + ")";
        return obj;
            
        
    };
    //This method is calling from pagination number  
    $scope.pageChanged = function () {
        $scope.getEmployeeList();
    };
    $scope.addRow = function () {

        obj = new Object();
        obj["showEdit"] = false;
        obj["showSub"] = false; 
        obj["History"] = [];
        obj["showHistory"] = false;

        $scope.userList.unshift(obj);
    }
    $scope.delete = function (user) {
        obj = new Object();
        obj["page"] = $scope.pageIndex;
        obj["limit"] = $scope.pageSizeSelected;

        obj["archive_str"] = $scope.archive_str;
        obj["Nomer_upakovki_str"] = $scope.Nomer_upakovki_str;
        obj["Naimenovanie_izdeliya_str"] = $scope.Naimenovanie_izdeliya_str;
        obj["Zavodskoj_nomer_str"] = $scope.Zavodskoj_nomer_str;
        obj["Oboznachenie_str"] = $scope.Oboznachenie_str;
        obj["Soderzhimoe_str"] = $scope.Soderzhimoe_str;
        obj["Sistema_str"] = $scope.Sistema_str;
        obj["Prinadlezhnost_str"] = $scope.Prinadlezhnost_str;
        obj["Mestonahozhdenie_na_sklade_str"] = $scope.Mestonahozhdenie_na_sklade_str;
        obj["Data_priyoma_str1"] = $scope.Data_priyoma_str1;
        obj["Data_vydachi_str1"] = $scope.Data_vydachi_str1;
        obj["Data_priyoma_str2"] = $scope.Data_priyoma_str2;
        obj["Data_vydachi_str2"] = $scope.Data_vydachi_str2;
        obj["Primechanie_str"] = $scope.Primechanie_str;

        obj["entity"] = user;


        SweetAlert.swal({
            title: "Вы уверены, что хотите удалить запись безвозвратно из базы данных?",
            text: "",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: "Да, удалить!",
            cancelButtonText: "Нет, отменить!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
function (isConfirm) {
    if (isConfirm) {

        if (user.id != null)
        {
            $http({
                url: '/Work/DeleteEventDocument',
                method: "POST",
                data: obj
            }).
                   then(function (response) {
                       if (response != null) {
                           $scope.userList = response.data.rows;
                           angular.forEach($scope.userList, function (obj) {
                               obj["showEdit"] = true;
                               obj["showSub"] = false;
                               obj["History"] = [];
                               obj["showHistory"] = false;
                               if (obj.value.Data_priyoma != null)
                                   obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                               if (obj.value.Data_vydachi != null)
                                   obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));

                           });
                           SweetAlert.swal("Запись удалена!");
                           $scope.getList();
                       }
                       else {
                           SweetAlert.swal("Ошибка", "Возникли проблемы, запись не удалена!", "error");
                       }
                   });
        }
        else {
            $scope.userList.splice(0, 1);
            SweetAlert.swal("Запись удалена!");

        }
    } else {
        SweetAlert.swal("Запись не  удалена!"); 
    }
});


 
    }








    $scope.toggleEdit = function (emp) {
        emp.showEdit = emp.showEdit ? false : true;
        $scope.buferList.push(emp.key);
    };
    $scope.isNumber = function (n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    $scope.isDate = function (n) {
        return angular.isDate(n);;
    }
    $scope.isInteger = function (n) {
        return n % 1 === 0;
    }
    $scope.IsHistory = function (emp) {
        if (emp.History.length > 0)
            return true;
        else
            return false;
    }
    $scope.gethistory = function (emp) {
        emp.showHistory = emp.showHistory ? false : true;
        if( emp.showHistory==true){
        $http({
            url: "/Work/GetEventHistory?id=" + emp.id,
            method: "GET",
            params: {
                page: $scope.pageIndex,
                limit: $scope.pageSizeSelected
            }
        }).
         then(function (response) {

             emp["History"] = response.data.rows;
             emp["History"]["showSub"] = false;

             angular.forEach(emp["History"], function (obj) {
               
                 if (obj.value.Data_priyoma != null)
                     obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                 if (obj.value.Data_vydachi != null)
                     obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));

             })
             
         });
    }
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
    $scope.IsSub = function (user) {
        if (user.value.Soderzhimoe != null && user.value.Soderzhimoe.length>0) {
            return true;
        }
        else {
            
                return false;
        }
    };
    $scope.AddSub = function (user) {
        if (user.value != null && user.value.Soderzhimoe != null) {
            obj = new Object();
            obj["Naimenovanie_sostavnoj_edinicy"] = "";
            obj["Oboznachenie_sostavnoj_edinicy"] = "";
            obj["Kolichestvo_sostavnyh_edinic"] = 1;
            user.value.Soderzhimoe.push(obj);
        }
        else {
            if (user.value == null)
            user["value"] = new Object();
            user.value["Soderzhimoe"] = [];
            obj = new Object();
            obj["Naimenovanie_sostavnoj_edinicy"] = "";
            obj["Oboznachenie_sostavnoj_edinicy"] = "";
            obj["Kolichestvo_sostavnyh_edinic"] = 1;
            user.value.Soderzhimoe.push(obj);
        }
    };
    $scope.cancelEdit = function (emp) {
        if (emp.key!=null) {
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
            if (response.data.Data_priyoma != null)
                $scope.userList[idx2].value.Data_priyoma = new Date(parseInt(response.data.Data_priyoma.substr(6)));
            if (response.data.Data_vydachi != null)
                $scope.userList[idx2].value.Data_vydachi = new Date(parseInt(response.data.Data_vydachi.substr(6)));
        });
        }
        else {
            $scope.userList.splice(0,1);
        }
    };
    $scope.checkevent = function (user) {
    var flag=user.value.Nomer_upakovki && $scope.isNumber(user.value.Nomer_upakovki) && $scope.isInteger(user.value.Nomer_upakovki)
        && user.value.Naimenovanie_izdeliya && user.value.Zavodskoj_nomer &&user.value.Kolichestvo && $scope.isNumber(user.value.Kolichestvo)   
           && user.value.Kolichestvo != 0 && user.value.Sistema && user.value.Prinadlezhnost &&
          ( user.value.Stoimost? $scope.isNumber(user.value.Stoimost) :true)&& user.value.Otvetstvennyj && user.value.Mestonahozhdenie_na_sklade
        && (user.value.Ves_brutto ? $scope.isNumber(user.value.Ves_brutto) : true) && (user.value.Ves_netto ? $scope.isNumber(user.value.Ves_netto) : true)
        && (user.value.Dlina ? $scope.isNumber(user.value.Dlina) : true)
                && (user.value.Shirina ? $scope.isNumber(user.value.Shirina) : true)
           && (user.value.Vysota ? $scope.isNumber(user.value.Vysota) : true)
        && user.value.Data_priyoma;
    angular.forEach(user.value.Soderzhimoe, function (obj) {
        flag = flag && obj.Naimenovanie_sostavnoj_edinicy && obj.Kolichestvo_sostavnyh_edinic &&
             $scope.isNumber(obj.Kolichestvo_sostavnyh_edinic) && obj.Kolichestvo_sostavnyh_edinic != 0 && $scope.isInteger(obj.Kolichestvo_sostavnyh_edinic); 
    });
    return flag;
    }
    $scope.acceptEdit = function (emp) {
        if ($scope.checkevent(emp)) {
        emp.showEdit = emp.showEdit ? false : true;
        var idx = $scope.buferList.indexOf(emp.key);
        $scope.buferList.splice(idx);

        $http({
            url: '/Work/ChangeEventDocument',
            method: "POST",
            data: emp
        }).
           then(function (response) {
               if (response.data != "") {
                   if (response.data._id != "000000000000000000000000") {

                       if (emp.id != null) {
                           var idx2 = -1;
                           for (var i = 0, len = $scope.userList.length; i < len; i++) {
                               if ($scope.userList[i].id === emp.id) {
                                   idx2 = i;
                                   break;
                               }
                           }
                           $scope.userList[idx2].value = response.data;

                           if (response.data.Data_priyoma != null)
                               $scope.userList[idx2].value.Data_priyoma = new Date(parseInt(response.data.Data_priyoma.substr(6)));
                           if (response.data.Data_vydachi != null)
                               $scope.userList[idx2].value.Data_vydachi = new Date(parseInt(response.data.Data_vydachi.substr(6)));
                           $scope.getList();
                       }
                       else {
                           $scope.userList[0]["id"] = response.data._id;
                           $scope.userList[0]["key"] = response.data._id;
                           $scope.userList[0].value = response.data;
                           if (response.data.Data_priyoma != null)
                               $scope.userList[0].value.Data_priyoma = new Date(parseInt(response.data.Data_priyoma.substr(6)));
                           if (response.data.Data_vydachi != null)
                               $scope.userList[0].value.Data_vydachi = new Date(parseInt(response.data.Data_vydachi.substr(6)));

                           $scope.getList();
                       }
                   }
                   else {
                       SweetAlert.swal("Ошибка", "Изделие уже есть в базе данных!", "error");
                       if (emp.id==null)
                           $scope.userList.shift();
                       else {
                           var idx2 = -1;
                           for (var i = 0, len = $scope.userList.length; i < len; i++) {
                               if ($scope.userList[i].id === emp.id) {
                                   idx2 = i;
                                   break;
                               }
                           }
                           $scope.userList[idx2].value = response.data;
                           $scope.userList[idx2].value._id = emp.id;
                       }
                   }
               }
               else {
                   // ничего не изменилось
                   SweetAlert.swal("Отмена", "Запись не была обновлена, так как текущая версия записи совпадает с предыдущей!", "warning");
                   if (emp.id==null)
                       $scope.userList.shift();

               }

           });
    }
    };
    $scope.childInfo = function (emp) {
        emp.showSub = emp.showSub ? false : true;

    }
    $scope.childInfoHis = function (emp) {
        emp.showSub = emp.showSub ? false : true;

    }
    $scope.showarchive = function () {
        $scope.archive = $scope.archive ? false : true;
        $scope.getList();
    }
    $scope.IschildInfoHis = function (emp) {
        return emp.showSub; 

    }


}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'SweetAlert', 'moment'];
