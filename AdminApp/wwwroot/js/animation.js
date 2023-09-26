var hamburger = document.querySelector(".hamburger");
var sidebar = document.querySelector(".sidebar");
var rootAdmin = document.getElementById("rootAdmin");

hamburger.addEventListener("click", function () {
    document.body.classList.toggle("active");
    sidebar.classList.toggle("active");

    
    var isSidebarActive = sidebar.classList.contains("active");

    if (isSidebarActive) {
        
        rootAdmin.style.transition = "width 0.5s, padding-left 0.5s"; 
        rootAdmin.style.width = "100%";
        rootAdmin.style.paddingLeft = "0";

    } else {
       
        rootAdmin.style.transition = "width 0.5s, padding-left 0.5s";
        rootAdmin.style.width = "calc(100% - 250px)"; 
        rootAdmin.style.paddingLeft = "250px";

    }
});
