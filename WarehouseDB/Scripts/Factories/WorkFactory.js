var WorkFactory = function ($http, $q) {
    return {
        getDocuments: function (page, limit) {

            var deferred = $q.defer();

            $http({
                method: 'GET',
                url: '/Work/GetDocument/',
                params: { page: page, limit:limit }
            }).success(function (response) {
                if (typeof response.data == 'object') {
                    deferred.resolve(response.data);
                } else {
                    deferred.reject(response.data);
                }
            }).error(function (response) {
                deferred.reject(response.data);
            });

            return deferred.promise;
        }
    }
}

WorkFactory.$inject = ['$http', '$q'];