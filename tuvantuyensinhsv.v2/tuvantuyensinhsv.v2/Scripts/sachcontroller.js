(function () {
    var app = angular.module("main_app", []);

    app.controller('tangsachcontrolller', ['$scope', '$scope', '$http', '$rootScope',
        function ($scope, $scope, $http, $rootScope) {
            $scope.ten = "";
            $scope.nxb = "";
            $scope.tacgia = "";
            $scope.linkanh = "";
            $scope.saches = [];
            $scope.count = 0;
            $scope.soluong = 1;
            $scope.comment = "";
            $scope.index = -1;

            // value for modal
            $scope.allbook = [];

            $scope.id = -1;

            $scope.tensacherror = "";
            $scope.soluong_alert = "";

            // hàm cho trang tạo sách
            $scope.tangsach = function () {
                $scope.themsach(1);
            }

            $scope.nhansach = function () {
                $scope.themsach(0);
            }

            //loai = 1: tang, loai = 0: nhan
            $scope.themsach = function (loai) {
                $scope.tensacherror = "";
                if ($scope.soluong <= 0) {
                    $scope.soluong_alert = "Số lượng phải >= 1.";
                    return;
                }

                if ($scope.id >= 0) {
                    for (var i = 0; i < $scope.saches.length; i++) {
                        if ($scope.id == $scope.saches[i].id) {
                            alert("Bạn đã có sách này trong danh sách!");
                            return;
                        }
                    }
                }


                if ($scope.ten) {
                    var sach = {};
                    sach.ten = $scope.ten;
                    sach.nxb = $scope.nxb;
                    sach.tacgia = $scope.tacgia;
                    sach.soluong = 1;
                    sach.linkanh = $scope.linkanh;
                    sach.soluong = $scope.soluong;
                    sach.comment = $scope.comment;
                    sach.loai = loai;
                    sach.id = $scope.id;

                    //Neu $scope.index != -1, co nghia la dang thay doi thong tin sach
                    if ($scope.index != -1) {
                        $scope.xoasach($scope.index);
                        sach.index = $scope.index;
                        $scope.index = -1;
                    }
                    else {
                        sach.index = $scope.count;
                        $scope.count += 1;
                    }

                    $scope.saches.push(sach);
                    $scope.resetvalue();
                }
                else {
                    $scope.tensacherror = "Cần điền tên sách";
                    return;
                }
            }

            $scope.xoasach = function (index) {
                for (var i = 0; i < $scope.saches.length; i++) {
                    if ($scope.saches[i].index == index) {
                        $scope.saches.splice(i, 1);
                    }
                }
            }

            $scope.resetvalue = function () {
                $scope.ten = "";
                $scope.nxb = "";
                $scope.tacgia = "";
                $scope.linkanh = "";
                $scope.soluong = 1;
                $scope.comment = "";
                $scope.soluong_alert = "";
                $scope.tensacherror = "";
                $scope.index = -1;
                $scope.id = -1;
            }

            $scope.doithongtin = function (index) {
                for (var i = 0; i < $scope.saches.length; i++) {
                    if ($scope.saches[i].index == index) {
                        $scope.ten = $scope.saches[i].ten;
                        $scope.nxb = $scope.saches[i].nxb;
                        $scope.tacgia = $scope.saches[i].tacgia;
                        $scope.linkanh = $scope.saches[i].linkanh;
                        $scope.soluong = $scope.saches[i].soluong;
                        $scope.id = $scope.saches[i].id;
                        $scope.comment = $scope.saches[i].comment;
                        $scope.index = $scope.saches[i].index;
                    }
                }
            }

            $scope.huytoanbo = function () {
                $scope.saches = [];
                $scope.count = 0;
            }

            $scope.hoanthanh = function () {
                if ($scope.saches.length >= 0) {
                    var postobject = JSON.stringify($scope.saches)
                    // Simple POST request example (passing data) :
                    $http.post('../../sach/tangsach', { saches: $scope.saches }).
                      success(function (data, status, headers, config) {
                          window.location.href = "../../sach";
                      }).
                      error(function (data, status, headers, config) {
                          alert("FAIL");
                      });
                }
                else {
                    alert("Chưa có sách nào!");
                }

            }

            $scope.showallbook = function () {
                $scope.allbook = [];
                $http.get("../../sach/getallbook").success(function (data) {
                    for (var i = 0; i < data.length; i++) {
                        var book = {};
                        book.ten = data[i].Ten;
                        book.linkanh = data[i].linkanh;
                        book.tacgia = data[i].tacgia;
                        book.nxb = data[i].NXB;
                        book.id = data[i].id;

                        $scope.allbook.push(book);
                        $("#allbookmodal").modal('show');
                    }

                }).error(function () {

                });
            }

            // Hàm cho AllBookModal
            $scope.chonsach = function (id) {
                for (var i = 0; i < $scope.allbook.length; i++) {
                    if ($scope.allbook[i].id == id) {
                        $scope.ten = $scope.allbook[i].ten;
                        $scope.nxb = $scope.allbook[i].nxb;
                        $scope.tacgia = $scope.allbook[i].tacgia;
                        $scope.linkanh = $scope.allbook[i].linkanh;
                        $scope.index = -1;
                        $scope.id = $scope.allbook[i].id;
                        $scope.soluong = 1;
                        $scope.comment = "";
                    }
                }
                $("#allbookmodal").modal('hide');
            }

            //$scope.showbook = function () {
            //    $("#tangsachmodal").modal('show');
            //}

            //$rootScope.$on('tangsachcontrolller:showmadal', function (event, id) {
            //    $scope.showbook(id);
            //});

        }]);

    app.controller('bookinforcontroller', ['$scope', '$scope', '$http', '$rootScope',
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

                    if (data.linkanh) {
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

            $scope.dangkynhansach = function () {

            }

            $scope.dangkytangsach = function () {

            }

            $rootScope.$on('bookinforcontroller:showmadal', function (event, id) {
                $scope.getbook(id);
            });
        }]);

    app.controller('bookindexcontroller', ['$scope', '$scope', '$http', '$rootScope',
        function ($scope, $scope, $http, $rootScope) {

            $scope.showTangSachModal = function () {
                $rootScope.$broadcast('tangsachcontrolller:showmadal');
            }

            $scope.showBookInforModal = function (id) {
                $rootScope.$broadcast('bookinforcontroller:showmadal', id);
            }

        }]);
})();


