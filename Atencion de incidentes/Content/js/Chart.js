$(document).ready(function () {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        url: '/Administrador/VisualizeStudentResult',
        success: function (result) {
            google.charts.load('current', {
                'packages': ['corechart']
            });
            google.charts.setOnLoadCallback(function () {
                drawChart(result);
            });
        }
    });
});

function drawChart(result) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Nombre');
    data.addColumn('number', 'Cantidad');
    var dataArray = [];

    $.each(result, function (i, obj) {
        dataArray.push([obj.Tecnico, obj.Cantidad]);
    });
    data.addRows(dataArray);

    var columnChartOptions = {
        title: "Top 5 ticket",
        width: 1000,
        height: 400,
        bar: { groupWidth: "20%" },
    };

    var columnChart = new google.visualization.ColumnChart(document
        .getElementById('columnchart_div'));
    columnChart.draw(data, columnChartOptions);
}
