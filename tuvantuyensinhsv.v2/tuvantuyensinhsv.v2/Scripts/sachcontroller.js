(function () {
    var app = angular.module("main_app", []);

    app.controller('tangsachcontrolller', ['$scope', '$scope', '$http','$rootScope',
        function ($scope, $scope, $http, $rootScope) {
            $scope.title = "";
            $scope.message = "";
            $scope.alert_title = "";
            $scope.alert_message = "";

            $scope.showbook = function () {
                $("#tangsachmodal").modal('show');
            }

            $rootScope.$on('tangsachcontrolller:showmadal', function (event, id) {
                $scope.showbook(id);
            });

        }]);

    app.controller('bookinforcontroller', ['$scope', '$scope', '$http','$rootScope',
        function ($scope, $scope, $http, $rootScope) {
            $scope.Ten = "";
            $scope.linkanh = "";
            $scope.nxb = "";
            $scope.tacgia = "";
            $scope.id = "";
            $scope.nguoi_cho = [];
            $scope.nguoi_nhan = [];
            $scope.getbook = function (id) {            

                $http.get("../../sach/getbookinfor?id=" + id).success(function (data) {
                    $scope.Ten = data.Ten;
                    $scope.id = data.id;                   
                    $scope.nxb = data.NXB;
                    $scope.tacgia = data.tacgia;

                    if(data.linkanh)
                    {
                        $scope.linkanh = data.linkanh;
                    }
                    else {
                        $scope.linkanh = "../../content/default_book.png";
                    }
                    $scope.nguoi_cho = data.nguoi_cho;
                    $scope.nguoi_nhan = data.nguoi_nhan;


                    $("#bookinformodal").modal('show');
                }).error(function () {
                    alert("Có lỗi xuất hiện..");
                });


                //// send post
                //$http.post('../../service/reportBug', { title: $scope.title, message: $scope.message }).
                //  success(function (data, status, headers, config) {
                //      my_alert_service.show_my_info("Send message successfully. Thank you for your feed back!");
                //  }).
                //  error(function (data, status, headers, config) {
                //      my_alert_service.show_my_alert("Send message failed. We sorry this in-convinion. Please try again later.");
                //  });

                // reset value


            }

            $scope.dangkynhansach = function()
            {

            }

            $scope.dangkytangsach = function () {

            }

            $rootScope.$on('bookinforcontroller:showmadal', function (event, id) {
                $scope.getbook(id);
            });
        }]);

    app.controller('bookindexcontroller', ['$scope', '$scope', '$http','$rootScope',
        function ($scope, $scope, $http, $rootScope) {

            $scope.showTangSachModal = function () {
                $rootScope.$broadcast('tangsachcontrolller:showmadal');              
            }

            $scope.showBookInforModal = function (id) {
                $rootScope.$broadcast('bookinforcontroller:showmadal', id);
            }

        }]);
})();


