
var thongKe = document.getElementById('thongKe');
thongKe.addEventListener('click', function (event) {
    event.preventDefault();
    $.ajax({
        url: "/Statistics/GetAllBills",
        type: "GET",
        success: function (result) {
            $("#root").html(result);
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    });

})