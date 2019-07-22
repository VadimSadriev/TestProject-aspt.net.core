angular.module('login-app', []).controller('login-ctrl', function ($scope, $http) {

    $scope.isLoading = false;
    $scope.user = {};

    $scope.login = function () {
        $scope.isLoading = true;

        $http.post('/account/login?returnUrl=xd', $scope.user)
            .then(function (res) {
                if (res.data.success) {
                    window.location.replace(res.data.response.returnUrl);
                }
                else {
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                }
                $scope.isLoading = false;
            }).catch(function (res) {
                $scope.isLoading = false;
                toast.error({
                    bodyText: res.data.errorMessage
                });
            });
    };

    function onEnterPress(event) {
        if (event.keyCode === 13) {
            $scope.login();
        }
    }

    window.addEventListener('keypress', onEnterPress);

    $scope.$on("$destroy", function (event) {
        window.removeEventListener('keypress', onEnterPress);
    });
   
});