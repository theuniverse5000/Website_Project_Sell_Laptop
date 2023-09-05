
var loginButton = document.getElementById('LoginButton');
loginButton.addEventListener('click', function () {
    $.ajax({
        url: "/Login/Login", // Đường dẫn đến action đã tạo ở bước 1
        type: "GET",
        success: function (result) {
            $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    });
    console.log(1)
})
