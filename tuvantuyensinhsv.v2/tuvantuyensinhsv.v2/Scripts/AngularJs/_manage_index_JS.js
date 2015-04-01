var TenTab;
var MaTab;
var LoaiTab;
var ListTab = "";
$("#QuickFindInput").typeahead({
    limit: 100,
    source: function (query, process) {
        var listKhoi = [];
        map = {};
        searchkey = $("#QuickFindInput").val();
        // This is going to make an HTTP post request to the controller
        return $.post('/TabEngineServer/quickSearch?Text=' + searchkey, { query: searchkey }, function (data) {

            // Loop through and push to the array
            $.each(data, function (i, khoi) {
                map[khoi.Ten] = khoi;
                listKhoi.push(khoi.Ten);
            });

            // Process the details
            process(listKhoi);

        });
    },
    updater: function (item) {

        TenTab = map[item].Ten;
        MaTab = map[item].ID;
        LoaiTab = map[item].Loai;

        return item;
    }
});
(function () {
    var app = angular.module('ProjectHApp', []);
    app.controller('QuicksearchTruong', ['$http', function ($http) {
        this.truong = {};
        this.nganhs = [];
        this.selectednganh = {};
        this.linkprofile = "";

        var ctrll = this;

        this.loadTruong = function (idtruong, idnganh) {
            $http.get("/TabEngineServer/getTruong?id=" + idtruong).success(function (data) {
                ctrll.truong = data;
                ctrll.loadNganh(idtruong, idnganh);
            }).error(function () {
                alert("Lỗi khi lấy dữ liệu tags");
            });
        }

        this.loadNganh = function (idtruong, idnganh) {
            $http.get("/TabEngineServer/getNganh?idtruong=" + idtruong).success(function (data) {
                ctrll.nganhs = data;

                for (var i = 0; i < ctrll.nganhs.length; i++) {
                    if (ctrll.nganhs[i].Ten[0] === 'C') {
                        ctrll.nganhs[i].Ten = ctrll.nganhs[i].Ten + " - hệ cao đẳng";
                    }
                    else {
                        ctrll.nganhs[i].Ten = ctrll.nganhs[i].Ten + " - hệ đại học";
                    }
                    if (ctrll.nganhs[i].ID === idnganh) {
                        ctrll.selectednganh = ctrll.nganhs[i];
                    }
                }

            }).error(function () {
                alert("Lỗi khi lấy dữ liệu tags");
            });
        }

        this.changeschool = function () {

            $("#_hidden_idTruong").val(MaTab);
            ctrll.loadTruong(MaTab, "");
            ctrll.loadNganh(MaTab, "");
            ctrll.truong.ID = MaTab;
        }

        this.changenganh = function () {
            $("#_selecte_nganh").val();
            ctrll.loadTruong(MaTab, "");
            ctrll.loadNganh(MaTab, "");

            ctrll.truong.ID = MaTab;
        }
    }])
})();