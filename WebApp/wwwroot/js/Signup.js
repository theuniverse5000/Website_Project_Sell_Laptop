function OpenSignUp() {
    event.preventDefault();
    $.ajax({
        url: "/Login/SignUp",
        type: "GET",
        success: function (result) {
            $("#root").html(result);
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    });
}
function getFormValues() {
    var signUpdata = {
        userName: document.getElementById('SignUpUserName').value,
        email: document.getElementById('SignUpEmail').value,
        phoneNumber: document.getElementById('SignUpPhoneNumber').value,
        password: document.getElementById('SignUpPassWord').value,
        passwordConfirm: document.getElementById('SignUpComfirmPassWord').value,
    };
    var apiUrl ="/Login/AccountSignUp"
    $.ajax({
        url: apiUrl,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(signUpdata), // Thay đổi ở đây
        success: function (response) {
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}

