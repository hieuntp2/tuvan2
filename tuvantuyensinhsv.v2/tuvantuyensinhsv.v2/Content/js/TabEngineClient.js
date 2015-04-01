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

    app.controller('TagEngineController', ['$http', function ($http) {
        this.object = {};
        this.tags = [];
        var ctrll = this;

        this.loadListTags = function (idbaiviet) {
            $http.get("/TabEngineServer/getTagList?IDBaiViet=" + idbaiviet).success(function (data) {

                ctrll.tags = data;

            }).error(function () {
                alert("Lỗi khi lấy dữ liệu tags");
            });
        }

        this.loadListTagsMBTI = function (idbaiviet) {
            $http.get("/TabEngineServer/getTagListMBTI?idmbti=" + idbaiviet).success(function (data) {

                ctrll.tags = data;

            }).error(function () {
                alert("Lỗi khi lấy dữ liệu tags");
            });
        }

        this.addTagObject = function () {
            if (MaTab == null || MaTab == "") {
                return;
            }
            this.object.Ten = TenTab;
            this.object.ID = MaTab;
            this.object.Loai = LoaiTab;

            this.tags.push(this.object);
            this.object = {};

            $("#_inputsTabs").val(this.Code(this.tags));

            MaTab = "";

        }

        this.InputTagEnter = function (e) {
            this.addTagObject();
            return false;

        }

        this.Code = function (list) {
            var result = "";
            for (var i = 0; i < list.length; i++) {
                var object = list[i];
                result += object.Loai + object.ID + ",";
            }
            result = result.substring(0, result.length - 1);
            return result;
        }

        this.RemoveTag = function (idtag) {
            findAndRemove(this.tags, 'ID', idtag);
            $("#_inputsTabs").val(this.Code(this.tags));
        }

        this._ViewTagClickChange = function (loai, id) {
            alert(loai, id);
        }
    }])

    app.controller('QuicksearchTruong', ['$http', function ($http) {
        this.truong = {};
        this.nganhs = [];
        this.selectednganh = {};
        this.linkprofile = "";

        var ctrll = this;

        this.loadTruong = function (idtruong,idnganh) {
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

                for (var i = 0; i < ctrll.nganhs.length; i++)
                {
                    if (ctrll.nganhs[i].Ten[0] === 'C') {
                        ctrll.nganhs[i].Ten = ctrll.nganhs[i].Ten + " - hệ cao đẳng";
                    }
                    else {
                        ctrll.nganhs[i].Ten = ctrll.nganhs[i].Ten + " - hệ đại học";
                    }
                    if (ctrll.nganhs[i].ID === idnganh)
                    {
                        ctrll.selectednganh = ctrll.nganhs[i];
                    }
                }

            }).error(function () {
                alert("Lỗi khi lấy dữ liệu tags");
            });
        }

        this.changeschool = function()
        {

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

function findAndRemove(array, property, value) {
    $.each(array, function (index, result) {
        if (result[property] == value) {
            array.splice(index, 1);
        }
    });
}
