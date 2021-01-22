var input = document.querySelector('#image_uploads');
var preview = document.querySelector('#respuesta');
preview.style.opacity = 0;
input.addEventListener('change', updateImageDisplay);
var color = 0;
var suma = 10;
function updateImageDisplay() {
    while (preview.firstChild) {
        preview.removeChild(preview.firstChild);
    }

    var curFiles = input.files;
    if (curFiles.length === 0) {
        var para = document.createElement('p');
        para.textContent = 'No imagen, seleccione una imagen';
        $("#respuesta").fadeTo("slow", 1);
        preview.appendChild(para);
        $("#respuesta").fadeTo(2000, -1);
    } else {
        for (var i = 0; i < curFiles.length; i++) {
            var para = document.createElement('span');
            if (validFileType(curFiles[i])) {
                para.textContent = 'Operacion exitosa';
                document.getElementById("img2").src = window.URL.createObjectURL(curFiles[i]);
                var formdata = new FormData($('form').get(0));
                Cambiar_Imagen(formdata);
                $("#respuesta").fadeTo("slow", 1);
                preview.appendChild(para);
                $("#respuesta").fadeTo(2000, -1);

            } else {
                para.textContent = 'No es un valido type de imagen. Actualice tu seleccion.';
                $("#respuesta").fadeTo("slow", 1);
                preview.appendChild(para);
                $("#respuesta").fadeTo(2000, -1);
            }
        }
    }
}

function Cambiar_Imagen(file) {
    $.ajax({
        url: '/Home/Recargarimagen',
        type: 'POST',
        data: { Imagen1 : file },
        cache: false,
        processData: false,
        contentType: false,
        success: function (color) {
            alert('Bien prro :v');
        },
        error: function () {
            alert('Error occured');
        }
    });
}

var fileTypes = [
    'image/jpeg',
    'image/pjpeg',
    'image/png'
]

function validFileType(file) {
    for (var i = 0; i < fileTypes.length; i++) {
        if (file.type === fileTypes[i]) {
            return true;
        }
    }

    return false;
}

function returnFileSize(number) {
    if (number < 1024) {
        return number + 'bytes';
    } else if (number > 1024 && number < 1048576) {
        return (number / 1024).toFixed(1) + 'KB';
    } else if (number > 1048576) {
        return (number / 1048576).toFixed(1) + 'MB';
    }
}