document.addEventListener('DOMContentLoaded', function () {
    var signUpButton = document.getElementById('signUpButton');

    signUpButton.addEventListener('click', function () {
        $.ajax({
            url: "/Login/SignUp",
            type: "GET",
            success: function (result) {
                $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
            },
            error: function () {
                alert("Có lỗi xảy ra.");
            }
        });
    });
});
