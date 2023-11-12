var loginButton = document.getElementById('login_button');

function loginWithJWT(ev) {
    ev.preventDefault();

    var UserNameInput = document.getElementById('UserName').value;
    var passwordInput = document.getElementById('PassWord').value;
    console.log(1)
  
    var dataToSend = {
        UserName: UserNameInput,
        Password: passwordInput,
        RememberMe: true,
        ReturnUrl: "/" 
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
          
        }
    });
}



