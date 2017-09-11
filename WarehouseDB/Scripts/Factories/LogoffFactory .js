var LogOffFactory = function ($http, $q) {
    return function (loginName, password, rememberMe) {

        var deferredObject = $q.defer();

        $http.post(
            '/Account/LogOff', {
                UserName: loginName,
                Password: password,
                RememberMe: rememberMe
            }
        ).
        then(function (data) {
            if (data.data == "True") {
                deferredObject.resolve({ success: true });
            } else {
                deferredObject.resolve({ success: false });
            }
        }).catch(function () {
            deferredObject.resolve({ success: false });
        });

        return deferredObject.promise;
    }
}

LogOffFactory.$inject = ['$http', '$q'];