document.addEventListener("DOMContentLoaded", function () {
    var navBarColorChange = document.getElementById("navbarHeader")

    function changeColor() {
        var colors = ["#E4C2C2", "#F7959F", "#E27196", "#C55081", "indigo", "violet"];
        var randomColor = colors[Math.floor(Math.random() * colors.length)];
        navBarColorChange.style.background = randomColor;

        setTimeout(changeColor, 1)
    }

    changeColor();
});