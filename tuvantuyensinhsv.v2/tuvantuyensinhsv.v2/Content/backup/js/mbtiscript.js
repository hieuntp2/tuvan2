
function fstep1(key) {
    $("#result_step_1").html(key);
    call_SN();
}

function fstep2(key) {
    $("#result_step_2").html(key);
    call_TF();
}

function fstep3(key) {
    $("#result_step_3").html(key);
    call_JP();
}

function fstep4(key) {
    $("#result_step_4").html(key);
}

function call_EI() {
    $("#li_step0").removeClass("active");
    $("#li_step2").removeClass("active");
    $("#li_step3").removeClass("active");
    $("#li_step4").removeClass("active");
    $("#li_step1").addClass("active");
    
    $("#intro").removeClass("in active");
    $("#profile").removeClass("in active");
    $("#messages").removeClass("in active");
    $("#settings").removeClass("in active");
    $("#intro").removeClass("in active");
    $("#home").addClass("in active");
}
function call_SN() {
    $("#li_step0").removeClass("active");
    $("#li_step1").removeClass("active");
    $("#li_step3").removeClass("active");
    $("#li_step4").removeClass("active");
    $("#li_step2").addClass("active");

    $("#profile").removeClass("in active");
    $("#home").removeClass("in active");
    $("#messages").removeClass("in active");
    $("#settings").removeClass("in active");
    $("#intro").removeClass("in active");
    $("#profile").addClass("in active");
}
function call_TF() {
    $("#li_step0").removeClass("active");
    $("#li_step2").removeClass("active");
    $("#li_step1").removeClass("active");
    $("#li_step4").removeClass("active");
    $("#li_step3").addClass("active");

    $("#intro").removeClass("in active");
    $("#profile").removeClass("in active");
    $("#home").removeClass("in active");
    $("#settings").removeClass("in active");
    $("#intro").removeClass("in active");
    $("#messages").addClass("in active");
}
function call_JP() {
    $("#li_step0").removeClass("active");
    $("#li_step2").removeClass("active");
    $("#li_step3").removeClass("active");
    $("#li_step1").removeClass("active");
    $("#li_step4").addClass("active");

    $("#intro").removeClass("in active");
    $("#profile").removeClass("in active");
    $("#messages").removeClass("in active");
    $("#home").removeClass("in active");
    $("#intro").removeClass("in active");
    $("#settings").addClass("in active");
}

function _fgetmbti() {
    var idmbti = "";
    idmbti += $("#result_step_1").html();
    idmbti += $("#result_step_2").html();
    idmbti += $("#result_step_3").html();
    idmbti += $("#result_step_4").html();


    if (idmbti.length < 4) {
        alert("Cần chọn đủ 4 ký tự!");
    }
    else {

        $("#_imtbi_result").html("<div class='panel'><img src='http://rpg.drivethrustuff.com/shared_images/ajax-loader.gif' style='width:150px;'/></div>");
        $.ajax({
            url: "/MBTIs/Details",
            data: { id: idmbti },
            type: "get",

            success: function (html) {
                $("#_imtbi_result").html(html);
            }
        });
    }
}
function _flocdiem() {

    var _idnganh = $('#_idmodelnganh').val();
    var _diem = $('#_match').val();

    if (_diem <= 0) {
        alert("Bạn cần nhập điểm phù hợp!");
        return;
    }
    $("#_mymatchModalBody").html("Chờ xíu nhé....");
    $.ajax({
        url: "/MBTIs/getRangeSchool",
        data: { idnganh: _idnganh, diem: _diem },
        type: "get",
        success: function (html) {
            $("#_mymatchModalBody").html(html);
        }
    });
}
$("#_match").keypress(function (e) {

    if (e.which == 13) {
        _flocdiem();
    }
});

function fallschool()
{
    var _idnganh = $('#_idmodelnganh').val();
    window.location = "/Nganhs/Details/"+_idnganh;
}
