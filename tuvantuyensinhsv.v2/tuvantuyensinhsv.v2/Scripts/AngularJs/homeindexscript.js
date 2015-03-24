(function () {
    var app = angular.module('homepage', []);

    app.controller('indexcontroller', ['$http', '$scope', function ($http, $scope) {
        var ctrll = this;
        this.posts = [];

        this.getQuestions = function (index) {

            $http.get("/baiviets/getNewPost?index=" + index).success(function (data) {
                ctrll.posts.push.apply(ctrll.posts, data);
            }).error(function () {
                alert("Lỗi khi lấy dữ liệu bai viet");
            });

            $http.get("/questions/getquestion?index=" + index).success(function (data) {
                ctrll.posts.push.apply(ctrll.posts,data);
            }).error(function () {
                alert("Lỗi khi lấy dữ liệu questions");
            });
        }

        this.init = function ()
        {
            ctrll.getQuestions(0);
        }

        $scope.addpost = function (index) {
            this.getQuestions(index);
        }

        this.postclick = function (id, loai)
        {
            if(loai === 'b')
            {
                window.location.href = "/baiviets/details/" + id;
            }
            if (loai === 'q') {
                window.location.href = "/questions/details/" + id;
            }
        }

        $scope.getQuestions = function (index) {

            $http.get("/baiviets/getNewPost?index=" + index).success(function (data) {
                ctrll.posts.push.apply(ctrll.posts, data);
            }).error(function () {
                alert("Lỗi khi lấy dữ liệu bai viet");
            });

            $http.get("/questions/getquestion?index=" + index).success(function (data) {
                ctrll.posts.push.apply(ctrll.posts, data);
            }).error(function () {
                alert("Lỗi khi lấy dữ liệu questions");
            });
        }
    }])
})();

