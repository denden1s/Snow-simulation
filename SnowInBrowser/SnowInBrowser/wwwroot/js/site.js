document.onload = setInterval(draw, 16);
function draw()
{
    $.ajax({
        url: "/home/GetSnow",
        success: function (data) {
            console.log(data);
            let canvas = document.getElementById("snowView");
            let context = canvas.getContext('2d');
            context.beginPath();
            context.clearRect(0, 0, canvas.width, canvas.height);
            context.fillStyle = 'white';
            for (let i = 0; i < data.length; i++)
            {
                context.moveTo(data[i].x, data[i].y);
                context.arc(data[i].x, data[i].y, 5, 0, 2 * Math.PI);
                context.fill();
            }
            context.closePath();
        }
    });
}