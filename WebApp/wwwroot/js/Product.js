
var hotProducts = document.getElementsByClassName('hot-product');

for (var i = 0; i < hotProducts.length; i++) {
    hotProducts[i].addEventListener('click', function (event) {
        event.preventDefault()
        var code = this.getAttribute('data-code');
        var encodedCodeProductDetail = encodeURIComponent(code);
        $.ajax({
            url: "/Home/ProductDetail?code=" + encodedCodeProductDetail,
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

