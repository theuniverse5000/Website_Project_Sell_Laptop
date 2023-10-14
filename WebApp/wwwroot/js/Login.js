var loginButton = document.getElementById('login_button');

function loginWithJWT(ev) {
    ev.preventDefault();

    var emailInput = document.getElementById('UserName').value;
    var passwordInput = document.getElementById('PassWord').value;
    console.log(1)
    // Create an object containing the data to send to the server
    var dataToSend = {
        UserName: emailInput,
        Password: passwordInput,
        RememberMe: true,
        ReturnUrl: "/" // Fixed the syntax error here (changed semicolon to colon)
    }
    console.log(dataToSend.UserName)
    $.ajax({
        url: "/Login/LoginWithJWT",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend), // Thay đổi ở đây
        success: function (response) {
            // Xử lý kết quả thành công
            alert(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi nếu cuộc gọi API thất bại
            console.error(error);
        }
    });
}



