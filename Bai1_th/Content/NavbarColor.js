document.addEventListener("DOMContentLoaded", function () {
    var navBarColorChange = document.getElementById("navbarHeader")

    function changeColor() {
        var colors = ["red", "green", "blue", "orange", "indigo", "violet"];
        var randomColor = colors[Math.floor(Math.random() * colors.length)];
        navBarColorChange.style.background = randomColor;

        setTimeout(changeColor, 1000)
    }

    changeColor();
});