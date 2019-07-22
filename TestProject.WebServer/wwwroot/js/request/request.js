angular.module('requestApp', ['pageList', 'ui.bootstrap', '720kb.datepicker', 'moment-picker', 'ngFileUpload']).controller('requestCtrl', function ($scope, $http, uibDateParser, Upload) {
    $scope.isLoading = false;
    $scope.requests = [];
    $scope.currentUser = {};
    $scope.requestTypes = [];
    $scope.onLoadCallbacks = [];
    $scope.requestStatuses = [];
    $scope.grid = {
        id: '',
        requestType: '',
        dateCreated: '',
        creator: '',
        dateCreated: ''
    }

    $scope.filterOptions = {
        currentPage: 1,
        pageSize: 20,
        totalPages: null,
        options: null
    }

    $scope.getFilterOptions = function () {
        const filterOptions = {
            filters: [],
            sortings: []
        };

        for (let key in $scope.grid) {
            if ($scope.grid[key]) {
                const currentFilter = {
                    field: key,
                    value: []
                };
                currentFilter.value.push($scope.grid[key]);
                filterOptions.filters.push(currentFilter);
            }
        }

        if ($scope.filterMeta) {
            for (let key in $scope.filterMeta) {
                const value = $scope.filterMeta[key];
                const currentFilter = {
                    field: key,
                    value: []
                };
                if (value.type === 'filterById') {
                    for (key in value.values) {
                        for (let i = 0; i < value.values[key].length; i++) {
                            currentFilter.value.push(value.values[key][i]);
                        }
                    }
                    filterOptions.filters.push(currentFilter);
                }
            }
        }

        return filterOptions;
    };

    $scope.getFilters = function () {
        $scope.filterOptions.options = $scope.getFilterOptions();
        $scope.filterOptions.currentPage = 1;
    };

    $scope.onPropertyChanged = function () {
        $scope.getFilters();
        $scope.getRequests();
    };

    $scope.onLoadCallbacks.push($scope.getFilters);

    $scope.getCurrentUser = function () {

        $http.get('/account/GetCurrentUser')
            .then(res => {
                if (res.data.success) {
                    $scope.currentUser = res.data.response;
                }
            }, res => {
                if (res.data.errorMessage) {
                    toast.error({ bodyText: res.data.errorMessage });
                }
            });

    }

    $scope.onLoadCallbacks.push($scope.getCurrentUser);

    $scope.getSecondaryData = function () {
        $scope.isLoading = true;

        $http.get('/request/GetSecondaryData')
            .then(res => {
                if (res.data.success) {
                    $scope.requestTypes = res.data.response.requestTypes;
                    $scope.requestStatuses = res.data.response.requestStatuses;

                    if (res.data.message) {
                        toast.succes({ bodyText: res.data.message });
                    }
                }
                else {
                    toast.error({ bodyText: res.data.errorMessage });
                }

                $scope.isLoading = false;
            }).catch(res => {
                $scope.isLoading = false;
                if (res.data.errorMessage) {
                    toast.error({ bodyText: res.data.errorMessage });
                }
            });
    }

    $scope.onLoadCallbacks.push($scope.getSecondaryData);

    $scope.getRequests = function () {
        $scope.isLoading = true;
        console.log($scope.filterOptions);
        $http.post('/request/getrequests', $scope.filterOptions)
            .then(function (res) {

                if (res.data.success) {
                    $scope.requests = res.data.response.requests;
                    Object.assign($scope.filterOptions, {
                        totalPages: res.data.response.totalPages,
                        hasPreviousPage: res.data.response.hasPreviousPage,
                        hasNextPage: res.data.response.hasNextPage
                    });
                }
                //else {
                //    toast.error({
                //        bodyText: res.data.errorMessage
                //    });
                //}

                $scope.isLoading = false;
            }).catch(function (res) {
                $scope.isLoading = false;
                toast.error({
                    bodyText: res.data.errorMessage
                });
            });
        console.log('requests done');
    }

    $scope.onLoadCallbacks.push($scope.getRequests);

    $scope.selectRequest = function (request) {

        if ($scope.request !== user) {

            if ($scope.request) {
                $scope.request.isSelected = false;
            }

            $scope.request = request;
            $scope.request.isSelected = true;

            $scope.getRequest(request);
        }
    }

    $scope.getRequest = function (request) {
        $scope.isLoading = true;
        $http.post(`/request/GetRequest?id=${request.id}`)
            .then(res => {

                if (res.data.success) {

                    if ($scope.selectedRequest && $scope.selectedRequest !== request) {
                        $scope.selectedRequest.isSelected = false;
                    }

                    $scope.request = res.data.response;
                    $scope.selectedRequest = request;
                    $scope.selectedRequest.isSelected = true;

                    // $scope.converDateTimes($scope.request);

                }
                else {
                    toast.error({ bodyText: res.data.errorMessage });
                }
                $scope.isLoading = false;
            }).catch(res => {
                $scope.isLoading = false;
                toast.error({ bodyText: res.data.errorMessage });
            });
    }

    $scope.createRequestTemplate = function () {
        if ($scope.selectedRequest) {
            $scope.selectedRequest.isSelected = false;
        }
        $scope.request = new Request("Новая заявка");
    }

    $scope.addRequest = function () {
        if (!$scope.request.id) {
            $scope.isLoading = true;

            $http.post('/request/AddRequest', $scope.request)
                .then(res => {
                    if (res.data.success) {
                        Object.assign($scope.request, res.data.response);

                        if (res.data.message) {
                            toast.succes({ bodyText: res.data.message });
                        }
                        $scope.getRequests();
                    }
                    else {
                        toast.error({ bodyText: res.data.errorMessage });
                    }
                    $scope.isLoading = false;
                }, res => {
                    $scope.isLoading = false;

                    if (res.data.errorMessage) {
                        toast.error({ bodyText: res.data.errorMessage });
                    }
                });
        }
    }

    $scope.updateRequest = function () {
        $scope.isLoading = true;

        $http.post('/request/UpdateRequest', $scope.request)
            .then(res => {

                if (res.data.success) {
                    try {
                        Object.assign($scope.request, res.data.response, { isChanged: false });
                        Object.assign($scope.selectedRequest, {
                            displayStatus: $scope.request.displayStatus,
                            type: $scope.request.requestType.name
                        });
                    }
                    catch (err) {
                        $scope.getRequests();
                    }

                    if (res.data.message) {
                        toast.succes({ bodyText: res.data.message });
                    }
                }
                else {
                    toast.error({ bodyText: res.data.errorMessage });
                }
                $scope.isLoading = false;
            }, res => {
                $scope.isLoading = false;
                if (res.data.errorMessage) {
                    toast.error({ bodyText: res.data.errorMessage });
                }
            });
    }

    $scope.getRequestFieldsData = function () {

        if ($scope.request.id) {
            $scope.isLoading = true;

            $http.post(`/request/GetFieldsData?typeId=${$scope.request.requestType.id}&reqId=${$scope.request.id}`)
                .then(res => {

                    if (res.data.success) {
                        $scope.request.requestType = res.data.response;
                    }
                    else {
                        if (res.data.errorMessage) {
                            toast.error({ bodyText: res.data.errorMessage });
                        }
                    }
                    $scope.isLoading = false;
                }).catch(res => {
                    $scope.isLoading = false;
                    if (res.data.errorMessage) {
                        toast.error({ bodyText: res.data.errorMessage });
                    }
                });
        }
    }

    $scope.uploadFile = function (typeField) {
        typeField.isFileLoading = true;

        Upload.upload({
            url: '/File/Upload',
            file: typeField.file
        }).then(res => {
            if (res.data.success) {
                typeField.requestValue.fileValue = res.data.response.fileGuid;
                typeField.requestValue.fileName = res.data.response.name;
            }
            typeField.isFileLoading = false;
            typeField.fileLoadProgress = 0;
        }, res => {
            typeField.isFileLoading = false;
            if (res.data.errorMessage) {
                toast.error({ bodyText: res.data.errorMessage });
            }
        }, e => {
            typeField.fileLoadProgress = parseInt(100.0 * e.loaded / e.total);
        });
    }

    $scope.closeRequest = function () {
        $scope.request = null;
        if ($scope.selectedRequest) {
            $scope.selectedRequest.isSelected = false;
            $scope.selectedRequest = null;
        }
    }

    function Request(name) {
        this.name = name;
        this.requestTypeFields = [];
    }

    $scope.setFilterRequestType = function (requestType) {

        requestType.isSelected = !requestType.isSelected;

        let idIndex = $scope.filterMeta.requestType.values.requestTypeIds.indexOf(requestType.id);

        if (requestType.isSelected) {
            if (idIndex === -1) {
                $scope.filterMeta.requestType.values.requestTypeIds.push(requestType.id);
            }
        }
        else {
            if (idIndex > -1) {
                $scope.filterMeta.requestType.values.requestTypeIds.splice(idIndex, 1);
            }
        }
        $scope.onPropertyChanged();
    }

    $scope.filterMeta = {
        requestType: {
            type: 'filterById',
            values: {
                requestTypeIds: []
            }
        }
    };


    $scope.onLoadCallbacks.reduce((prev, cur) => {
        return prev.then(cur);
    }, Promise.resolve("privet"));
});