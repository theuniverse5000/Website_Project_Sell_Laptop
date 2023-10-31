﻿
var hotProducts = document.getElementsByClassName('hot-product');

for (var i = 0; i < hotProducts.length; i++) {
    hotProducts[i].addEventListener('click', function (event) {
        event.preventDefault()
        var id = this.getAttribute('data-id');

        
        
        $.ajax({
            url: "/Home/ProductDetail/" + id,
            type: "GET",
            success: function (result) {
                $("#root").html(result); // Cập nhật nội dung của phần tử trên trang web
            },
            error: function () {
                alert("Có lỗi xảy ra.");
            }
        });
    });
}

