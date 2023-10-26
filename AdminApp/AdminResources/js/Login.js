
var loginButton = document.getElementById("lgbutton");

// Thêm một trình nghe sự kiện cho sự kiện "click" (bấm nút)
loginButton.addEventListener("click", function (event) {
    // Thực hiện các hành động bạn muốn khi nút "ĐĂNG NHẬP" được bấm

    event.preventDefault();

    var UserNameInput = document.getElementById('UserName').value;
    var passwordInput = document.getElementById('PassWord').value;
    var apiUrl = "https://localhost:7286/api/Account/Login";
    // Create an object containing the data to send to the server
    var dataToSend = {
        UserName: UserNameInput,
        Password: passwordInput,
        RememberMe: true,
        ReturnUrl: "/" // Fixed the syntax error here (changed semicolon to colon)
    }
    console.log(dataToSend.UserName)
    $.ajax({
        url: apiUrl,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend), // Thay đổi ở đây
        success: function (response) {
            Cookies.set('account', JSON.stringify(response))
            window.location.href = "/Home";
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi nếu cuộc gọi API thất bại
            console.error(error);
        }
    });
});

