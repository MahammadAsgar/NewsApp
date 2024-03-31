function RandomColor() {
    var colors = ["#FF5733", "#FFC300", "#FF33FF", "#3366FF", "#33FF33", "#33CCCC", "#CC33FF"];
    var randomIndex = Math.floor(Math.random() * colors.length);
    return colors[randomIndex];
}