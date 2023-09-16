document.addEventListener('DOMContentLoaded', function () {
    var signUpButton = document.getElementById('signUp');

    signUpButton.addEventListener('click', function (event) {
        event.preventDefault()
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
