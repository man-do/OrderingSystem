$(document).on(function () {
    $('#search').bind('input', function () {
        var searchVal = $('#search').val();
        $.ajax({
            url: "/Admin/Index?search=" + searchVal,
            method: "GET",
            contentType: "application/json",
            success: function (data) {
                $('#user-template').html(data);
                console.log("Sukses !");
            },
            error: function () {
                console.log("Deshtoi !");
            }
        });
    });
});