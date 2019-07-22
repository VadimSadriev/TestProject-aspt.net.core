(function () {
    angular.module('pageList', []);
})();

angular.module('pageList').directive('pagelist',
    function ($http) {
        return {
            restrict: 'E',
            templateUrl: '/Home/PageList',
            scope: {
                filterOptions: '=filterOptions',
                getItemsCallback: '=getItemsCallback'
            },
            controller: function ($scope) {

                $scope.goToPage = function (pageNumber) {
                    $scope.filterOptions.currentPage = pageNumber;
                    $scope.getItemsCallback();
                }

                $scope.lastPages = function () {
                    const lastPagesArray = [];

                    for (let i = Math.max(4, $scope.filterOptions.totalPages - 2); i <= $scope.filterOptions.totalPages; i++) {
                        lastPagesArray.push(i);
                    }
                    return lastPagesArray;
                }
            }
        };
    });