$(document).ready(function () {
    $("#testSelect").change(function () {
        $.ajax({
            type: 'POST',
            url: '/Administrador/VisualizeStudentResult',
            dataType: 'json',
            data: { id: $("#testSelect").val() },
            success: function (result) {
                google.charts.load('current', {
                    'packages': ['corechart']
                });
                google.charts.setOnLoadCallback(function () {
                    $("#Graficos").change(function () {
                        var grafico = $("#Graficos").val();
                        drawChart(result, grafico);
                    });
                    drawChart(result,1);
                });
            }
        })
    });
});

function drawChart(result, grafico) {
   

    if (grafico == 1) {
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
            .getElementById('columnchart_div2'));
        columnChart.draw(data, columnChartOptions);
    } else if (grafico == 2) {
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
        var columnChart = new google.visualization.PieChart(document
            .getElementById('columnchart_div2'));
        columnChart.draw(data, columnChartOptions);
    } else if (grafico ==3) {
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
            width: "100%",
            height: "100%",
            bar: { groupWidth: "20%" },
            pieHole: 0.4,
        };
        var columnChart = new google.visualization.PieChart(document
            .getElementById('columnchart_div2'));
        columnChart.draw(data, columnChartOptions);
    }
}
