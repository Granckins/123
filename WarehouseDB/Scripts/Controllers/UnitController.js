var UnitController = function ($scope, $http,$document,  DTOptionsBuilder, DTColumnBuilder, $compile, SweetAlert, moment, HistoryUpdateFactoryUnit, $q, $timeout, FileUploader, FileSaver, Blob) {
    $scope.message = "fdf";
    $scope.SortOptions = [
          { name: 'Номер упаковки', value: 1 },
       { name: 'Наименование изделия', value: 0 },
     { name: 'Заводской номер', value: 0 },
          { name: 'Обозначение', value: 0 },
     { name: 'Количество', value: 0 },
     { name: 'Заводской номер', value: 0 },
     { name: 'Местонахождение на складе', value: 0 },
     { name: 'Система', value: 0 },
     { name: 'Ответственный', value: 0 },
     { name: 'Принадлежность', value: 0 },
                                        { name: 'Дата приёма', value: 0 },
                                           { name: 'Дата выдачи', value: 0 }
    ];
    $scope.launchTypeOptions = [
     { name: 'Номер упаковки', value: 'Номер упаковки' },
     { name: 'Наименование', value: 'Наименование изделия' },
     { name: 'Заводской номер', value: 'Заводской номер' },
      { name: 'Обозначение', value: 'Обозначение' },
     { name: 'Ответственный', value: 'Ответственный' },
     { name: 'Принадлежность', value: 'Принадлежность' },
         { name: 'Система', value: 'Система' },
                     { name: 'Содержимое', value: 'Содержимое' },
                         { name: 'Местонахождение на складе', value: 'Местонахождение на складе' },
                                        { name: 'Примечание', value: 'Примечание' }, { name: 'Дата приёма', value: 'Дата приёма' },
{ name: 'Дата выдачи', value: 'Дата выдачи' }
    ];
    $scope.Math = window.Math;
    $scope.Data_vydachi = false;
    $scope.Data_priyoma = false;
    $scope.Data_priyoma_str1 = null;
    $scope.Data_vydachi_str1 = null;
    $scope.Data_priyoma_str2 = null;
    $scope.Data_vydachi_str2 = null;
    $scope.searchfiltername = [];
    $scope.searchfiltervalue = [];
    $scope.sortfiltername = "Номер упаковки";
    $scope.sortfiltervalue = "1";
    $scope.getUnits= function () {
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
        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
        obj["filtername"] = searchfilternameString;
        obj["filtervalue"] = searchfiltervalueString;
        obj["sortname"] = $scope.sortfiltername;
        obj["sortvalue"] = $scope.sortfiltervalue;
        obj["datepr"] = $scope.getdatepr();
        obj["datevd"] = $scope.getdatevd();
        obj["entity"] = null;
        $http({
            url: "/Unit/GetUnits",
            method: "POST",
            data: obj
        }).
           then(function (response) {
               $scope.userList = [];
               $scope.totalCount = response.data.total_rows;
               var i = 0;
               var lcount = $scope.userList.length;
               angular.forEach(response.data.rows, function (obj) {
                   obj["showEdit"] = true;
                   obj["showSub"] = false;
                   obj["History"] = [];
                   obj["IsHistory"] = false;
                   obj["showHistory"] = false;
                   if (obj.value.Data_priyoma != null)
                       obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                   if (obj.value.Data_vydachi != null)
                       obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));
                   if (obj.value.Data_ismenen != null)
                       obj.value.Data_ismenen = new Date(parseInt(obj.value.Data_ismenen.substr(6)));
                   if (i == 0)
                       $scope.userList = [];
                   if (i < lcount) {
                       $scope.userList[i] = obj;
                   }
                   else {
                       $scope.userList.push(obj);
                   }
                   i++;
               })
               angular.forEach($scope.userList, function (obj) {

                   var result = HistoryUpdateFactoryUnit(obj.id);
                   result.then(function (result) {
                       if (result.success) {

                           obj["IsHistory"] = true;
                       } else {
                           obj["IsHistory"] = false;
                       }

                   });

               })
           
               var items = angular.element(document.querySelectorAll('.circle1'));
               for (var i = 0, l = items.length; i < l; i++) {
                   items[i].style.left = (50 - 35 * Math.cos(-0.5 * Math.PI - 2 * (1 / l) * i * Math.PI)).toFixed(4) + "%";

                   items[i].style.top = (50 + 35 * Math.sin(-0.5 * Math.PI - 2 * (1 / l) * i * Math.PI)).toFixed(4) + "%";
               }

           });


    }
    $scope.do = function ($event) {
        $event.preventDefault();
        var myEl = angular.element(document.querySelector('.circle'));
        myEl.addClass('open');
    };
    $scope.GetStatusSort = function (name) {
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name == name) {
                return $scope.SortOptions[i].value;
            }
        }
    };
    $scope.SortReset = function (name) {
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name != name) {
                {

                    $scope.SortOptions[i].value = 0;


                }
            }
        }
    };
    $scope.SortChange = function (name) {
        var value = 0;
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name == name) {
                {
                    if (name == 'Дата приёма' || name == 'Дата выдачи') {
                        if ($scope.SortOptions[i].value == 0) {
                            $scope.SortOptions[i].value = 1;
                            value = $scope.SortOptions[i].value;
                            break;
                        }
                        if ($scope.SortOptions[i].value == 1) {
                            $scope.SortOptions[i].value = 0;
                            value = $scope.SortOptions[i].value;
                            break;
                        }
                    } else {

                        if ($scope.SortOptions[i].value == 0) {
                            $scope.SortOptions[i].value = 1;
                            value = $scope.SortOptions[i].value;
                            break;
                        }
                        if ($scope.SortOptions[i].value == 1) {
                            $scope.SortOptions[i].value = 2;
                            value = $scope.SortOptions[i].value;
                            break;
                        }
                        if ($scope.SortOptions[i].value == 2) {
                            $scope.SortOptions[i].value = 0;
                            value = $scope.SortOptions[i].value;
                            break;
                        }
                    }
                }
            }
        }
        $scope.SortReset(name);
        var count = 0;
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].value != 0) {
                {
                    count++;
                    break;
                }
            }
        }
        if (count == 0) {
            $scope.SortOptions[0].value = 1;
            value = $scope.SortOptions[0].value;
            name = $scope.SortOptions[0].name;
        }

        var post = new Object();
  
    };
    $scope.isNumber = function (n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }
    $scope.getdatepr = function () {
        if ($scope.Data_priyoma_str1 != null && $scope.Data_priyoma_str2 != null) {
            var Data_priyoma_str1 = new Date($scope.Data_priyoma_str1);
            var curr_date = Data_priyoma_str1.getDate();
            var DD = "" + curr_date;
            if (curr_date < 10)
                DD = "0" + curr_date;
            var curr_month = Data_priyoma_str1.getMonth() + 1;
            var MM = "" + curr_month;
            if (curr_month < 10)
                MM = "0" + curr_month;
            var curr_year = Data_priyoma_str1.getFullYear();
            var sdsd1 = curr_year + "-" + MM + "-" + DD;
            var Data_priyoma_str2 = new Date($scope.Data_priyoma_str2);
            var curr_date1 = Data_priyoma_str2.getDate();
            DD = "" + curr_date1;
            if (curr_date1 < 10)
                DD = "0" + curr_date1;
            var curr_month1 = Data_priyoma_str2.getMonth() + 1;
            MM = "" + curr_month1;
            if (curr_month1 < 10)
                MM = "0" + curr_month1;
            var curr_year1 = Data_priyoma_str2.getFullYear();
            var sdsd2 = curr_year1 + "-" + MM + "-" + DD;
            return sdsd1 + ";" + sdsd2;
        }
        return ";";
    }
    $scope.getdatevd = function () {
        if ($scope.Data_vydachi_str1 != null && $scope.Data_vydachi_str2 != null) {
            var Data_vydachi_str1 = new Date($scope.Data_vydachi_str1);
            var curr_date = Data_vydachi_str1.getDate();
            var DD = "" + curr_date;
            if (curr_date < 10)
                DD = "0" + curr_date;
            var curr_month = Data_vydachi_str1.getMonth() + 1;
            var MM = "" + curr_month;
            if (curr_month < 10)
                MM = "0" + curr_month;
            var curr_year = Data_vydachi_str1.getFullYear();
            var sdsd1 = curr_year + "-" + curr_month + "-" + curr_date;
            var Data_vydachi_str2 = new Date($scope.Data_vydachi_str2);
            var curr_date1 = Data_vydachi_str2.getDate();
            var curr_date1 = Data_priyoma_str2.getDate();
            DD = "" + curr_date1;
            if (curr_date1 < 10)
                DD = "0" + curr_date1;
            var curr_month1 = Data_vydachi_str2.getMonth() + 1;
            MM = "" + curr_month1;
            if (curr_month1 < 10)
                MM = "0" + curr_month1;
            var curr_year1 = Data_vydachi_str2.getFullYear();
            var sdsd2 = curr_year1 + "-" + curr_month1 + "-" + curr_date1;
            return sdsd1 + ";" + sdsd2;
        }
        return ";";
    }
    $scope.login = function () {
        var result = HistoryUpdateFactory($scope.userList);
        result.then(function (result) {
            if (result.success) {

            }
            else {

            }
        });
    }
    $scope.onclick = function () {
    }
}

UnitController.$inject = ['$scope', '$http', '$document',   'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'SweetAlert', 'moment', 'HistoryUpdateFactoryUnit', '$q', '$timeout', 'FileUploader', 'FileSaver', 'Blob'];
