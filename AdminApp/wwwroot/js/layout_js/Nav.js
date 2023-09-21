
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
var ProductDetail = document.getElementById('createproductdetail');
ProductDetail.addEventListener('click', function (event) {
    event.preventDefault();
    $.ajax({
        url: "/ProductDetail/Create",
        type: "GET",
        success: function (result) {
            $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    });

})