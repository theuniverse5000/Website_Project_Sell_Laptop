
var loginButton = document.getElementById('LoginButton');
loginButton.addEventListener('click', function () {
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
    console.log(1)
})

var cartButton = document.getElementById('CartButton')
cartButton.addEventListener('click', function ()){
    $.ajax({
        url: "/Cart/UserCart"/
            type: "GET",
        success: function (result) {
            $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
        },
        error: function () {
            alert("Có lỗi xảy ra.");
        }
    })
}