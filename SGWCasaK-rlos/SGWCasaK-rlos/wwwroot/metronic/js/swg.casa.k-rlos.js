function showError(message) {
    swal("Error!", message, "error");
}

function showSuccess(message) {
    swal("Exito!", message, "success");
}

function showSuccessAndGoToUrl(message, url) {
    swal("Exito!", message, "success")
        .then(function (result) {
            window.location.replace(url);
        });
}