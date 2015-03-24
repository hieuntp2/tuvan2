$("#Search_Input").keyup(function (e) {

    if (e.keyCode == 13) {
        Layout_SearchButtonClick();
    }
});
$("#Search_Input").typeahead({
    limit: 20,
    source: function (query, process) {
        var listKhoi = [];
        map = {};
        var quicksearchkey = $("#Search_Input").val();
        searchkey = removeWord(quicksearchkey);
        // This is going to make an HTTP post request to the controller
        return $.post('/Search/quickSearch?Text=' + searchkey, { query: searchkey },

function (data) {

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
        // Set the text to our selected id

        // window.location = map[item].Href;
        if (item == "Không tìm thấy") {
            return null;
        }
        return item;
    }
});


function Layout_SearchButtonClick()
{
    var key = $("#Search_Input").val().toLowerCase();

    var res = key.split(" ");
    var first = res[0];
    switch (first) {
        case 'trường':
            key = removeWord(key);
            key = key.trim();
            window.location = "/Search/typekey?key=" + key + "&t=t";
            return;
        case 'ngành':
            key = removeWord(key);
            key = key.trim();
            window.location = "/Search/typekey?key=" + key + "&t=n";
            return;
        case 'khoa':
            key = removeWord(key);
            key = key.trim();
            window.location = "/Search/typekey?key=" + key + "&t=n";
            return;
        case 'tỉnh':
            key = removeWord(key);
            key = key.trim();
            window.location = "/Search/typekey?key=" + key + "&t=p";
            return;
        case 'thành phố':
            key = removeWord(key);
            key = key.trim();
            window.location = "/Search/typekey?key=" + key + "&t=p";
            return;
    }
    key = removeWord(key);

    if (key == "y" || key == "dược" || key == "marketing")
    {
        window.location = "/Search/Key?key=" + key;
        return;
    }
    if (countWords(key))
    {
        window.location = "/Search/Key?key=" + key;
    }
    else {
        ShowMessageAlert("Hãy nhập nhiều hơn 2 từ để tìm kiếm");
    }
}

function removeWord(key)
{
    var res = key.split(" ");
    if (res[0].toLowerCase() == "trường"
        || res[0].toLowerCase() == "khoa"
        || res[0].toLowerCase() == "ngành"
        || res[0].toLowerCase() == "tỉnh")
    {
        key = "";
        for(var i = 1; i<res.length; i++)
        {
            key += res[i] + " ";
        }
    }
    key.trim();
    return key;
}

function countWords(s) {
    s = s.replace(/(^\s*)|(\s*$)/gi, "");
    s = s.replace(/[ ]{2,}/gi, " ");
    s = s.replace(/\n /, "\n");
    if (s.split(' ').length < 2)
        return false;
    else
        return true;
}


function ShowMessageAlert(message)
{
    $("#_mesageAlertError").text(message);
    $("#_mesageAlertError").animate({ opacity: "1" }, "slow");
    $("#_mesageAlertError").animate({ right: "+=350px" }, "slow");

    timer();
}

function HideMessageAlert() {
    $("#_mesageAlertError").animate({ right: "-=350px" },"slow");
    $("#_mesageAlertError").animate({ opacity: "0" }, "slow");
  
    $("#_mesageAlertError").text(" -->>");
    count = 10;
    counter = setInterval(timer, 1000);
}

var count = 10;

var counter = setInterval(timer, 1000); //1000 will  run it every 1 second

function timer() {
    count = count - 1;
    if (count < 0) {
        clearInterval(counter);
        
        HideMessageAlert();
        return;
    }

    //Do code for showing the number of seconds here
}