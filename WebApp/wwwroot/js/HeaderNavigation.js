
var loginButton = document.getElementById('LoginIcon');
loginButton.addEventListener('click', function (event) {
    event.preventDefault();
    $.ajax({
        url: "/Login/Login", 
        type: "GET",
        success: function (result) {
            $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    });

})


var cartButton = document.getElementById('CartButton');

cartButton.addEventListener('click', function (event) {
    event.preventDefault()
        $.ajax({
            url: "/Cart/UserCart",
            type: "GET",
            success: function (result) {
                $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
            },
            error: function () {
                alert("Có lỗi xảy ra.");
            }
        });
});


