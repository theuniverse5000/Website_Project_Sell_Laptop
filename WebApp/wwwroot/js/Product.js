
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
function BuyProduct(productDetailCode) {
    event.preventDefault();
    console.log(productDetailCode);

    // Mã hóa giá trị nếu cần
    var encodedIdProductDetail = encodeURIComponent(productDetailCode);

    // Gọi API hoặc xử lý sự kiện chính
    $.ajax({
        url: "/ProductDetail/AddProductToCart?productDetailCode=" + encodedIdProductDetail,
        type: "POST",
        success: function (result) {
            // Xử lý kết quả thành công nếu cần
            console.log(result);
        },
        error: function (error) {
            // Xử lý lỗi nếu có
            console.error(error);
        }
    });
}

