angular.module('admin-app', []).controller('admin-ctrl', function ($scope, $http) {

    $scope.isLoading = false;
    $scope.users = [];
    $scope.allCustomRoles = [];
    $scope.customRoles = [];
    $scope.allAppRoles = [];
    $scope.requestTypes = [];
    $scope.selectedUser = null;
    $scope.selectedCustomRole = null;
    $scope.selectedRequestType = null;
    $scope.requestFieldTypes = [];

    $scope.onLoadCallbacks = [];

    $scope.getUsers = function () {
        $scope.selectedUser = null;
        $scope.isLoading = true;
        $http.get('/admin/getusers').
            then(function success(res) {

                if (res.data.success) {
                    $scope.users = res.data.response.users;
                    $scope.customRolesList = res.data.response.roles;
                } else {
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                }
                $scope.isLoading = false;
            }, function error(res) {
                toast.error({
                    bodyText: res.data.errorMessage
                });
                $scope.isLoading = false;
            });
    };

    $scope.onLoadCallbacks.push($scope.getUsers);

    $scope.getRoles = function () {

        $scope.selectedCustomRole = null;
        $scope.isLoading = true;

        $http.get('/admin/getroles')
            .then(function success(res) {

                if (res.data.success) {
                    $scope.customRoles = res.data.response.roles;
                    $scope.allAppRoles = res.data.response.appRoles;
                } else {
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                }

                $scope.isLoading = false;
            }, function error(res) {
                toast.error({
                    bodyText: res.data.errorMessage
                });
                $scope.isLoading = false;
            });
    };

    $scope.getRequestTypes = function () {
        $scope.isLoading = true;
        $scope.selectedRequestType = null;
        $http.get('/admin/getrequesttypes')
            .then(function (res) {
                if (res.data.success) {
                    $scope.requestTypes = res.data.response.requestTypes;
                    $scope.requestFieldTypes = res.data.response.requestFieldTypes;

                    if (res.data.message) {
                        toast.succes({
                            bodyText: res.data.message
                        });
                    }
                }
                else {
                    toast.error({ bodyText: res.data.errorMessage });
                }
                $scope.isLoading = false;
            }).catch(function (res) {
                $scope.isLoading = false;
                toast.error({ bodyText: res.data.errorMessage });
            });
    }

    $scope.addNewUser = function () {

        const newUser = new User("New user");
        $scope.users.unshift(newUser);

        $scope.selectUser(newUser);
    };

    $scope.updateUser = function () {

        if ($scope.selectedUser.id) {
            $scope.isLoading = true;
            $http.post('/admin/updateuser', $scope.selectedUser)
                .then(function (res) {
                    if (res.data.success) {
                        toast.succes({
                            bodyText: res.data.message
                        });
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
        }
        else {
            $scope.isLoading = true;
            $http.post('/Admin/AddUser', $scope.selectedUser)
                .then(function succes(response) {
                    if (response.data.success) {
                        Object.assign($scope.selectedUser, response.data.response, { isChanged: false });
                        toast.succes({
                            bodyText: response.data.message
                        });
                    }
                    else {
                        toast.error({
                            bodyText: response.data.errorMessage
                        });
                    }
                    $scope.isLoading = false;
                }, function error(response) {
                    $scope.isLoading = false;
                });
        }
    };

    $scope.deleteUser = function () {

        if ($scope.selectedUser.id) {
            $scope.isLoading = true;
            $http.post('/admin/deleteuser', { id: $scope.selectedUser.id })
                .then(function success(res) {

                    if (res.data.success) {
                        var userToDeleteIndex = $scope.users.indexOf($scope.selectedUser);
                        $scope.users.splice(userToDeleteIndex, 1);
                        $scope.selectedUser = null;

                        toast.succes({
                            bodyText: res.data.message
                        });
                    }
                    else {
                        toast.error({
                            bodyText: res.data.errorMessage
                        });
                    }
                    $scope.isLoading = false;
                }).catch(function error(res) {
                    $scope.isLoading = false;
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                });
        }
        else {
            $scope.users.splice($scope.users.indexOf($scope.selectedUser), 1);
            $scope.selectedUser = null;
        }
    };

    $scope.selectUser = function (user) {

        if ($scope.selectedUser !== user) {

            if ($scope.selectedUser) {
                $scope.selectedUser.isSelected = false;
            }

            $scope.selectedUser = user;
            $scope.selectedUser.isSelected = true;

            if ($scope.selectedUser.id === '') {
                $scope.selectedUser.isChanged = true;
            }
        }
    };

    $scope.addNewRole = function () {

        const newRole = new Role("Новая роль");
        $scope.customRoles.unshift(newRole);
        $scope.selectCustomRole(newRole);
    }

    $scope.updateCustomRole = function () {

        $scope.isLoading = true;

        if ($scope.selectedCustomRole.id !== 0) {

            $http.post('/admin/updaterole', $scope.selectedCustomRole)
                .then(function (res) {

                    if (res.data.success) {
                        Object.assign($scope.selectedCustomRole, res.data.response, { isChanged: false });

                        if (res.data.message) {
                            toast.succes({
                                bodyText: res.data.message
                            });
                        }
                    }

                    $scope.isLoading = false;
                }).catch(function (res) {
                    $scope.isLoading = false;
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                })
        }
        else {
            $http.post('/admin/addrole', $scope.selectedCustomRole)
                .then(function (res) {

                    if (res.data.success) {
                        Object.assign($scope.selectedCustomRole, res.data.response, { isChanged: false });
                        if (res.data.message) {
                            toast.succes({
                                bodyText: res.data.message
                            });
                        }
                    }
                    else {
                        toast.error({
                            bodyText: res.data.errorMessage
                        });
                    }
                    $scope.isLoading = false;
                }).catch(function (res) {
                    toast.error({
                        bodyText: res.data.errorMessage
                    });
                    $scope.isLoading = false;
                });
        }
    };

    $scope.deleteCustomRole = function () {

        $scope.isLoading = true;

        if ($scope.selectedCustomRole.id) {
            $http.post('/admin/deleterole', $scope.selectedCustomRole)
                .then(function (res) {
                    if (res.data.success) {
                        var roleToDeleteIndex = $scope.customRoles.indexOf($scope.selectedCustomRole);
                        $scope.customRoles.splice(roleToDeleteIndex, 1);
                        $scope.selectedCustomRole = null;

                        if (res.data.message) {
                            toast.succes({
                                bodyText: res.data.message
                            });
                        }
                    }
                    else {
                        toast.error({
                            bodyText: res.data.errorMessage
                        });
                    }
                    $scope.isLoading = false;
                }).catch(function (res) {
                    toast.error({
                        bodyText: res.data.errorMessage
                    })
                });
        }
        else {
            $scope.customRoles.splice($scope.customRoles.indexOf($scope.selectedCustomRole), 1);
            $scope.selectedCustomRole = null;
        }


    }

    $scope.selectCustomRole = function (role) {

        if ($scope.selectedCustomRole !== role) {

            if ($scope.selectedCustomRole) {
                $scope.selectedCustomRole.isSelected = false;
            }

            $scope.selectedCustomRole = role;
            $scope.selectedCustomRole.isSelected = true;

            if ($scope.selectedCustomRole.id === 0) {
                $scope.selectedCustomRole.isChanged = true;
            }
        }
    };

    $scope.selectRequestType = function (requestType) {

        if ($scope.selectedRequestType !== requestType) {

            if ($scope.selectedRequestType) {
                $scope.selectedRequestType.isSelected = false;
            }

            $scope.selectedRequestType = requestType;
            $scope.selectedRequestType.isSelected = true;

            if ($scope.selectedRequestType.id === 0) {
                $scope.selectedRequestType.isChanged = true;
            }
        }
    };

    $scope.addNewRequestType = function () {
        const newRequestType = new RequestType("Новый тип звявки");
        $scope.requestTypes.unshift(newRequestType);
        $scope.selectRequestType(newRequestType);
    }

    $scope.updateRequestType = function () {
        $scope.isLoading = true;

        if ($scope.selectedRequestType.id) {
            $http.post('/admin/UpdateRequestType', $scope.selectedRequestType)
                .then(res => {

                    if (res.data.success) {
                        Object.assign($scope.selectedRequestType, res.data.response, { isChanged: false });

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
                    toast.error({ bodyText: res.data.errorMessage });
                })
        }
        else {
            $http.post('/admin/AddRequestType', $scope.selectedRequestType)
                .then(res => {
                    if (res.data.success) {
                        Object.assign($scope.selectedRequestType, res.data.response, { isChanged: false });

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
                    toast.error({ bodyText: res.data.errorMessage });
                });
        }
    }

    $scope.deleteRequestType = function () {
        $scope.isLoading = true;

        if ($scope.selectedRequestType.id) {
            $http.post(`/admin/DeleteRequestType?id=${$scope.selectedRequestType.id}`)
                .then(res => {

                    if (res.data.success) {
                        var requestTypeToDeleteIndex = $scope.requestTypes.indexOf($scope.selectedRequestType);
                        $scope.requestTypes.splice(requestTypeToDeleteIndex, 1);
                        $scope.selectedRequestType = null;

                        if (res.data.message) {
                            toast.succes({
                                bodyText: res.data.message
                            });
                        }
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
        else {
            $scope.requestTypes.splice($scope.requestTypes.indexOf($scope.selectedRequestType), 1);
            $scope.selectedRequestType = null;
        }
    }

    $scope.addNewRequestTypeField = function () {
        $scope.selectedRequestType.requestTypeFields.unshift({
            name: '',
            type: $scope.requestFieldTypes ? $scope.requestFieldTypes[0].value : null
        });
        $scope.selectedRequestType.isChanged = true;
    }

    function User(userName, email) {
        this.userName = userName;
        this.email = email;
        this.customRoles = $scope.customRolesList.map(e => {
            return {
                ...e
            };
        });
    }

    $scope.setDeleted = function (item, itemsArray) {

        if (!item.id) {
            var itemIndex = itemsArray.indexOf(item);
            itemsArray.splice(itemIndex, 1);
        }
        else {
            item.isDeleted = !item.isDeleted;
            item.isChanged = true;
        }
    }

    function Role(name) {
        this.id = 0;
        this.name = name;
        this.appRoles = $scope.allAppRoles.map(e => {
            return {
                ...e
            }
        });
    }

    function RequestType(name) {
        this.id = 0;
        this.name = name;
        this.requestTypeFields = [];
    }

    $scope.onLoadCallbacks.forEach(function (callBack) {
        callBack();
    });
});