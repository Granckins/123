var WorkController = function ($scope, $http, DTOptionsBuilder, DTColumnBuilder, $compile, SweetAlert, moment) {
    $scope.message = "fdf";
    $scope.vm = {};
    $scope.vm.dtInstance = {};

    $scope.archive = false;

    $scope.currentPage = 1;
    $scope.numPerPage = 10;
    $scope.maxSize = 5;


    $scope.archive_str = "";
    $scope.Nomer_upakovki_str = "";
    $scope.Naimenovanie_izdeliya_str = "";
    $scope.Zavodskoj_nomer_str = "";
    $scope.Oboznachenie_str = "";
    $scope.Soderzhimoe_str = "";
    $scope.Sistema_str = "";
    $scope.Prinadlezhnost_str = "";
    $scope.Mestonahozhdenie_na_sklade_str = "";

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
    $scope.pageSizeSelected = 5; // Maximum number of items per page.  
    function formatJSONDate(jsonDate) {
        var newDate = dateFormat(jsonDate, "mm/dd/yyyy");
        return newDate;
    }
    $scope.selectedOption = '';
    $scope.searchText = '';
     $scope.searchfiltername = [];
    $scope.searchfiltervalue = [];
    $scope.buferList = [];
    $scope.userList = [];
    $scope.sortfiltername = "Номер упаковки";
    $scope.sortfiltervalue = "1";
    $scope.Data_vydachi = false;
    $scope.Data_priyoma = false;
    $scope.Data_priyoma_str1 = null;
    $scope.Data_vydachi_str1 = null;
    $scope.Data_priyoma_str2 = null;
    $scope.Data_vydachi_str2 = null;
    $scope.launchTypeOptions = [
      { name: 'Номер упаковки', value: 'Номер упаковки' },
      { name: 'Наименование', value: 'Наименование изделия' },
      { name: 'Заводской номер', value: 'Заводской номер' },
      { name: 'Ответственный', value: 'Ответственный' },
      { name: 'Принадлежность', value: 'Принадлежность' },
          { name: 'Система', value: 'Система' },
                      { name: 'Содержимое', value: 'Содержимое' },
                          { name: 'Местонахождение на складе', value: 'Местонахождение на складе' },
                                         { name: 'Примечание', value: 'Примечание' }, { name: 'Дата приёма', value: 'Дата приёма' },
{ name: 'Дата выдачи', value: 'Дата выдачи' }
    ];

    $scope.SortOptions = [
           { name: 'Номер упаковки', value: 1 },
        { name: 'Наименование изделия', value: 0 },
      { name: 'Заводской номер', value: 0 },
      { name: 'Количество', value: 0 },
      { name: 'Заводской номер', value: 0 },
      { name: 'Местонахождение на складе', value: 0 },
      { name: 'Система', value: 0 },
      { name: 'Ответственный', value: 0 },
      { name: 'Принадлежность', value: 0},
                                         { name: 'Дата приёма', value: 0 },
                                            { name: 'Дата выдачи', value: 0 }
    ];
    $scope.isSearch = false;
    $scope.search = function (str) {
        switch (str) {
            case 'Наименование изделия':
                break
            default:
                break
        }
    };

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
            url: "/Work/FilterSortDocument",
            method: "POST",
            data: obj
        }).
           then(function (response) {
               $scope.userList = response.data.rows;
               $scope.totalCount = response.data.total_rows;

               angular.forEach($scope.userList, function (obj) {
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

               })
               angular.forEach($scope.userList, function (obj) {

                   $http({
                       url: "/Work/IsEventHistory?id=" + obj.id,
                       method: "GET",
                       params: {
                           page: $scope.pageIndex,
                           limit: $scope.pageSizeSelected
                       }
                   }).
                    then(function (response) {
                        var gdfgf = response.data > 0 ? true : false;
                        obj["IsHistory"] = gdfgf;

                    });

               })
           });
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

    $scope.updateCalcs = function () {
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
            url: "/Work/FilterSortDocument",
            method: "POST",
            data: obj
        }).
           then(function (response) {
               $scope.userList = response.data.rows;
               $scope.totalCount = response.data.total_rows;

               angular.forEach($scope.userList, function (obj) {
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
               })
               angular.forEach($scope.userList, function (obj) {

                   $http({
                       url: "/Work/IsEventHistory?id=" + obj.id,
                       method: "GET",
                       params: {
                           page: $scope.pageIndex,
                           limit: $scope.pageSizeSelected
                       }
                   }).
                    then(function (response) {
                        var gdfgf = response.data > 0 ? true : false;
                        obj["IsHistory"] = gdfgf;

                    });

               })
           });
    };
    $scope.$watch('pageIndex', function (newVal, oldVal) {
        $scope.pageIndex = newVal;
        $scope.getList();

    });
    $scope.pageSizes = ('5 10 25 50').split(' ').map(function (state) { return { abbrev: state }; }); 
    $scope.filters = ("Дата приёма;Дата выдачи").split(';').map(function (state) { return { abbrev: state }; });
    $scope.searchfilters = "";
    $scope.searchsorts = "";
    $scope.getList();
 
    $scope.changePageSize = function () {
        $scope.pageIndex = 1;
        $scope.getList();
    };
  
    $scope.GetLengthFilter = function () {
        return $scope.searchfiltername.length;
    };

 
    $scope.launchAPIQueryParams = {
        types: [],
    };


  
    $scope.deletechipsToList = function (chip) {
        var length = $scope.launchTypeOptions.length;
        for (var i = 0; i < length; i++) {
            if ($scope.launchTypeOptions[i].$$hashKey === chip.$$hashKey) {
                
                $scope.launchTypeOptions[i].name = $scope.launchTypeOptions[i].value;
            }
        }
    };

  
    $scope.SortReset= function (name) {
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name != name) {
                {
                    
                    $scope.SortOptions[i].value = 0;
                         
                     
                }
            }
        }
    };
    $scope.SortChange = function (name) {
       var value=0;
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name == name) {
                { 
                    if (name == 'Дата приёма' || name == 'Дата выдачи') {
                        if ($scope.SortOptions[i].value == 0)
                        { $scope.SortOptions[i].value = 1; 
                        value= $scope.SortOptions[i].value;
                        break; }
                        if ($scope.SortOptions[i].value == 1)
                        { $scope.SortOptions[i].value = 0;
                        value= $scope.SortOptions[i].value;    
                        break; }
                    } else {

                        if ($scope.SortOptions[i].value == 0)
                        { $scope.SortOptions[i].value = 1;
                        value= $scope.SortOptions[i].value;    
                        break; }
                        if ($scope.SortOptions[i].value == 1)
                        { $scope.SortOptions[i].value = 2; 
                        value= $scope.SortOptions[i].value;    
                        break; }
                        if ($scope.SortOptions[i].value == 2)
                        { $scope.SortOptions[i].value = 0; 
                        value= $scope.SortOptions[i].value;    
                        break; }
                    }
                }
            }
        }
        $scope.SortReset(name);


        var post = new Object();
        $scope.pageIndex = 1;
        post["page"] = $scope.pageIndex;
        post["limit"] = $scope.pageSizeSelected;
        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
        post["archive_str"] = $scope.archive_str;
        post["filtername"] = searchfilternameString;
        post["filtervalue"] = searchfiltervalueString;
        $scope.sortfiltername = "" + name;
        $scope.sortfiltervalue = "" + value;
        post["sortname"] = $scope.sortfiltername;
        post["sortvalue"] = $scope.sortfiltervalue;

        post["datepr"] = $scope.getdatepr();
        post["datevd"] = $scope.getdatevd();
        $http({
            url: '/Work/FilterSortDocument',
            method: "POST",
            data: post
        }).
            then(function (response) {
                $scope.userList = response.data.rows;
                $scope.totalCount = response.data.total_rows;

                angular.forEach($scope.userList, function (obj) {
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
                })
                angular.forEach($scope.userList, function (obj) {

                    $http({
                        url: "/Work/IsEventHistory?id=" + obj.id,
                        method: "GET",
                        params: {
                            page: $scope.pageIndex,
                            limit: $scope.pageSizeSelected
                        }
                    }).
                     then(function (response) {
                         var gdfgf = response.data > 0 ? true : false;
                         obj["IsHistory"] = gdfgf;

                     });

                })

            });


    };

    $scope.searchTextChange = function (searchText) {
        $scope.searchText = searchText;
    };
    $scope.GetStatusSort = function (name) {
        for (var i = 0; i < $scope.SortOptions.length ; i++) {
            if ($scope.SortOptions[i].name == name) {
                return $scope.SortOptions[i].value;
            }
        }
    };

    $scope.chipdelete = function (chip) {
        for (var i = 0; i < $scope.launchTypeOptions.length ; i++) {
            if ($scope.launchTypeOptions[i].$$hashKey === chip.$$hashKey) {
                $scope.launchTypeOptions[i].name = $scope.launchTypeOptions[i].value;
                break;
            }
        }
        for (var i = 0; i < $scope.searchfiltername.length ; i++) {
            if ($scope.searchfiltername[i] === chip.value) {
                $scope.searchfiltername.splice(i, 1);
                $scope.searchfiltervalue.splice(i, 1);
                break;
            }
        }
        if (chip.value == 'Дата приёма')
        {
        $scope.Data_priyoma_str1 = null;
        $scope.Data_priyoma_str2 = null;
        $scope.Data_priyoma = false;
        }
           
        if (chip.value == 'Дата выдачи')
        {
            $scope.Data_vydachi_str1 = null;
            $scope.Data_vydachi_str2 = null;
            $scope.Data_vydachi = false;
        }
        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
        var post = new Object();
        post["page"] = $scope.pageIndex;
        post["limit"] = $scope.pageSizeSelected;

        post["archive_str"] = $scope.archive_str;
        post["filtername"] = searchfilternameString;
        post["filtervalue"] = searchfiltervalueString;
        post["sortname"] = $scope.sortfiltername;
        post["sortvalue"] = $scope.sortfiltervalue;
        post["datepr"] = $scope.getdatepr();
        post["datevd"] = $scope.getdatevd();
        $http({
            url: '/Work/FilterSortDocument',
            method: "POST",
            data: post
        }).
            then(function (response) {
                $scope.userList = response.data.rows;
                $scope.totalCount = response.data.total_rows;

                angular.forEach($scope.userList, function (obj) {
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
                })
                angular.forEach($scope.userList, function (obj) {

                    $http({
                        url: "/Work/IsEventHistory?id=" + obj.id,
                        method: "GET",
                        params: {
                            page: $scope.pageIndex,
                            limit: $scope.pageSizeSelected
                        }
                    }).
                     then(function (response) {
                         var gdfgf = response.data > 0 ? true : false;
                         obj["IsHistory"] = gdfgf;

                     });

                })

            });
    };
    $scope.newVeg = function (chip) {
        var obj = new Object();


        if (chip.name == null) {
            obj["name"] = chip;
            obj["value"] = 'Номер упаковки';
        }
        else
            obj = chip;
        var flagSod = false;
        if (obj.value == 'Содержимое')
            flagSod = true;
        if (obj.value == 'Дата приёма')
            $scope.Data_priyoma = true;
        if (obj.value == 'Дата выдачи')
            $scope.Data_vydachi = true;
        obj.name = $scope.searchText + " (" + obj.value + ")";

        $scope.searchfiltername.push(obj.value);
        $scope.searchfiltervalue.push($scope.searchText);
        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
        var post = new Object();
        $scope.pageIndex = 1;
        post["page"] = $scope.pageIndex;
        post["limit"] = $scope.pageSizeSelected;

        post["archive_str"] = $scope.archive_str;
        post["filtername"] = searchfilternameString;
        post["filtervalue"] = searchfiltervalueString;
        post["sortname"] = $scope.sortfiltername;
        post["sortvalue"] = $scope.sortfiltervalue;
        post["datepr"] = $scope.getdatepr();
        post["datevd"] = $scope.getdatevd();
        $http({
            url: '/Work/FilterSortDocument',
            method: "POST",
            data: post
        }).
            then(function (response) {
                $scope.userList = response.data.rows;
                $scope.totalCount = response.data.total_rows;

                angular.forEach($scope.userList, function (obj) {
                    obj["showEdit"] = true; 
                    obj["History"] = [];
                    obj["IsHistory"] = false;
                    obj["showHistory"] = false;
                    if (obj.value.Data_priyoma != null)
                        obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                    if (obj.value.Data_vydachi != null)
                        obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));
                    if (obj.value.Data_ismenen != null)
                        obj.value.Data_ismenen = new Date(parseInt(obj.value.Data_ismenen.substr(6)));
                })
                angular.forEach($scope.userList, function (obj) {

                    $http({
                        url: "/Work/IsEventHistory?id=" + obj.id,
                        method: "GET",
                        params: {
                            page: $scope.pageIndex,
                            limit: $scope.pageSizeSelected
                        }
                    }).
                     then(function (response) {
                         var gdfgf = response.data > 0 ? true : false;
                         obj["IsHistory"] = gdfgf;

                     });

                })

            });




    };
    //This method is calling from pagination number  
    $scope.pageChanged = function () {
        $scope.getEmployeeList();
    };
    $scope.addRow = function () {

        obj = new Object();
        obj["showEdit"] = false;
        obj["IsHistory"] = false;
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

        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");


        obj["filtername"] = searchfilternameString;
        obj["filtervalue"] = searchfiltervalueString;
        obj["sortname"] = $scope.sortfiltername;
        obj["sortvalue"] = $scope.sortfiltervalue;
        obj["datepr"] = $scope.getdatepr();
        obj["datevd"] = $scope.getdatevd();

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

        if (user.id != null) {
            $http({
                url: '/Work/DeleteEventDocument',
                method: "POST",
                data: obj
            }).
                   then(function (response) {
              
                       $scope.userList = response.data.rows;
                       $scope.totalCount = response.data.total_rows;

                       angular.forEach($scope.userList, function (obj) {
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
                       })
                       angular.forEach($scope.userList, function (obj) {

                           $http({
                               url: "/Work/IsEventHistory?id=" + obj.id,
                               method: "GET",
                               params: {
                                   page: $scope.pageIndex,
                                   limit: $scope.pageSizeSelected
                               }
                           }).
                            then(function (response) {
                                var gdfgf = response.data > 0 ? true : false;
                                obj["IsHistory"] = gdfgf;

                            });

                       })
                   });
        } 
            SweetAlert.swal("Запись удалена!");
         
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
    $scope.iseventhistory = function (obj) {



    };

    $scope.gethistory = function (emp) {
        emp.showHistory = emp.showHistory ? false : true;
        if (emp.showHistory == true) {
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
                     if (obj.value.Data_ismenen != null)
                         obj.value.Data_ismenen = new Date(parseInt(obj.value.Data_ismenen.substr(6)));
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
        if (user.value != null && user.value.Soderzhimoe != null && user.value.Soderzhimoe.length > 0) {
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
        if (emp.key != null) {
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
            $scope.userList.splice(0, 1);
        }
    };
    $scope.checkevent = function (user) {
        var flag = user.value.Nomer_upakovki && $scope.isNumber(user.value.Nomer_upakovki) && $scope.isInteger(user.value.Nomer_upakovki)
            && user.value.Naimenovanie_izdeliya && user.value.Zavodskoj_nomer && user.value.Kolichestvo && $scope.isNumber(user.value.Kolichestvo)
               && user.value.Kolichestvo != 0 && user.value.Sistema && user.value.Prinadlezhnost &&
              (user.value.Stoimost ? $scope.isNumber(user.value.Stoimost) : true) && user.value.Otvetstvennyj && user.value.Mestonahozhdenie_na_sklade
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
    $scope.archivete = function (emp) {
         
          
            var idx = $scope.buferList.indexOf(emp.key);
            $scope.buferList.splice(idx);
            var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
            var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
            var post = new Object();
            emp["page"] = $scope.pageIndex;
            emp["limit"] = $scope.pageSizeSelected;
            emp["showEdit"] = false;
            emp["archive_str"] = $scope.archive_str;
            emp["filtername"] = searchfilternameString;
            emp["filtervalue"] = searchfiltervalueString;
            emp["sortname"] = $scope.sortfiltername;
            emp["sortvalue"] = $scope.sortfiltervalue;
            emp["datepr"] = $scope.getdatepr();
            emp["datevd"] = $scope.getdatevd();
            emp.value.archive = true;
            $http({
                url: '/Work/ChangeEventDocument',
                method: "POST",
                data: emp
            }).
               then(function (response) {
                   $scope.getList();

               });
  
    };
    $scope.unarchivete = function (emp) {


        var idx = $scope.buferList.indexOf(emp.key);
        $scope.buferList.splice(idx);
        var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
        var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
        var post = new Object();
        emp["page"] = $scope.pageIndex;
        emp["limit"] = $scope.pageSizeSelected;
        emp["showEdit"] = false;
        emp["archive_str"] = $scope.archive_str;
        emp["filtername"] = searchfilternameString;
        emp["filtervalue"] = searchfiltervalueString;
        emp["sortname"] = $scope.sortfiltername;
        emp["sortvalue"] = $scope.sortfiltervalue;
        emp["datepr"] = $scope.getdatepr();
        emp["datevd"] = $scope.getdatevd();
        emp.value.archive = false;
        emp.value.Data_vydachi = null;
        $http({
            url: '/Work/ChangeEventDocument',
            method: "POST",
            data: emp
        }).
           then(function (response) {
               $scope.getList();

           });

    };
    $scope.activate = function (emp, user) {
        if ($scope.checkevent(emp)) {
            user["showHistory"] = false;
            var idx = $scope.buferList.indexOf(emp.key);
            $scope.buferList.splice(idx);
            var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
            var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
            var post = new Object();
            emp["page"] = $scope.pageIndex;
            emp["limit"] = $scope.pageSizeSelected;
            
            emp["archive_str"] = $scope.archive_str;
            emp["filtername"] = searchfilternameString;
            emp["filtervalue"] = searchfiltervalueString;
            emp["sortname"] = $scope.sortfiltername;
            emp["sortvalue"] = $scope.sortfiltervalue;
            emp["datepr"] = $scope.getdatepr();
            emp["datevd"] = $scope.getdatevd();
            $http({
                url: '/Work/ChangeEventDocument',
                method: "POST",
                data: emp
            }).
               then(function (response) {
                   //  if (response.data != "") {
                   $scope.userList = response.data.rows;
                   $scope.totalCount = response.data.total_rows;

                   angular.forEach($scope.userList, function (obj) {
                       obj["showEdit"] = true;
                       obj["showSub"] = false;
                       obj["History"] = [];
                    
                       obj["showHistory"] = false;
                       if (obj.value.Data_priyoma != null)
                           obj.value.Data_priyoma = new Date(parseInt(obj.value.Data_priyoma.substr(6)));
                       if (obj.value.Data_vydachi != null)
                           obj.value.Data_vydachi = new Date(parseInt(obj.value.Data_vydachi.substr(6)));
                       if (obj.value.Data_ismenen != null)
                           obj.value.Data_ismenen = new Date(parseInt(obj.value.Data_ismenen.substr(6)));
                   })
                   angular.forEach($scope.userList, function (obj) {

                       $http({
                           url: "/Work/IsEventHistory?id=" + obj.id,
                           method: "GET",
                           params: {
                               page: $scope.pageIndex,
                               limit: $scope.pageSizeSelected
                           }
                       }).
                        then(function (response) {
                            var gdfgf = response.data > 0 ? true : false;
                            obj["IsHistory"] = gdfgf;

                        });

                   }); 
               });
        }
    };
    $scope.acceptEdit = function (emp) {
        if ($scope.checkevent(emp)) {
            emp.showEdit = emp.showEdit ? false : true;
            var idx = $scope.buferList.indexOf(emp.key);
            $scope.buferList.splice(idx);
            var searchfilternameString = Array.prototype.join.call($scope.searchfiltername, ";");
            var searchfiltervalueString = Array.prototype.join.call($scope.searchfiltervalue, ";");
            var post = new Object();
            emp["page"] = $scope.pageIndex;
            emp["limit"] = $scope.pageSizeSelected;
            emp["showEdit"] = false;
            emp["archive_str"] = $scope.archive_str;
            emp["filtername"] = searchfilternameString;
            emp["filtervalue"] = searchfiltervalueString;
            emp["sortname"] = $scope.sortfiltername;
            emp["sortvalue"] = $scope.sortfiltervalue;
            emp["datepr"] = $scope.getdatepr();
            emp["datevd"] = $scope.getdatevd();
            $http({
                url: '/Work/ChangeEventDocument',
                method: "POST",
                data: emp
            }).
               then(function (response) {
                   //  if (response.data != "") {
                   $scope.userList = response.data.rows;
                   $scope.totalCount = response.data.total_rows;

                   angular.forEach($scope.userList, function (obj) {
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
                   })
                   angular.forEach($scope.userList, function (obj) {

                       $http({
                           url: "/Work/IsEventHistory?id=" + obj.id,
                           method: "GET",
                           params: {
                               page: $scope.pageIndex,
                               limit: $scope.pageSizeSelected
                           }
                       }).
                        then(function (response) {
                            var gdfgf = response.data > 0 ? true : false;
                            obj["IsHistory"] = gdfgf;

                        });

                   });
                   //   }
                   //else {
                   //    // ничего не изменилось
                   //    SweetAlert.swal("Отмена", "Запись не была обновлена, так как текущая версия записи совпадает с предыдущей!", "warning");
                   //    if (emp.id == null)
                   //        $scope.userList.shift();

                   //}

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
        $scope.archive_str = $scope.archive;
        $scope.getList();
    }
    $scope.IschildInfoHis = function (emp) {
        return emp.showSub;

    }


}

WorkController.$inject = ['$scope', '$http', 'DTOptionsBuilder', 'DTColumnBuilder', '$compile', 'SweetAlert', 'moment'];
