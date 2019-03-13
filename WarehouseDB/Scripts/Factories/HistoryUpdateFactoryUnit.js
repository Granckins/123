var HistoryUpdateFactoryUnit = function ($http, $q) {
    return function (id) {
        var deferred = $q.defer();

        var deferredObject = $q.defer();

        $http.post(
            '/Unit/IsEventHistory', {
                id: id
            }
        ).
            then(function (res) {
                if (res.data == "True") {
                    deferredObject.resolve({ success: true });
                } else {
                    deferredObject.resolve({ success: false });
                }
            });

        return deferredObject.promise;
    }
}

HistoryUpdateFactoryUnit.$inject = ['$http', '$q'];