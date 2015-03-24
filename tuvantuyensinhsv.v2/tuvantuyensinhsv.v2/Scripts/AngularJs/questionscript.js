(function () {
    var app = angular.module('questionapp', []);

    app.controller('questioncontroller', ['$http', function ($http) {
        var ctrll = this;
        this.question = [];

        this.getQuestions = function (index) {
            $http.get("/questions/getquestion?index=" + index).success(function (data) {

                ctrll.question = data;

            }).error(function () {
                alert("Lỗi khi lấy dữ liệu questions");
            });
        }

        this.init = function()
        {
            ctrll.getQuestions(0);
        }
    }])
})();