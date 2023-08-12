

$(document).ready(function () {
    $.ajax({
        url: "https://api.example.com/data", // URL API mà bạn muốn gửi yêu cầu GET đến
        type: "GET",
        dataType: "json",
        success: function (response) {
            // Xử lý dữ liệu khi yêu cầu API thành công
            displayData(response);
        },
        error: function (xhr, status, error) {
            // Xử lý khi có lỗi xảy ra trong quá trình yêu cầu API
            console.log("Error: " + error);
        }
    });
});

function displayData(data) {
    var html = "";

    // Tạo HTML cho mỗi dữ liệu trong response
    for (var i = 0; i < data.length; i++) {
        var item = data[i];
        html += "<p>Thông tin: " + item.thongTin + ", Thời gian: " + item.thoiGian + "</p>";
    }

    // Đưa HTML vào phần tử có id là "data-container"
    $("#data-container").html(html);
}
