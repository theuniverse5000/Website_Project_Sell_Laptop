var logginButton = document.getElementById('signUp');
var myData = "Dữ liệu chuỗi của bạn ở đây";
logginButton.addEventListener('click', function (ev) {
    ev.preventDefault();

    $.ajax({
        url: "/Login/LoginWithJWT",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({   "okkk" }), 
        success: function (response) {
            alert(myData);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi trong trường hợp gọi API thất bại
            console.error(error);
        }
        
    });
})
