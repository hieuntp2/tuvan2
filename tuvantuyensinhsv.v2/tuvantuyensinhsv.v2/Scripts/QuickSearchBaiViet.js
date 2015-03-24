$("#QuickFindInput").typeahead({
    limit: 20,
    source: function (query, process) {
        var listKhoi = [];
        map = {};
        searchkey = $("#QuickFindInput").val();
        // This is going to make an HTTP post request to the controller
        return $.post('/Search/quickSearch?Text=' + searchkey, { query: searchkey }, function (data) {

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
        var stringA = '<a hreft="' + map[item].Href + '">' + map[item].Ten + '</a>';
        var result = $("#tinymce").val() + stringA;

        // Sets the bbcode contents of the activeEditor editor if the bbcode plugin was added
        tinyMCE.activeEditor.setContent(result);
        return item;
    }
});


function AddTab()
{

}